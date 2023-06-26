namespace Payment.Application.ViewModels.Base;

/// <summary>
/// Represents a single result with a DTO item of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the DTO item.</typeparam>
public class SingleResult<T> : ResultBase
{
    /// <summary>
    /// Gets or sets the DTO item.
    /// </summary>
    public T? Data { get; set; }
}