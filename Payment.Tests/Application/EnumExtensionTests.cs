using Payment.Application.Extensions;

namespace Payment.Tests.Application;

[TestFixture]
public class EnumExtensionTests
{
    public enum SampleEnum
    {
        [System.ComponentModel.Description("First value")] Value1,
        [System.ComponentModel.Description("Second value")] Value2,
        Value3
    }
    
    [Test]
    public void GetDescription_EnumValueWithDescriptionAttribute_ReturnsDescription()
    {
        // Arrange
        var value = SampleEnum.Value1;
        var expectedDescription = "First value";

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.That(result, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void GetDescription_EnumValueWithoutDescriptionAttribute_ReturnsEnumToString()
    {
        // Arrange
        var value = SampleEnum.Value3;
        var expectedDescription = "Value3";

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.That(result, Is.EqualTo(expectedDescription));
    }
    
    [Test]
    public void GetDescription_InvalidEnumValue_ReturnsEmptyString()
    {
        // Arrange
        var value = (SampleEnum)100; // An invalid value
        var expectedDescription = string.Empty;

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.That(result, Is.EqualTo(expectedDescription));
    }
}