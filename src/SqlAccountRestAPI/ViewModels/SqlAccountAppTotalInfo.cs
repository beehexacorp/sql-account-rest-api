using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlAccountRestAPI.ViewModels;
public class SqlAccountAppInfo
{
    public string Title { get; set; } = null!;
    public string ReleaseDate { get; set; } = null!;
    public string BuildNo { get; set; } = null!;
    public string Version { get; set; } = null!;
}
public class SqlAccountTotalInfo
{
    public SqlAccountAppInfo sqlAccountAppInfo { get; set; } = new SqlAccountAppInfo();
    public IDictionary<string, object> releaseInfo { get; set; } = null!;
}
