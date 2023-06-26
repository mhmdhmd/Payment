using System.ComponentModel;
using System.Reflection;

namespace Payment.Application.Extensions;

/// <summary>
/// Provides extension methods for working with enums.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// Gets the description attribute value associated with the enum value.
    /// If no description attribute is found, returns the string representation of the enum value.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The description attribute value or the string representation of the enum value.</returns>
    public static string GetDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : value.ToString();
        }
        
        return string.Empty;
    }
}