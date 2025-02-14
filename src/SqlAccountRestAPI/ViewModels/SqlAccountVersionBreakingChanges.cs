namespace SqlAccountRestAPI.ViewModels;
public class SqlAccountVersionBreakingChanges
{
    public string SqlAccountVersion { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string FunctionCall { get; set; } = "";
    public List<string> AcceptedParams { get; set; } = new List<string>();
}