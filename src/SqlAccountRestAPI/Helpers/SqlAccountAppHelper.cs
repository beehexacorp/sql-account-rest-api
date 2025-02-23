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
using Scriban;

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
        string appPort = releaseInfo["PORT"].ToString()!;
        string appPoolName = releaseInfo["APP_POOL_NAME"].ToString()!;
        string deploymentMethod = releaseInfo["DEPLOYMENT_METHOD"].ToString()!;
        string packageDir = Path.Combine(appDir, appName);
        string processName = ApplicationConstants.APPLICATION_NAME.ToString();
        string processPath = Path.Combine(appDir, appName, processName + ".exe");

        var templatePath = Path.Combine(AppContext.BaseDirectory, "assets/scripts/update_app.ps1.template");
        
        var templateContent = await File.ReadAllTextAsync(templatePath);
        var template = Template.Parse(templateContent);

         var scriptContent = template.Render(new
        {
            app_dir = appDir,
            app_name = appName,
            app_port = appPort,
            app_pool_name = appPoolName,
            deployment_method = deploymentMethod,
            download_url = downloadUrl,
            package_dir = packageDir,
            process_name = processName,
            process_path = processPath
        });


        // Start PowerShell process
        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = deploymentMethod != "WindowsStartup" 
                ? $"-NoProfile -ExecutionPolicy Bypass -Command \"{scriptContent}\""
                : $"-NoExit -NoProfile -ExecutionPolicy Bypass -Command \"{scriptContent}\"",
            Verb = "runas",
            UseShellExecute = true,
        };

        System.Diagnostics.Process.Start(processInfo);
        var response = "Update process starated. Service will restart soon.";
        if (deploymentMethod == "IIS") {
            response = "Update process starated. IIS will restart in 10 seconds. Please close every swagger window.";
        }
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
