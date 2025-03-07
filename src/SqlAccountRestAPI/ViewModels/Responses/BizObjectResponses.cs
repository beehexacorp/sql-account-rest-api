namespace SqlAccountRestAPI.ViewModels.Responses;

public class BizObjectQueryResponse
{
    public List<BizObjectData> BizData { get; set; } = null!;
}
public class BizObjectData
{
    public Dictionary<string, object> Fields { get; set; } = null!;
}
public class BizObjectGeneralResponse
{
    public Dictionary<string, object> Fields { get; set; } = null!;
}