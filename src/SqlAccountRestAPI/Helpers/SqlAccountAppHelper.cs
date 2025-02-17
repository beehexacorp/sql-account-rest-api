using System.Reflection;
using System.Text.Json.Nodes;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;
using SqlAccountRestAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.IO.Compression;

namespace SqlAccountRestAPI.Helpers;

public class SqlAccountAppHelper
{
    private readonly SqlAccountORM _microORM;
    private readonly SqlAccountFactory _factory;

    public SqlAccountAppHelper(
        SqlAccountORM microORM,
        SqlAccountFactory factory)
    {
        _microORM = microORM;
        _factory = factory;
    }

    public async Task<SqlAccountTotalInfo> GetInfo()
    {
        var result = new SqlAccountTotalInfo()
        {
            sqlAccountAppInfo = GetAppInfo(),
            releaseInfo = await GetReleaseInfo()
        };
        return result;
    }

    public IEnumerable<SqlAccountModuleInfo> GetModules()
    {
        dynamic app = _factory.GetInstance();
        var result = new List<SqlAccountModuleInfo>();
        for (int i = 0; i < app.Modules.Count; i++)
        {
            var item = app.Modules.Items(i);
            result.Add(new SqlAccountModuleInfo
            {
                Code = item.Code,
                Description = item.Description
            });
        }
        return result;
    }

    public IEnumerable<SqlAccountActionInfo> GetActions()
    {
        dynamic app = _factory.GetInstance();
        var results = new List<SqlAccountActionInfo>();
        for (int i = 0; i < app.Actions.Count; i++)
        {
            results.Add(new SqlAccountActionInfo
            {
                Name = app.Actions.Items(i).Name
            });
        }
        return results;
    }

    public IEnumerable<BizObjectInfo> GetBizObjects()
    {
        dynamic app = _factory.GetInstance();
        var results = new List<BizObjectInfo>();
        for (int i = 0; i < app.BizObjects.Count; i++)
        {
            results.Add(new BizObjectInfo
            {
                Name = app.BizObjects.Items(i)
            });
        }
        return results;
    }

    public object? GetBizObjectInfo(string name)
    {
        /**
        TODO: implement this function
        {
          "name": "...",
          "description": "...",
          "fields": Array<string>
          "cds": ...
        }
        // alternative
        {
          "name": "...",
          "datasets": [
            {
              "name": "...",
              "fields": Array<string>
            },
            {
              "name": "...",
              "fields": Array<string>
            },...
          ]      
        
        }
        */
        var result = new Dictionary<string, object?>
        {
            { "name", name }
        };
        var datasetList = new List<Dictionary<string, object?>>();
        dynamic app = _factory.GetInstance();
        var datasets = app.BizObjects.Find(name).Datasets;

        for (int i = 0; i < datasets.Count; i++)
        {
            var dataset = datasets.Items(i);
            var fields = _microORM.FieldIterator(dataset.Fields);
            var datasetData = new Dictionary<string, object?>
            {
                { "name", dataset.Name },
                { "fields", fields }
            };
            datasetList.Add(datasetData);
        }
        result.Add("datasets", datasetList);
        return result;

        throw new NotImplementedException();
    }
    public async Task<IDictionary<string, object>> Update()
    {
        var releaseInfo = await GetReleaseInfo();
        SystemHelper.WriteJsonFile(await SystemHelper.GetCliConfigurationFilePath(), new Dictionary<string, object>
        {
            {"API_VERSION", releaseInfo["LATEST_VERSION"]}
        });
        string downloadUrl = await GithubHelper.GetDownloadUrl();

        string appDir = releaseInfo["APP_DIR"].ToString()!.Replace("\\", "/");
        string appName = releaseInfo["APP_NAME"].ToString()!;
        string appPort = releaseInfo["APP_PORT"].ToString()!;
        string processName = ApplicationConstants.APPLICATION_NAME.ToString();
        string deploymentMethod = releaseInfo["DEPLOYMENT_METHOD"].ToString()!;

        // WindowsStartup
        // stop process -> create backup -> download zip -> extract zip -> check & backup -> start process
        // WindowsService
        // stop service -> create backup -> download zip -> extract zip -> check & backup -> start service
        // IIS
        // stop site -> create backup -> download zip -> extract zip -> check & backup -> start site
        string powerShellScript = $@"
        $ProcessName = '{processName}'
        $ProcessPath = '{Path.Combine(appDir, appName, processName + ".exe")}'
        $AppName = '{appName}'
        $DownloadUrl = '{downloadUrl}'
        $AppDir = '{appDir}'
        $PackageDir = '{Path.Combine(appDir, appName)}'
        $SwaggerUrl = 'http://localhost:{appPort}/swagger'
        $DeploymentMethod = '{deploymentMethod}'

        if($DeploymentMethod -eq 'WindowsStartup') {{
            $process = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue
            if ($process) {{ 
                Stop-Process -Name $ProcessName -Force
                Write-Host 'Process $ProcessName has stopped.'
            }}
        }}

        if($DeploymentMethod -eq 'WindowsService') {{
            $service = Get-Service -Name $AppName -ErrorAction SilentlyContinue
            if ($service) {{
                Stop-Service -Name $AppName -Force
                Write-Host 'Service $AppName has stopped.'
            }}
        }}

        if($DeploymentMethod -eq 'IIS') {{
            $site = Get-WebSite -Name $AppName -ErrorAction SilentlyContinue
            if ($site) {{
                Stop-WebSite -Name $AppName -Force
                Write-Host 'Site $AppName has stopped.'
            }}
        }}

        $BackupDir = Join-Path -Path $AppDir -ChildPath 'backup'
        if (Test-Path $BackupDir) {{
            Remove-Item -Path $BackupDir -Recurse -Force
        }}
        New-Item -Path $BackupDir -ItemType Directory
        Move-Item -Path $PackageDir -Destination $BackupDir

        $DownloadPath = Join-Path -Path $AppDir -ChildPath 'downloaded.zip'
        Invoke-WebRequest -Uri $DownloadUrl -OutFile $DownloadPath

        Expand-Archive -Path $DownloadPath -DestinationPath $PackageDir -Force
        Remove-Item -Path $DownloadPath -Force

        if($DeploymentMethod -eq 'WindowsStartup') {{
            Start-Process $ProcessPath
        }}

        if($DeploymentMethod -eq 'WindowsService') {{
            $service = Get-Service -Name $AppName -ErrorAction SilentlyContinue
            if ($service) {{
                Start-Service -Name $AppName
            }}
        }}

        if($DeploymentMethod -eq 'IIS') {{
            $site = Get-WebSite -Name $AppName -ErrorAction SilentlyContinue
            if ($site) {{
                Start-WebSite -Name $AppName
            }}
        }}

        Start-Sleep -Seconds 10
        try {{
            $response = Invoke-WebRequest -Uri $SwaggerUrl -UseBasicParsing -TimeoutSec 5
            if ($response.StatusCode -eq 200) {{
                Write-Host 'Check passed. Update successful.'
                Remove-Item -Path $BackupDir -Recurse -Force
            }} else {{
                throw 'Unexpected response status.'
            }}
        }} catch {{
            Write-Host 'Check failed. Reverting to backup...'
            
            if($DeploymentMethod -eq 'WindowsStartup') {{
                $process = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue
                if ($process) {{ 
                    Stop-Process -Name $ProcessName -Force
                    Write-Host 'Process $ProcessName has stopped.'
                }}
            }}

            if($DeploymentMethod -eq 'WindowsService') {{
                $service = Get-Service -Name $AppName -ErrorAction SilentlyContinue
                if ($service) {{
                    Stop-Service -Name $AppName -Force
                    Write-Host 'Service $AppName has stopped.'
                }}
            }}

            if($DeploymentMethod -eq 'IIS') {{
                $site = Get-WebSite -Name $AppName -ErrorAction SilentlyContinue
                if ($site) {{
                    Stop-WebSite -Name $AppName -Force
                    Write-Host 'Site $AppName has stopped.'
                }}
            }}

            $BackupPackagePath = Join-Path -Path $BackupDir -ChildPath $AppName
            Remove-Item -Path $PackageDir -Recurse -Force
            Move-Item -Path $BackupPackagePath -Destination $AppDir

            Remove-Item -Path $BackupDir -Recurse -Force
            
            if($DeploymentMethod -eq 'WindowsStartup') {{
                Start-Process $ProcessPath
            }}

            if($DeploymentMethod -eq 'WindowsService') {{
                $service = Get-Service -Name $AppName -ErrorAction SilentlyContinue
                if ($service) {{
                    Start-Service -Name $AppName
                }}
            }}

            if($DeploymentMethod -eq 'IIS') {{
                $site = Get-WebSite -Name $AppName -ErrorAction SilentlyContinue
                if ($site) {{
                    Start-WebSite -Name $AppName
                }}
            }}

            Write-Host 'Revert complete. Application is running on the previous version.'
        }}
    ";

        // Start PowerShell process
        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoExit -NoProfile -ExecutionPolicy Bypass -Command \"{powerShellScript}\"",
            Verb = "runas",
            UseShellExecute = true,
        };

        System.Diagnostics.Process.Start(processInfo);

        return new Dictionary<string, object>
        {
            { "Status", "Update process started. Service will restart soon." },
        };
    }
    public string GetVersion()
    {
        dynamic app = _factory.GetInstance();
        return app.Version;
    }
    public async Task<IDictionary<string, object>> GetReleaseInfo()
    {
        string configPath = await SystemHelper.GetCliConfigurationFilePath();

        if (!File.Exists(configPath))
        {
            return new Dictionary<string, object>
                {
                    { "Error", "The configuration file does not exist." }
                };
        }
        var result = new Dictionary<string, object> { };
        string fileContent = File.ReadAllText(configPath);

        // Parse JSON to Dictionary
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
        var applicationInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(fileContent, options);
        // handle Github API limit

        var releaseInfo = await GithubHelper.GetLatestReleaseInfo();
        applicationInfo!["LATEST_VERSION"] = releaseInfo["tag_name"];


        return applicationInfo;

    }
    public SqlAccountAppInfo GetAppInfo()
    {
        dynamic app = _factory.GetInstance();
        var result = new SqlAccountAppInfo
        {
            Title = app.Title,
            ReleaseDate = app.ReleaseDate.ToString("yyyy-MM-dd"),
            BuildNo = app.BuildNo.ToString(),
            Version = app.Version.ToString()
        };
        return result;
    }
}
