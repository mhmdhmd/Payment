namespace Payment.Application.Extensions;

/// <summary>
/// Provides extension methods for working with strings.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Converts a string value to the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="value">The string value to convert.</param>
    /// <param name="ignoreCase">Specifies whether the conversion should ignore case.</param>
    /// <returns>The converted enum value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the enum type is not an enumeration or the conversion fails.</exception>
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct
    {
        if (value is null) throw new ArgumentNullException(nameof(value), "Failed to convert null value");
        if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enumeration type.");

 

        TEnum enumValue;
        if (Enum.TryParse(value, ignoreCase, out enumValue))
            return enumValue;

 

        throw new ArgumentException($"Failed to convert '{value}' to {typeof(TEnum).Name}.");
    }
}