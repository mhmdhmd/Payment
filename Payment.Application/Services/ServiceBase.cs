using AutoMapper;

namespace Payment.Application.Services;

/// <summary>
/// Represents a base service class that provides common functionality for derived services.
/// </summary>
public abstract class ServiceBase
{
    /// <summary>
    /// The AutoMapper mapper instance used for object mapping.
    /// </summary>
    protected readonly IMapper Mapper;

    protected ServiceBase(IMapper mapper)
    {
        Mapper = mapper;
    }
}