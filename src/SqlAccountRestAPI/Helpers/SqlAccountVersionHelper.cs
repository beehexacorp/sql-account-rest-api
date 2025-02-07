using Microsoft.VisualBasic;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.ViewModels;
namespace SqlAccountRestAPI.Helpers;
public class SqlAccountVersionHelper
{
    private readonly SqlAccountFactory _factory;
    public SqlAccountVersionHelper(
        SqlAccountFactory factory
    ){
        _factory = factory;
    }
    private readonly Dictionary<string, SqlAccountVersionBreakingChanges> _config = new()
    {
        
    };
    public string GetRunningVersion() {
        var app = _factory.GetInstance();
        return app.Version;
    }
    public List<string>? GetConfig(string version, string entityType, string functionCall)
    {
        if (_config.TryGetValue(version, out var config) && 
            config.EntityType == entityType && 
            config.FunctionCall == functionCall)
        {
            return config.AcceptedParams;
        }
        return null;
    }
}
