namespace SqlAccountRestAPI;
public class BizObjectRequest
{
    public IDictionary<string, object?> Data { get; set; } = null!;
}

public class BizObjectUpdateRequest
{
    /// <example>DOCKEY</example>
    public string MainKey { get; set; } = null!;    
    public IDictionary<string, object?> Data { get; set; } = null!;
}

public class BizObjectAddChildrenRequest
{
    public IEnumerable<IDictionary<string, object?>> Data { get; set; } = null!;
}