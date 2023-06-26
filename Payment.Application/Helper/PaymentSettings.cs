namespace Payment.Application.Helper;

/// <summary>
/// Represents the Payment Settings provided by appsettings.json file. it is used in IOption pattern
/// </summary>
public class PaymentSettings
{
    public string? Provider { get; set; }
    public AdyenSettings? AdyenSettings { get; set; }
}

public class AdyenSettings
{
    public string? ApiKey { get; set; }
    public string? MerchantAccount { get; set; }
}