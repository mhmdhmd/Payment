namespace Payment.Application.ViewModels.Base;

/// <summary>
/// Represents a generic result that contains a collection of DTO.
/// </summary>
/// <typeparam name="T">The type of DTO in the collection.</typeparam>
public class ListResult<T> : ResultBase
{
    /// <summary>
    /// Gets or sets the collection of DTO.
    /// </summary>
    public ICollection<T>? DataList { get; set; }
}