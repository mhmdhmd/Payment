namespace Payment.Domain.DomainModels.Order;

public class OrderDetailEntity: EntityBase<int>
{
    public OrderEntity OrderEntity { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int OrderEntityId { get; set; }
}