namespace SqlAccountRestAPI.ViewModels.Responses;
public class SalesOrderResponse
{
    public List<SalesOrder> SalesOrders { get; set; } = new()!;
}
public class SalesOrder
{
    ///<example>1</example>
    public int DOCKEY { get; set; }
    ///<example>SO-00001</example>
    public string DOCNO { get; set; } = null!;
    ///<example></example>
    public string DOCNOEX { get; set; } = null!;
    ///<example>2024-06-28T00</example>
    public DateTime DOCDATE { get; set; }
    ///<example>2024-06-28T00</example>
    public DateTime POSTDATE { get; set; }
    ///<example>2024-06-28T00</example>
    public DateTime TAXDATE { get; set; }
    ///<example>300-A0001</example>
    public string CODE { get; set; } = null!;
    ///<example>A HOME FURNISHING &amp; SERVICE</example>
    public string COMPANYNAME { get; set; } = null!;
    ///<example>Lot 98, Jalan Mentari Kuah,</example>
    public string ADDRESS1 { get; set; } = null!;
    ///<example>50480 KL</example>
    public string ADDRESS2 { get; set; } = null!;
    ///<example></example>
    public string ADDRESS3 { get; set; } = null!;
    ///<example></example>
    public string ADDRESS4 { get; set; } = null!;
    ///<example></example>
    public string POSTCODE { get; set; } = null!;
    ///<example></example>
    public string CITY { get; set; } = null!;
    ///<example></example>
    public string STATE { get; set; } = null!;
    ///<example></example>
    public string COUNTRY { get; set; } = null!;
    ///<example>04-976 5235</example>
    public string PHONE1 { get; set; } = null!;
    ///<example></example>
    public string MOBILE { get; set; } = null!;
    ///<example></example>
    public string FAX1 { get; set; } = null!;
    ///<example>Mr. Delta</example>
    public string ATTENTION { get; set; } = null!;
    ///<example>KL</example>
    public string AREA { get; set; } = null!;
    ///<example>Yuki</example>
    public string AGENT { get; set; } = null!;
    ///<example>----</example>
    public string PROJECT { get; set; } = null!;
    ///<example>30 Days</example>
    public string TERMS { get; set; } = null!;
    ///<example>----</example>
    public string CURRENCYCODE { get; set; } = null!;
    ///<example>1</example>
    public decimal CURRENCYRATE { get; set; }
    ///<example>----</example>
    public string SHIPPER { get; set; } = null!;
    ///<example>Sales Order</example>
    public string DESCRIPTION { get; set; } = null!;
    ///<example>F</example>
    public string CANCELLED { get; set; } = null!;
    ///<example>1370</example>
    public decimal DOCAMT { get; set; }
    ///<example>1370</example>
    public decimal LOCALDOCAMT { get; set; }
    ///<example></example>
    public string D_DOCNO { get; set; } = null!;
    ///<example></example>
    public string D_PAYMENTMETHOD { get; set; } = null!;
    ///<example></example>
    public string D_CHEQUENUMBER { get; set; } = null!;
    ///<example></example>
    public string D_PAYMENTPROJECT { get; set; } = null!;
    ///<example>0</example>
    public decimal D_BANKCHARGE { get; set; }
    ///<example></example>
    public string D_BANKCHARGEACCOUNT { get; set; } = null!;
    ///<example>0</example>
    public decimal D_AMOUNT { get; set; }
    ///<example></example>
    public string VALIDITY { get; set; } = null!;
    ///<example></example>
    public string DELIVERYTERM { get; set; } = null!;
    ///<example></example>
    public string CC { get; set; } = null!;
    ///<example></example>
    public string DOCREF1 { get; set; } = null!;
    ///<example></example>
    public string DOCREF2 { get; set; } = null!;
    ///<example></example>
    public string DOCREF3 { get; set; } = null!;
    ///<example></example>
    public string DOCREF4 { get; set; } = null!;
    ///<example>BILLING</example>
    public string BRANCHNAME { get; set; } = null!;
    ///<example>Lot 98, Jalan Mentari Kuah,</example>
    public string DADDRESS1 { get; set; } = null!;
    ///<example>50480 KL</example>
    public string DADDRESS2 { get; set; } = null!;
    ///<example></example>
    public string DADDRESS3 { get; set; } = null!;
    ///<example></example>
    public string DADDRESS4 { get; set; } = null!;
    ///<example></example>
    public string DPOSTCODE { get; set; } = null!;
    ///<example></example>
    public string DCITY { get; set; } = null!;
    ///<example></example>
    public string DSTATE { get; set; } = null!;
    ///<example></example>
    public string DCOUNTRY { get; set; } = null!;
    ///<example>Mr. Delta</example>
    public string DATTENTION { get; set; } = null!;
    ///<example>04-976 5235</example>
    public string DPHONE1 { get; set; } = null!;
    ///<example></example>
    public string DMOBILE { get; set; } = null!;
    ///<example></example>
    public string DFAX1 { get; set; } = null!;
    ///<example></example>
    public string TAXEXEMPTNO { get; set; } = null!;
    ///<example></example>
    public string SALESTAXNO { get; set; } = null!;
    ///<example></example>
    public string SERVICETAXNO { get; set; } = null!;
    ///<example></example>
    public string TIN { get; set; } = null!;
    ///<example>0</example>
    public int IDTYPE { get; set; }
    ///<example></example>
    public string IDNO { get; set; } = null!;
    ///<example></example>
    public string TOURISMNO { get; set; } = null!;
    ///<example></example>
    public string SIC { get; set; } = null!;
    ///<example></example>
    public string INCOTERMS { get; set; } = null!;
    ///<example>0</example>
    public int SUBMISSIONTYPE { get; set; }
    ///<example></example>
    public string ATTACHMENTS { get; set; } = null!;
    ///<example></example>
    public string NOTE { get; set; } = null!;
    ///<example>T</example>
    public string TRANSFERABLE { get; set; } = null!;
    ///<example></example>
    public string UPDATECOUNT { get; set; } = null!;
    ///<example>0</example>
    public int PRINTCOUNT { get; set; }

    public List<SalesOrderDetail> cdsDocDetail { get; set; } = null!;
}

public class SalesOrderDetail
{
    ///<example>3</example>
    public int DTLKEY { get; set; }
    ///<example>1</example>
    public int DOCKEY_1 { get; set; }
    ///<example>1000</example>
    public int SEQ { get; set; }
    ///<example></example>
    public string STYLEID { get; set; } = null!;
    ///<example></example>
    public string NUMBER { get; set; } = null!;
    ///<example>CCE/Black-Chair</example>
    public string ITEMCODE { get; set; } = null!;
    ///<example>----</example>
    public string LOCATION { get; set; } = null!;
    ///<example></example>
    public string BATCH { get; set; } = null!;
    ///<example>----</example>
    public string PROJECT_1 { get; set; } = null!;
    ///<example>Creative Curvy Modern Style Eames Chair (Black)</example>
    public string DESCRIPTION_1 { get; set; } = null!;
    ///<example></example>
    public string DESCRIPTION2 { get; set; } = null!;
    ///<example></example>
    public string DESCRIPTION3 { get; set; } = null!;
    ///<example></example>
    public string PERMITNO { get; set; } = null!;
    ///<example>2</example>
    public decimal QTY { get; set; }
    ///<example>UNIT</example>
    public string UOM { get; set; } = null!;
    ///<example>1</example>
    public decimal RATE { get; set; }
    ///<example>2</example>
    public decimal SQTY { get; set; }
    ///<example>0</example>
    public decimal SUOMQTY { get; set; }
    ///<example>0</example>
    public decimal OFFSETQTY { get; set; }
    ///<example>85</example>
    public decimal UNITPRICE { get; set; }
    ///<example>2024-06-28T00</example>
    public DateTime DELIVERYDATE { get; set; }
    ///<example></example>
    public string DISC { get; set; } = null!;
    ///<example></example>
    public string TAX { get; set; } = null!;
    ///<example></example>
    public string TARIFF { get; set; } = null!;
    ///<example></example>
    public string TAXEXEMPTIONREASON { get; set; } = null!;
    ///<example>022</example>
    public string IRBM_CLASSIFICATION { get; set; } = null!;
    ///<example></example>
    public string TAXRATE { get; set; } = null!;
    ///<example>0</example>
    public decimal TAXAMT { get; set; }
    ///<example>0</example>
    public decimal LOCALTAXAMT { get; set; }
    ///<example>0</example>
    public int TAXINCLUSIVE { get; set; }
    ///<example>170</example>
    public decimal AMOUNT { get; set; }
    ///<example>170</example>
    public decimal LOCALAMOUNT { get; set; }
    ///<example>T</example>
    public string PRINTABLE { get; set; } = null!;
    ///<example>QT</example>
    public string FROMDOCTYPE { get; set; } = null!;
    ///<example>1</example>
    public string FROMDOCKEY { get; set; } = null!;
    ///<example>3</example>
    public string FROMDTLKEY { get; set; } = null!;
    ///<example>T</example>
    public string TRANSFERABLE_1 { get; set; } = null!;
    ///<example></example>
    public string REMARK1 { get; set; } = null!;
    ///<example></example>
    public string REMARK2 { get; set; } = null!;
    ///<example>0</example>
    public decimal INITIALPURCHASECOST { get; set; }
}