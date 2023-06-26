namespace Payment.Application.ViewModels.Base;

/// <summary>
/// Base class for result objects.
/// </summary>
public class ResultBase
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the error message in case of failure.
    /// </summary>
    public string? ErrorMessage { get; set; }
}