using Payment.Application.ViewModels.Base;

namespace Payment.Application.ViewModels;

public class PaymentRequest : Request<PaymentResult>
{
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpiryMonth { get; set; }
    public string? ExpiryYear { get; set; }
    public string? SecurityCode { get; set; }
    
    //****
    public string? FName { get; set; }
    public string? LName { get; set; }
    public string? Address { get; set; }
}

public class PaymentResult : SingleResult<PaymentResultModel> { }

public class PaymentResultModel
{
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionId { get; set; }
    public string? Status { get; set; }
    public string? ResultType { get; set; }
}