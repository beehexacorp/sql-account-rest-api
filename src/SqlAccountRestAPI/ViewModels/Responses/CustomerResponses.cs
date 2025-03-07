namespace SqlAccountRestAPI.ViewModels.Responses;

public class CustomerResponse
{
    public List<Customer> Customers { get; set; } = new()!;
}

public class Customer
{
    /// <example>300-A0001</example>
    public string Code { get; set; } = null!;
    /// <example>300-000</example>
    public string ControlAccount { get; set; } = null!;
    /// <example>A HOME FURNISHING &amp; SERVICE</example>
    public string CompanyName { get; set; } = null!;
    /// <example></example>
    public string CompanyName2 { get; set; } = null!;
    /// <example>----</example>
    public string CompanyCategory { get; set; } = null!;
    /// <example>KL</example>
    public string Area { get; set; } = null!;
    /// <example>Yuki</example>
    public string Agent { get; set; } = null!;
    ///<example></example>
    public string BizNature { get; set; } = null!;
    ///<example>30 Days</example>
    public string CreditTerm { get; set; } = null!;
    ///<example>30000</example>
    public string CreditLimit { get; set; } = null!;
    ///<example>0</example>
    public string OverdueLimit { get; set; } = null!;
    ///<example>O</example>
    public string StatementType { get; set; } = null!;
    ///<example>----</example>
    public string CurrencyCode { get; set; } = null!;
    ///<example>1455</example>
    public string Outstanding { get; set; } = null!;
    ///<example>T</example>
    public string AllowExceedCreditLimit { get; set; } = null!;
    ///<example>T</example>
    public string AddPdcToCrLimit { get; set; } = null!;
    ///<example>I</example>
    public string AgingOn { get; set; } = null!;
    ///<example>A</example>
    public string Status { get; set; } = null!;
    ///<example></example>
    public string PriceTag { get; set; } = null!;
    ///<example>2023-02-25T00</example>
    public DateTime CreationDate { get; set; }
    ///<example></example>
    public string Tax { get; set; } = null!;
    ///<example></example>
    public string TaxExemptNo { get; set; } = null!;
    ///<example></example>
    public string TaxExpDate { get; set; } = null!;
    ///<example></example>
    public string Brn { get; set; } = null!;
    ///<example></example>
    public string Brn2 { get; set; } = null!;
    ///<example></example>
    public string GstNo { get; set; } = null!;
    ///<example></example>
    public string SalesTaxNo { get; set; } = null!;
    ///<example></example>
    public string ServiceTaxNo { get; set; } = null!;
    ///<example></example>
    public string Tin { get; set; } = null!;
    ///<example>0</example>
    public int IdType { get; set; }
    ///<example></example>
    public string IdNo { get; set; } = null!;
    ///<example></example>
    public string TourismNo { get; set; } = null!;
    ///<example></example>
    public string Sic { get; set; } = null!;
    ///<example>0</example>
    public int SubmissionType { get; set; }
    ///<example></example>
    public string IrbmClassification { get; set; } = null!;
    ///<example></example>
    public string PeppolId { get; set; } = null!;
    ///<example></example>
    public string BusinessUnit { get; set; } = null!;
    ///<example></example>
    public string TaxArea { get; set; } = null!;
    ///<example></example>
    public string Attachments { get; set; } = null!;
    ///<example></example>
    public string Remark { get; set; } = null!;
    ///<example></example>
    public string Note { get; set; } = null!;
    ///<example>1723112184</example>
    public long LastModified { get; set; }
    public List<CdsBranch> CdsBranch { get; set; } = null!;
}

public class CdsBranch
{
    ///<example>1</example>
    public int DtlKey { get; set; }
    ///<example>300-A0001</example>
    public string Code1 { get; set; } = null!;
    ///<example>B</example>
    public string BranchType { get; set; } = null!;
    ///<example>BILLING</example>
    public string BranchName { get; set; } = null!;
    ///<example>Lot 98, Jalan Mentari Kuah</example>
    public string Address1 { get; set; } = null!;
    ///<example>50480 KL</example>
    public string Address2 { get; set; } = null!;
    ///<example></example>
    public string Address3 { get; set; } = null!;
    ///<example></example>
    public string Address4 { get; set; } = null!;
    ///<example></example>
    public string Postcode { get; set; } = null!;
    ///<example></example>
    public string City { get; set; } = null!;
    ///<example></example>
    public string State { get; set; } = null!;
    ///<example></example>
    public string Country { get; set; } = null!;
    ///<example></example>
    public string GeoLat { get; set; } = null!;
    ///<example></example>
    public string GeoLong { get; set; } = null!;
    ///<example>Mr. Delta</example>
    public string Attention { get; set; } = null!;
    ///<example>04-976 5235</example>
    public string Phone1 { get; set; } = null!;
    ///<example></example>
    public string Phone2 { get; set; } = null!;
    ///<example></example>
    public string Mobile { get; set; } = null!;
    ///<example></example>
    public string Fax1 { get; set; } = null!;
    ///<example></example>
    public string Fax2 { get; set; } = null!;
    ///<example>delta@ahome.com</example>
    public string Email { get; set; } = null!;

}
