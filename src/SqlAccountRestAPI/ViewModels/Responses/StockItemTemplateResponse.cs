namespace SqlAccountRestAPI.ViewModels.Responses;

public class StockItemTemplateResponse
{
    public List<StockItemTemplate> StockItemTemplates { get; set; } = new()!;
}

public class StockItemTemplate
{
    ///<example>NewStockItem</example>
    public string Code { get; set; } = null!;
    ///<example></example>
    public string Description { get; set; } = null!;
    ///<example></example>
    public string Description2 { get; set; } = null!;
    ///<example></example>
    public string Description3 { get; set; } = null!;
    ///<example></example>
    public decimal RefPrice { get; set; }
    ///<example>T</example>
    public string IsActive { get; set; } = null!;
    ///<example></example>
    public string Attachments { get; set; } = null!;
    ///<example>1741233335</example>
    public long LastModified { get; set; }
    public List<ItemTemplateDetail> CdsItemTplDtl { get; set; } = new();
}

public class ItemTemplateDetail
{
    ///<example>1</example>
    public string DtlKey { get; set; } = null!;
    ///<example>NewStockItem</example>
    public string Code1 { get; set; } = null!;
    ///<example>1000</example>
    public int Seq { get; set; }
    ///<example></example>
    public string StyleId { get; set; } = null!;
    ///<example></example>
    public string Number { get; set; } = null!;
    ///<example>BOM-01/Black</example>
    public string ItemCode { get; set; } = null!;
    ///<example>----</example>
    public string Location { get; set; } = null!;
    ///<example>----</example>
    public string Project { get; set; } = null!;
    ///<example>Premium PU Leather Ultimate Gaming Chair (Black)</example>
    public string Description1 { get; set; } = null!;
    ///<example></example>
    public string Description2_1 { get; set; } = null!;
    ///<example></example>
    public string Description3_1 { get; set; } = null!;
    ///<example>1</example>
    public decimal Qty { get; set; }
    ///<example>0</example>
    public decimal SuomQty { get; set; }
    ///<example>UNIT</example>
    public string Uom { get; set; } = null!;
    ///<example>700</example>
    public decimal UnitAmount { get; set; }
    ///<example></example>
    public decimal Disc { get; set; }
    ///<example>700</example>
    public decimal Amount { get; set; }
    ///<example>T</example>
    public string Printable { get; set; } = null!;
    ///<example></example>
    public string Remark1 { get; set; } = null!;
    ///<example></example>
    public string Remark2 { get; set; } = null!;
}