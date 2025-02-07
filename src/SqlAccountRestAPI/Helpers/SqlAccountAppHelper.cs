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
        var result = new SqlAccountTotalInfo(){
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
        string appPort = ApplicationConstants.APPLICATION_PORT.ToString();
        string ProcessName = ApplicationConstants.APPLICATION_NAME.ToString();
        // PowerShell script as a string
        // stop process -> create backup -> download zip -> extract zip -> check & backup -> start process
        string powerShellScript = $@"
        $ProcessName = '{ProcessName}'
        $ProcessPath = '{Path.Combine(appDir, appName, ProcessName + ".exe")}'
        $AppName = '{appName}'
        $DownloadUrl = '{downloadUrl}'
        $AppDir = '{appDir}'
        $PackageDir = '{Path.Combine(appDir, appName)}'
        $SwaggerUrl = 'http://localhost:{appPort}/swagger'

        $process = Get-Process -Name $processName -ErrorAction SilentlyContinue
        if ($process) {{ 
            Stop-Process -Name $processName -Force
            Write-Host 'Process $processName has stopped.'
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

        Start-Process $ProcessPath
        Write-Host 'Process $processName has started.'

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
            
            $process = Get-Process -Name $processName -ErrorAction SilentlyContinue
            if ($process) {{ 
                Stop-Process -Name $processName -Force
            }}

            $BackupPackagePath = Join-Path -Path $BackupDir -ChildPath $AppName
            Remove-Item -Path $PackageDir -Recurse -Force
            Move-Item -Path $BackupPackagePath -Destination $AppDir

            Remove-Item -Path $BackupDir -Recurse -Force
            
            Start-Process $ProcessPath

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
    public async Task<IDictionary<string,object>> GetReleaseInfo(){
        string configPath = await SystemHelper.GetCliConfigurationFilePath();
        
        if (!File.Exists(configPath))
        {
            return new Dictionary<string, object>
                {
                    { "Error", "The configuration file does not exist." }
                };
        }
        try
        {
            var result = new Dictionary<string,object>{};
            string fileContent = File.ReadAllText(configPath);

            // Parse JSON to Dictionary
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            var applicationInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(fileContent, options);
            // handle Github API limit
            try {
                var releaseInfo = await GithubHelper.GetLatestReleaseInfo();
                applicationInfo!["LATEST_VERSION"] = releaseInfo["tag_name"];
            }
            catch (Exception ex){
                applicationInfo!["LATEST_VERSION"] = $"Error: {ex.Message}";
            }
            
            return applicationInfo;
        }
        catch (Exception ex)
        {
            return new Dictionary<string, object>
            {
                { "Error", ex.Message }
            };
        }

    }
    public SqlAccountAppInfo GetAppInfo(){
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
