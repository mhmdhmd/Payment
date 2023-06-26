namespace Payment.Domain.DomainModels;

/// <summary>
/// Represents the base class for entities with a generic identifier.
/// </summary>
/// <typeparam name="T">The type of the identifier.</typeparam>
public abstract class EntityBase<T>
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    public T Id { get; set; }
}