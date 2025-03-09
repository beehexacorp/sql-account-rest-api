namespace SqlAccountRestAPI.Controllers;
public class BizObjectQueryRequest
{
    /// <example>SELECT * FROM AR_CUSTOMER</example>
    public string Sql { get; set; } = null!;
    /// <example>0</example>
    public int Offset { get; set; } = 0;
    /// <example>1</example>
    public int Limit { get; set; } = 100;
}