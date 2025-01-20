using System;
using System.Text.Json.Nodes;
using System.Xml;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using SqlAccountRestAPI.Core;
using System.Web;

namespace SqlAccountRestAPI.Helpers;

public class SqlAccountStockItemTemplateHelper
{
    private SqlAccountORM _microORM;
    public SqlAccountStockItemTemplateHelper(SqlAccountORM microORM)
    {
        _microORM = microORM;
    }

    public IEnumerable<IDictionary<string, object>> GetByCode(string code, int limit, int offset){
        var mainFields = _microORM.GetFields("ST_ITEM_TPL", limit, offset).Distinct().ToHashSet(); //app.ComServer.DBManager.NewDataSet("SELECT * FROM AR_CUSTOMER").Fields;
        code = HttpUtility.UrlDecode(code);
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM ST_ITEM_TPL
    WHERE ST_ITEM_TPL.CODE ='{code}'
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) ST_ITEM_TPL_LIMIT
LEFT JOIN ST_ITEM_TPLDTL 
    ON ST_ITEM_TPL_LIMIT.CODE = ST_ITEM_TPLDTL.CODE 
";
           
        return _microORM.GroupQuery(sql, mainFields, "CODE", "cdsItemTplDtl", 0, offset);
    }
    public IEnumerable<IDictionary<string, object>> GetFromDaysAgo(int days, int limit, int offset){
        var mainFields = _microORM.GetFields("ST_ITEM_TPL", limit, offset).Distinct().ToHashSet(); 
        
        var currentUnixTime = new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeSeconds();
        var convertedUnixTime = currentUnixTime - (days * 86400);
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM ST_ITEM_TPL
    WHERE ST_ITEM_TPL.LASTMODIFIED >= {convertedUnixTime}
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) ST_ITEM_TPL_LIMIT
LEFT JOIN ST_ITEM_TPLDTL 
    ON ST_ITEM_TPL_LIMIT.CODE = ST_ITEM_TPLDTL.CODE 
";
        
           
        return _microORM.GroupQuery(sql, mainFields, "CODE", "cdsItemTplDtl", 0, offset);
    }
    public IEnumerable<IDictionary<string, object>> GetFromDate(string date, int limit, int offset){
        var mainFields = _microORM.GetFields("ST_ITEM_TPL", limit, offset).Distinct().ToHashSet(); 

        DateTime.TryParse(date, out var parsedDate);
        var convertedUnixTime = new DateTimeOffset(parsedDate).ToUnixTimeSeconds();
        
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM ST_ITEM_TPL
    WHERE ST_ITEM_TPL.LASTMODIFIED >= {convertedUnixTime}
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) ST_ITEM_TPL_LIMIT
LEFT JOIN ST_ITEM_TPLDTL 
    ON ST_ITEM_TPL_LIMIT.CODE = ST_ITEM_TPLDTL.CODE 
";
        
           
        return _microORM.GroupQuery(sql, mainFields, "CODE", "cdsItemTplDtl", 0, offset);
    }
}