namespace SqlAccountRestAPI.Controllers;

public class AddCustomerPaymentRequest
{
    public string DocumentNo { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string Project { get; set; } = null!;
    public string Code { get; set; } = null!; // Release-0.0.42 and older
}
