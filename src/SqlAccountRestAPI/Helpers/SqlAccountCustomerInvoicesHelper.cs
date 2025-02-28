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

public class SqlAccountCustomerInvoiceHelper
{
    private SqlAccountORM _microORM;
    public SqlAccountCustomerInvoiceHelper(SqlAccountORM microORM)
    {
        _microORM = microORM;
    }

    public IEnumerable<IDictionary<string, object>> GetByDocno(string documentNumber, int limit, int offset){
        var mainFields = _microORM.GetFields("AR_IV", limit, offset).Distinct().ToHashSet(); 
        documentNumber = HttpUtility.UrlDecode(documentNumber);
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM AR_IV
    WHERE AR_IV.DOCNO ='{documentNumber}'
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) AR_IV_LIMIT
LEFT JOIN AR_IVDTL 
    ON AR_IV_LIMIT.DOCKEY = AR_IVDTL.DOCKEY 
";
           
        return _microORM.GroupQuery(sql, mainFields, "DOCKEY", "cdsDocDetail", 0, offset);
    }
    public IEnumerable<IDictionary<string, object>> GetFromDaysAgo(int days, int limit, int offset){
        var mainFields = _microORM.GetFields("AR_IV", limit, offset).Distinct().ToHashSet(); 

        var date = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
        
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM AR_IV
    WHERE AR_IV.DOCDATE >= '{date}'
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) AR_IV_LIMIT
LEFT JOIN AR_IVDTL 
    ON AR_IV_LIMIT.DOCKEY = AR_IVDTL.DOCKEY 
";
        
           
        return _microORM.GroupQuery(sql, mainFields, "DOCKEY", "cdsDocDetail", 0, offset);
    }
    public IEnumerable<IDictionary<string, object>> GetFromDate(string date, int limit, int offset){
        var mainFields = _microORM.GetFields("AR_IV", limit, offset).Distinct().ToHashSet(); 
        
        var sql = $@"SELECT * 
FROM (
    SELECT *
    FROM AR_IV
    WHERE AR_IV.DOCDATE >= '{date}'
    OFFSET {offset} ROWS
    FETCH NEXT {limit} ROWS ONLY
) AR_IV_LIMIT
LEFT JOIN AR_IVDTL ON AR_IV_LIMIT.DOCKEY = AR_IVDTL.DOCKEY 
";
        
           
        return _microORM.GroupQuery(sql, mainFields, "DOCKEY", "cdsDocDetail", 0, offset);
    }
}
