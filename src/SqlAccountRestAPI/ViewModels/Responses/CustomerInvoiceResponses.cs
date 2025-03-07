namespace SqlAccountRestAPI.ViewModels.Responses;

using Swashbuckle.AspNetCore.Annotations;

public class CustomerInvoiceResponse
{
    public List<CustomerInvoice> CustomerInvoices { get; set; } = new();
}

public class CustomerInvoice
{
    /// <example>1</example>
    public int DOCKEY { get; set; }

    /// <example>IV-00001</example>
    public string DOCNO { get; set; } = null!;

    /// <example></example>
    public string DOCNOEX { get; set; } = null!;

    /// <example>300-T0001</example>
    public string CODE { get; set; } = null!;

    /// <example>SALES</example>
    public string JOURNAL { get; set; } = null!;

    /// <example>2024-06-11T00:00:00</example>
    public DateTime DOCDATE { get; set; }

    /// <example>2024-06-11T00:00:00</example>
    public DateTime POSTDATE { get; set; }

    /// <example>2024-06-11T00:00:00</example>
    public DateTime TAXDATE { get; set; }

    /// <example>60 Days</example>
    public string TERMS { get; set; } = null!;

    /// <example>2024-08-10T00:00:00</example>
    public DateTime DUEDATE { get; set; }

    /// <example>Sales</example>
    public string DESCRIPTION { get; set; } = null!;

    /// <example>Selangor</example>
    public string AREA { get; set; } = null!;

    /// <example>Yuki</example>
    public string AGENT { get; set; } = null!;

    /// <example>----</example>
    public string PROJECT { get; set; } = null!;

    /// <example>----</example>
    public string CURRENCYCODE { get; set; } = null!;

    /// <example>1</example>
    public decimal CURRENCYRATE { get; set; }

    /// <example>800</example>
    public decimal DOCAMT { get; set; }

    /// <example>800</example>
    public decimal LOCALDOCAMT { get; set; }

    /// <example>800</example>
    public decimal InvoiceAMT { get; set; }

    /// <example>IV</example>
    public string FROMDOCTYPE { get; set; } = null!;

    /// <example></example>
    public string TAXEXEMPTNO { get; set; } = null!;

    /// <example>1</example>
    public int GLTRANSID { get; set; }

    /// <example>F</example>
    public string CANCELLED { get; set; } = null!;

    /// <example></example>
    public string UPDATECOUNT { get; set; } = null!;

    /// <example></example>
    public string ATTACHMENTS { get; set; } = null!;

    /// <example></example>
    public string NOTE { get; set; } = null!;

    public List<CustomerInvoiceDetail> CdsDocDetail { get; set; } = new();
}

public class CustomerInvoiceDetail
{
    /// <example>3</example>
    public int DTLKEY { get; set; }

    /// <example>1</example>
    public int DOCKEY_1 { get; set; }

    /// <example>1000</example>
    public int SEQ { get; set; }

    /// <example>----</example>
    public string PROJECT_1 { get; set; } = null!;

    /// <example>500-500</example>
    public string ACCOUNT { get; set; } = null!;

    /// <example>Ultra Soft Fluffy Rug Rectangle Carpet (Blue)</example>
    public string DESCRIPTION_1 { get; set; } = null!;

    /// <example></example>
    public string TAX { get; set; } = null!;

    /// <example></example>
    public string TARIFF { get; set; } = null!;

    /// <example></example>
    public string TAXRATE { get; set; } = null!;

    /// <example>0</example>
    public decimal TAXAMT { get; set; }

    /// <example>0</example>
    public decimal LOCALTAXAMT { get; set; }

    /// <example>0</example>
    public int TAXINCLUSIVE { get; set; }

    /// <example>400</example>
    public decimal AMOUNT { get; set; }

    /// <example>400</example>
    public decimal LOCALAMOUNT { get; set; }

    /// <example>400</example>
    public decimal TAXABLEAMT { get; set; }

    /// <example>3</example>
    public int FROMDTLKEY { get; set; }
}
