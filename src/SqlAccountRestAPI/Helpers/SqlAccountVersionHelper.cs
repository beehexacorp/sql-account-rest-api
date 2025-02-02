using SqlAccountRestAPI.ViewModels;
namespace SqlAccountRestAPI.Helpers;
public class SqlAccountVersionHelper
{
    private static readonly Dictionary<string, SqlAccountVersionBreakingChanges> _config = new()
    {
        {
            GetRunningVersion(), new SqlAccountVersionBreakingChanges
            {
                SqlAccountVersion = GetRunningVersion(),
                EntityType = "CUSTOMER",
                FunctionCall = "AddPayment",
                AcceptedParams = new List<string> { "DocumentNo", "PaymentMethod", "Project" }
            }
        }
    };
    public static string GetRunningVersion() => "0.0.44";
    public static List<string> GetConfig(string version, string entityType, string functionCall)
    {
        if (_config.TryGetValue(version, out var config) && 
            config.EntityType == entityType && 
            config.FunctionCall == functionCall)
        {
            return config.AcceptedParams;
        }
        return new List<string>();
    }
}
