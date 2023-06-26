namespace Payment.Application.ViewModels.Base;

/// <summary>
/// Represents a request with a generic type parameter <typeparamref name="T"/>.
/// The generic type parameter <typeparamref name="T"/> must derive from <see cref="ResultBase"/>.
/// </summary>
/// <typeparam name="T">The type of the result derived from <see cref="ResultBase"/>.</typeparam>
public class Request<T>
    where T : ResultBase
{}