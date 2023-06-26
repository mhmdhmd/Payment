namespace Payment.Application.ViewModels;

public class Card
{
    public string? EncryptedCardNumber { get; set; }
    public string? EncryptedExpiryMonth { get; set; }
    public string? EncryptedExpiryYear { get; set; }
    public string? EncryptedSecurityCode { get; set; }
}