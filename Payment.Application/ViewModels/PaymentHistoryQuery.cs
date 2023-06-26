using Payment.Application.ViewModels.Base;

namespace Payment.Application.ViewModels;

public class PaymentHistoryResult : ListResult<PaymentHistoryModel>{}

public class PaymentHistoryModel
{
    public string TransactionId { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
} 