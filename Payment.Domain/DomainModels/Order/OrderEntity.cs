using Payment.Domain.DomainModels.Shopper;

namespace Payment.Domain.DomainModels.Order;

public class OrderEntity : EntityBase<int>
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ShopperEntity ShopperEntity { get; set; }
    public int ShopperEntityId { get; set; }
    public ICollection<OrderDetailEntity> OrderDetails { get; set; }
}