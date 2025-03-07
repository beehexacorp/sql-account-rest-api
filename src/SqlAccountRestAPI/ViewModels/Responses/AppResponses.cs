namespace SqlAccountRestAPI.ViewModels.Responses;
public class AppInfoResponse
{
    public SqlAccountAppInfo sqlAccountAppInfo { get; set; } = new();
    public SqlAccountAPIReleaseInfo sqlAccountAPIReleaseInfo { get; set; } = new();
}

public class SqlAccountAppInfo
{
    /// <example>SQL Account Education Edition - Testing Company [2025]</example>
    public string Title { get; set; } = null!;
    /// <example>2024-08-19</example>
    public string ReleaseDate { get; set; } = null!;
    /// <example>854</example>
    public string BuildNo { get; set; } = null!;
    /// <example>5.2024.994.854</example>
    public string Version { get; set; } = null!;
}

public class SqlAccountAPIReleaseInfo
{
    /// <example>release-0.0.44</example>
    public string API_VERSION { get; set; } = null!;
    /// <example>5001</example>
    public string PORT { get; set; } = null!;
    /// <example>SQLACC_API_IIS</example>
    public string APP_NAME { get; set; } = null!;
    /// <example>C:/Users/Admin/Code/SQLAccountPackage</example>
    public string APP_DIR { get; set; } = null!;
    /// <example>IIS</example>
    public string DEPLOYMENT_METHOD { get; set; } = null!;
    /// <example>SQLACC_API_IISPool</example>
    public string APP_POOL_NAME { get; set; } = null!;
    /// <example>release-0.0.46</example>
    public string LATEST_VERSION { get; set; } = null!;
}
public class AppActionsResponse
{
    public List<string> ActionNames { get; set; } = null!;
}
public class AppModulesResponse
{
    public List<string> ModuleNames { get; set; } = null!;
}
public class AppBizObjectsResponse
{
    public List<string> ObjectNames { get; set; } = null!;
}
public class AppBizObjectInfoResponse
{
    /// <example>AR_PM</example>
    public string Name { get; set; } = null!;
    public List<BizObjectInfoDataset> Datasets { get; set; } = new()!;
}
public class BizObjectInfoDataset
{
    /// <example>MainDataSet</example>
    public string Name { get; set; } = null!;
    public List<string> Fields { get; set; } = new()!;
}
public class BizObjectInfoFields
{
    public List<string> Fields { get; set; } = null!;
}
public class AppUpdateResponse
{
    /// <example>Update process starated. IIS will restart in 10 seconds. Please close every swagger window.</example>
    public string Status { get; set; } = null!;
}