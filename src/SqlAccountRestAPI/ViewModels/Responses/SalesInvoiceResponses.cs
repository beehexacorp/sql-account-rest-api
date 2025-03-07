namespace SqlAccountRestAPI.ViewModels.Responses;
public class SalesInvoiceResponse
{
    public List<SalesInvoice> SalesInvoices { get; set; } = new()!;
}
public class SalesInvoice
{
    ///<example>1</example>
    public int DOCKEY { get; set; }
    ///<example>IV-00001</example>
    public string DOCNO { get; set; } = null!;
    ///<example></example>
    public string DOCNOEX { get; set; } = null!;
    ///<example>2024-06-11T00</example>
    public DateTime DOCDATE { get; set; }
    ///<example>2024-06-11T00</example>
    public DateTime POSTDATE { get; set; }
    ///<example>2024-06-11T00</example>
    public DateTime TAXDATE { get; set; }
    ///<example></example>
    public string EIVDATETIME { get; set; } = null!;
    ///<example>300-T0001</example>
    public string CODE { get; set; } = null!;
    ///<example>THAI TEA TRADING</example>
    public string COMPANYNAME { get; set; } = null!;
    ///<example>No 45, Jalan Batu Tiga Klang,</example>
    public string ADDRESS1 { get; set; } = null!;
    ///<example>42000, Pelabuhan Klang, Selangor.</example>
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
    ///<example>03-3816 1027</example>
    public string PHONE1 { get; set; } = null!;
    ///<example></example>
    public string MOBILE { get; set; } = null!;
    ///<example></example>
    public string FAX1 { get; set; } = null!;
    ///<example>Mr. Leonard</example>
    public string ATTENTION { get; set; } = null!;
    ///<example>Selangor</example>
    public string AREA { get; set; } = null!;
    ///<example>Yuki</example>
    public string AGENT { get; set; } = null!;
    ///<example>----</example>
    public string PROJECT { get; set; } = null!;
    ///<example>60 Days</example>
    public string TERMS { get; set; } = null!;
    ///<example>----</example>
    public string CURRENCYCODE { get; set; } = null!;
    ///<example>1</example>
    public string CURRENCYRATE { get; set; } = null!;
    ///<example>----</example>
    public string SHIPPER { get; set; } = null!;
    ///<example>Sales</example>
    public string DESCRIPTION { get; set; } = null!;
    ///<example>F</example>
    public string CANCELLED { get; set; } = null!;
    ///<example>800</example>
    public string DOCAMT { get; set; } = null!;
    ///<example>800</example>
    public string LOCALDOCAMT { get; set; } = null!;
    ///<example>0</example>
    public string D_AMOUNT { get; set; } = null!;
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
    ///<example>No 45, Jalan Batu Tiga Klang,</example>
    public string DADDRESS1 { get; set; } = null!;
    ///<example>42000, Pelabuhan Klang, Selangor.</example>
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
    ///<example>Mr. Leonard</example>
    public string DATTENTION { get; set; } = null!;
    ///<example>03-3816 1027</example>
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
    public string IDTYPE { get; set; } = null!;
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
    ///<example>0</example>
    public int IRBM_STATUS { get; set; }
    ///<example></example>
    public string IRBM_INTERNALID { get; set; } = null!;
    ///<example></example>
    public string IRBM_UUID { get; set; } = null!;
    ///<example></example>
    public string IRBM_LONGID { get; set; } = null!;
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

    public List<SaleInvoiceDetail> CdsDocDetail { get; set; } = new()!;
}

public class SaleInvoiceDetail
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
    ///<example>RFC/Blue</example>
    public string ITEMCODE { get; set; } = null!;
    ///<example>----</example>
    public string LOCATION { get; set; } = null!;
    ///<example></example>
    public string BATCH { get; set; } = null!;
    ///<example>----</example>
    public string PROJECT_1 { get; set; } = null!;
    ///<example>Ultra Soft Fluffy Rug Rectangle Carpet (Blue)</example>
    public string DESCRIPTION_1 { get; set; } = null!;
    ///<example></example>
    public string DESCRIPTION2 { get; set; } = null!;
    ///<example></example>
    public string DESCRIPTION3 { get; set; } = null!;
    ///<example></example>
    public string PERMITNO { get; set; } = null!;
    ///<example>10</example>
    public decimal QTY { get; set; }
    ///<example>UNIT</example>
    public string UOM { get; set; } = null!;
    ///<example>1</example>
    public decimal RATE { get; set; }
    ///<example>10</example>
    public decimal SQTY { get; set; }
    ///<example>0</example>
    public decimal SUOMQTY { get; set; }
    ///<example>40</example>
    public decimal UNITPRICE { get; set; }
    ///<example>2024-06-11T00</example>
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
    ///<example>400</example>
    public decimal AMOUNT { get; set; }
    ///<example>400</example>
    public decimal LOCALAMOUNT { get; set; }
    ///<example>400</example>
    public decimal TAXABLEAMT { get; set; }
    ///<example>500-500</example>
    public string ACCOUNT { get; set; } = null!;
    ///<example>T</example>
    public string PRINTABLE { get; set; } = null!;
    ///<example></example>
    public string FROMDOCTYPE { get; set; } = null!;
    ///<example></example>
    public string FROMDOCKEY { get; set; } = null!;
    ///<example></example>
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