namespace Payment.Domain.DomainModels.Shopper;

public class ShopperEntity: EntityBase<int>
{
    public string FName { get; set; }
    public string LName { get; set; }
    public string Address { get; set; }
}