namespace SqlAccountRestAPI.Controllers;
public class BizObjectTransferRequest
{
    /// <example>SL_SO</example>
    public string FromEntityType { get; set; } = null!;
    /// <example>SL_IV</example>
    public string ToEntityType { get; set; } = null!;
    /// <example>SO-00001</example>
    public string DocNo { get; set; } = null!;
}
