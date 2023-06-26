using Payment.Domain.DomainModels;

namespace Payment.Application.ViewModels;

public class PaymentInfo
{
    public CurrencyType CurrencyType { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Card? Card { get; set; }
}