namespace SqlAccountRestAPI.ViewModels.Responses;
public class StockAdjustmentResponse
{
    public List<StockAdjustment> StockAdjustments { get; set; } = new()!;
}
public class StockAdjustment
{
    ///<example>1</example>
    public int Dockey { get; set; }
    ///<example>AJ-00001</example>
    public string DocNo { get; set; } = null!;
    ///<example>2024-06-30T00</example>
    public DateTime DocDate { get; set; }
    ///<example>2024-06-30T00</example>
    public DateTime PostDate { get; set; }
    ///<example>Stock Adjustment - Bedding Accessories</example>
    public string Description { get; set; } = null!;
    ///<example>F</example>
    public bool WriteOff { get; set; }
    ///<example>F</example>
    public bool Cancelled { get; set; }
    ///<example>-1170</example>
    public decimal DocAmt { get; set; }
    ///<example></example>
    public string Attachments { get; set; } = null!;
    ///<example></example>
    public string AuthBy { get; set; } = null!;
    ///<example></example>
    public string Reason { get; set; } = null!;
    ///<example></example>
    public string Remark { get; set; } = null!;
    ///<example></example>
    public string Note { get; set; } = null!;
    ///<example></example>
    public string UpdateCount { get; set; } = null!;
    ///<example>0</example>
    public int PrintCount { get; set; }
    public List<StockAdjustmentDetail> CdsDocDetail { get; set; } = null!;
}

public class StockAdjustmentDetail
{
    ///<example>3</example>
    public int DtlKey { get; set; }
    ///<example>1</example>
    public int Dockey_1 { get; set; }
    ///<example>1000</example>
    public int Seq { get; set; }
    ///<example></example>
    public string StyleId { get; set; } = null!;
    ///<example></example>
    public string Number { get; set; } = null!;
    ///<example>MF-Bolster</example>
    public string ItemCode { get; set; } = null!;
    ///<example>----</example>
    public string Location { get; set; } = null!;
    ///<example></example>
    public string Batch { get; set; } = null!;
    ///<example>----</example>
    public string Project { get; set; } = null!;
    ///<example>Microfiber Soft Bolster with 100% cotton fabric</example>
    public string Description1 { get; set; } = null!;
    ///<example></example>
    public string Description2 { get; set; } = null!;
    ///<example></example>
    public string Description3 { get; set; } = null!;
    ///<example>30</example>
    public string BookQty { get; set; } = null!;
    ///<example>29</example>
    public string PhysicalQty { get; set; } = null!;
    ///<example>-1</example>
    public int Qty { get; set; }
    ///<example>0</example>
    public int SuomQty { get; set; }
    ///<example>UNIT</example>
    public string Uom { get; set; } = null!;
    ///<example>1</example>
    public decimal Rate { get; set; }
    ///<example>-1</example>
    public int SQty { get; set; }
    ///<example>129</example>
    public decimal UnitCost { get; set; }
    ///<example>-129</example>
    public decimal Amount { get; set; }
    ///<example>T</example>
    public bool Printable { get; set; }
    ///<example></example>
    public string Remark1 { get; set; } = null!;
    ///<example></example>
    public string Remark2 { get; set; } = null!;

}
