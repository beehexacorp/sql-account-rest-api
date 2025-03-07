namespace SqlAccountRestAPI.ViewModels.Responses;
public class StockItemResponse
{
    public List<StockItem> StockItems { get; set; } = new()!;
}
public class StockItem
{
    ///<example>1</example>
    public int DockKey { get; set; }
    ///<example>SEMI BOM</example>
    public string Code { get; set; } = null!;
    ///<example>SEMI BOM</example>
    public string Description { get; set; } = null!;
    ///<example></example>
    public string Description2 { get; set; } = null!;
    ///<example></example>
    public string Description3 { get; set; } = null!;
    ///<example>OTHERS</example>
    public string StockGroup { get; set; } = null!;
    ///<example>T</example>
    public string StockControl { get; set; } = null!;
    ///<example>1</example>
    public int CostingMethod { get; set; }
    ///<example>F</example>
    public string SerialNumber { get; set; } = null!;
    ///<example></example>
    public string Remark1 { get; set; } = null!;
    ///<example></example>
    public string Remark2 { get; set; } = null!;
    ///<example>0</example>
    public int MinQty { get; set; }
    ///<example>0</example>
    public int MaxQty { get; set; }
    ///<example>0</example>
    public int ReorderLevel { get; set; }
    ///<example>1</example>
    public int ReorderQty { get; set; }
    ///<example></example>
    public string Shelf { get; set; } = null!;
    ///<example></example>
    public string Suom { get; set; } = null!;
    ///<example>B</example>
    public string ItemType { get; set; } = null!;
    ///<example>0</example>
    public int LeadTime { get; set; }
    ///<example>0</example>
    public int BomLeadTime { get; set; }
    ///<example>0</example>
    public decimal BomAsmCost { get; set; }
    ///<example></example>
    public string SlTax { get; set; } = null!;
    ///<example></example>
    public string PhTax { get; set; } = null!;
    ///<example></example>
    public string Tariff { get; set; } = null!;
    ///<example>022</example>
    public string IrbmClassification { get; set; } = null!;
    ///<example></example>
    public string StockMatrix { get; set; } = null!;
    ///<example></example>
    public string DefUomSt { get; set; } = null!;
    ///<example></example>
    public string DefUomSl { get; set; } = null!;
    ///<example></example>
    public string DefUomPh { get; set; } = null!;
    ///<example></example>
    public string ScriptCode { get; set; } = null!;
    ///<example>T</example>
    public string IsActive { get; set; } = null!;
    ///<example>-5</example>
    public int BalSQty { get; set; }
    ///<example>0</example>
    public int BalSuomQty { get; set; }
    ///<example>2024-08-08T00</example>
    public DateTime CreationDate { get; set; }
    ///<example></example>
    public string Picture { get; set; } = null!;
    ///<example></example>
    public string PictureClass { get; set; } = null!;
    ///<example></example>
    public string Attachments { get; set; } = null!;
    ///<example></example>
    public string Note { get; set; } = null!;
    ///<example>1723112180</example>
    public long LastModified { get; set; }
    public List<UOM> CdsUOM { get; set; } = null!;
}

public class UOM
{
    ///<example>SEMI BOM</example>
    public string Code1 { get; set; } = null!;
    ///<example>UNIT</example>
    public string Uom { get; set; } = null!;
    ///<example>1</example>
    public decimal Rate { get; set; }
    ///<example>23</example>
    public decimal RefCost { get; set; }
    ///<example>0</example>
    public decimal RefPrice { get; set; }
    ///<example></example>
    public string MinCost { get; set; } = null!;
    ///<example></example>
    public string MaxCost { get; set; } = null!;
    ///<example></example>
    public string MinPrice { get; set; } = null!;
    ///<example></example>
    public string MaxPrice { get; set; } = null!;
    ///<example>1</example>
    public int IsBase { get; set; }

}
