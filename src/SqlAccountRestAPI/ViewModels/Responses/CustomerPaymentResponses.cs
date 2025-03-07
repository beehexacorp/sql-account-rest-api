namespace SqlAccountRestAPI.ViewModels.Responses;

public class CustomerPaymentResponse
{
    public List<CustomerPayment> CustomerPayments { get; set; } = new()!;
}

public class CustomerPayment
{
    ///<example>1</example>
    public int DOCKEY { get; set; }
    
    ///<example>CS-00001</example>
    public string DOCNO { get; set; } = null!;
    
    ///<example>300-C0002</example>
    public string CODE { get; set; } = null!;
    
    ///<example>2024-06-15T00:00:00</example>
    public DateTime DOCDATE { get; set; }
    
    ///<example>2024-06-15T00:00:00</example>
    public DateTime POSTDATE { get; set; }
    
    ///<example>2024-06-15T00:00:00</example>
    public DateTime TAXDATE { get; set; }
    
    ///<example>Payment For Account</example>
    public string DESCRIPTION { get; set; } = null!;
    
    ///<example>----</example>
    public string AREA { get; set; } = null!;
    
    ///<example>----</example>
    public string AGENT { get; set; } = null!;
    
    ///<example>320-000</example>
    public string PAYMENTMETHOD { get; set; } = null!;
    
    ///<example></example>
    public string CHEQUENUMBER { get; set; } = null!;
    
    ///<example>CASH</example>
    public string JOURNAL { get; set; } = null!;
    
    ///<example>----</example>
    public string PROJECT { get; set; } = null!;
    
    ///<example>----</example>
    public string PAYMENTPROJECT { get; set; } = null!;
    
    ///<example>----</example>
    public string CURRENCYCODE { get; set; } = null!;
    
    ///<example>1</example>
    public decimal CURRENCYRATE { get; set; }
    
    ///<example></example>
    public string BANKACC { get; set; } = null!;
    
    ///<example>0</example>
    public decimal BANKCHARGE { get; set; }
    
    ///<example></example>
    public string BANKCHARGEACCOUNT { get; set; } = null!;
    
    ///<example>240</example>
    public decimal DOCAMT { get; set; }
    
    ///<example>240</example>
    public decimal LOCALDOCAMT { get; set; }
    
    ///<example>0</example>
    public decimal UNAPPLIEDAMT { get; set; }
    
    ///<example></example>
    public string DOCREF1 { get; set; } = null!;
    
    ///<example></example>
    public string DOCREF2 { get; set; } = null!;
    
    ///<example>CS</example>
    public string FROMDOCTYPE { get; set; } = null!;
    
    ///<example></example>
    public string FROMDOCKEY { get; set; } = null!;
    
    ///<example>19</example>
    public int GLTRANSID { get; set; }
    
    ///<example>F</example>
    public string CANCELLED { get; set; } = null!;
    
    ///<example>0</example>
    public int NONREFUNDABLE { get; set; }
    
    ///<example></example>
    public string BOUNCEDDATE { get; set; } = null!;
    
    ///<example>1</example>
    public string UPDATECOUNT { get; set; } = null!;
    
    ///<example></example>
    public string ATTACHMENTS { get; set; } = null!;
    
    ///<example></example>
    public string NOTE { get; set; } = null!;
    
    ///<example>0</example>
    public int BANKTRANSFERTYPE { get; set; }
    
    ///<example></example>
    public string BANKREFNO { get; set; } = null!;
    
    ///<example></example>
    public string BANKSTATUS { get; set; } = null!;
    
    ///<example></example>
    public string BANKSTATUSDESC { get; set; } = null!;
    
    public List<KnockOffDetail> CdsKnockOff { get; set; } = new()!;
}

public class KnockOffDetail
{
    ///<example>1</example>
    public int DOCKEY_1 { get; set; }
    
    ///<example>PM</example>
    public string FROMDOCTYPE_1 { get; set; } = null!;
    
    ///<example>1</example>
    public int FROMDOCKEY_1 { get; set; }
    
    ///<example>IV</example>
    public string TODOCTYPE { get; set; } = null!;
    
    ///<example>66</example>
    public int TODOCKEY { get; set; }
    
    ///<example>240</example>
    public decimal KOAMT { get; set; }
    
    ///<example>240</example>
    public decimal ACTUALLOCALKOAMT { get; set; }
    
    ///<example>240</example>
    public decimal LOCALKOAMT { get; set; }
    
    ///<example>2024-06-15T00:00:00</example>
    public DateTime KOTAXDATE { get; set; }
    
    ///<example>0</example>
    public decimal GAINLOSS { get; set; }
    
    ///<example></example>
    public string GAINLOSSPOSTDATE { get; set; } = null!;
}