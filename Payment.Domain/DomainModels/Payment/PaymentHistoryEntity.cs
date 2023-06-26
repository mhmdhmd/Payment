using Payment.Domain.DomainModels.Order;

namespace Payment.Domain.DomainModels.Payment;

public class PaymentHistoryEntity: EntityBase<int>
{
    public OrderEntity OrderEntity { get; set; }
    public DateTime PayDate { get; set; }
    public int Status { get; set; }
    public int OrderEntityId { get; set; }
    public string? TransactionId { get; set; }
}