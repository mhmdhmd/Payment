using Payment.Infrastructure.Services.Payment.Adyen.Helper;

namespace Payment.Tests.Infrastructure.Adyen;

[TestFixture]
public class CurrencyConverterTests
{
    [Test]
    public void ToMinorUnits_WithCurrencyCodeWithZeroDecimals_ReturnsCorrectValue()
    {
        // Arrange
        decimal amount = 123.45m;
        string currencyCode = "CLP";
        long expectedMinorUnits = 123;

        // Act
        long result = CurrencyConverter.ToMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedMinorUnits));
    }

    [Test]
    public void ToMinorUnits_WithCurrencyCodeWithTwoDecimals_ReturnsCorrectValue()
    {
        // Arrange
        decimal amount = 123.45m;
        string currencyCode = "USD";
        long expectedMinorUnits = 12345;

        // Act
        long result = CurrencyConverter.ToMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedMinorUnits));
    }

    [Test]
    public void ToMinorUnits_WithCurrencyCodeWithThreeDecimals_ReturnsCorrectValue()
    {
        // Arrange
        decimal amount = 123.45m;
        string currencyCode = "BHD";
        long expectedMinorUnits = 123450;

        // Act
        long result = CurrencyConverter.ToMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedMinorUnits));
    }

    [Test]
    public void FromMinorUnits_WithCurrencyCodeWithZeroDecimals_ReturnsCorrectValue()
    {
        // Arrange
        long amount = 123;
        string currencyCode = "CLP";
        decimal expectedAmount = 123;

        // Act
        decimal result = CurrencyConverter.FromMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void FromMinorUnits_WithCurrencyCodeWithTwoDecimals_ReturnsCorrectValue()
    {
        // Arrange
        long amount = 12345;
        string currencyCode = "USD";
        decimal expectedAmount = 123.45m;

        // Act
        decimal result = CurrencyConverter.FromMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void FromMinorUnits_WithCurrencyCodeWithThreeDecimals_ReturnsCorrectValue()
    {
        // Arrange
        long amount = 123450;
        string currencyCode = "BHD";
        decimal expectedAmount = 123.45m;

        // Act
        decimal result = CurrencyConverter.FromMinorUnits(amount, currencyCode);

        // Assert
        Assert.That(result, Is.EqualTo(expectedAmount));
    }
}