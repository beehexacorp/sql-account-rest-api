namespace SqlAccountRestAPI.Controllers;

public class AddCustomerPaymentRequest
{
    public string BankCharge { get; set; } = null!;
    public string DocumentNo { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string Project { get; set; } = null!;
}
