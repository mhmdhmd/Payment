namespace Payment.Infrastructure.Services.Payment.Adyen.Helper;

/// <summary>
/// Provides methods for converting currency amounts between major(decimal) and minor units(long). (For Adyen)
/// </summary>
public static class CurrencyConverter
{
    /// <summary>
    /// Converts the specified amount from major units to minor units based on the currency code.
    /// </summary>
    /// <param name="amount">The amount in major units(decimal).</param>
    /// <param name="currencyCode">The currency code.</param>
    /// <returns>The amount in minor units(long).</returns>
    public static long ToMinorUnits(decimal amount, string currencyCode)
    {
        int decimals = GetDecimals(currencyCode);
        decimal multiplier = (decimal)Math.Pow(10, decimals);
        return (long)(amount * multiplier);
    }

    /// <summary>
    /// Converts the specified amount from minor units to major units based on the currency code.
    /// </summary>
    /// <param name="amount">The amount in minor units(long).</param>
    /// <param name="currencyCode">The currency code.</param>
    /// <returns>The amount in major units(decimal).</returns>
    public static decimal FromMinorUnits(long amount, string currencyCode)
    {
        int decimals = GetDecimals(currencyCode);
        decimal divisor = (decimal)Math.Pow(10, decimals);
        return amount / divisor;
    }

    private static int GetDecimals(string currencyCode)
    {
        switch (currencyCode)
        {
            case "CLP":
            case "CVE":
            case "IDR":
            case "ISK":
                return 0;
            case "BHD":
                return 3;
            default:
                return 2;
        }
    }
}