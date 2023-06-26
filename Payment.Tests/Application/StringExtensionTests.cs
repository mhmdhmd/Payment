using Payment.Application.Extensions;

namespace Payment.Tests.Application;

[TestFixture]
public class StringExtensionTests
{
    public enum SampleEnum
    {
        Value1,
        Value2,
        Value3
    }
    
    public enum OtherEnum {}

    [Test]
    public void ToEnum_ValidStringValue_ReturnsEnumValue()
    {
        // Arrange
        var value = "Value2";
        var expectedEnum = SampleEnum.Value2;

        // Act
        var result = value.ToEnum<SampleEnum>();

        // Assert
        Assert.That(result, Is.EqualTo(expectedEnum));
    }

    [Test]
    public void ToEnum_InvalidStringValue_ThrowsArgumentException()
    {
        // Arrange
        var value = "InvalidValue";

        // Act and Assert
        Assert.Throws<ArgumentException>(() => value.ToEnum<SampleEnum>());
    }

    [Test]
    public void ToEnum_NullStringValue_ThrowsArgumentNullException()
    {
        // Arrange
        string value = null;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => value.ToEnum<SampleEnum>());
    }

    [Test]
    public void ToEnum_NonEnumType_ThrowsArgumentException()
    {
        // Arrange
        var value = "Value1";

        // Act and Assert
        Assert.Throws<ArgumentException>(() => value.ToEnum<OtherEnum>());
    }
}