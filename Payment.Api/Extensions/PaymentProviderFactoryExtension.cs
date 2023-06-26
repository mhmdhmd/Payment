using Payment.Application.Factories;
using Payment.Application.Interfaces;
using Payment.Infrastructure.Services.Payment.Adyen;

namespace Payment.Api.Extensions;

/// <summary>
/// Extension methods for configuring payment provider factory.
/// </summary>
public static class PaymentProviderFactoryExtension
{
    /// <summary>
    /// Adds the payment provider factory to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddPaymentProviderFactory(this IServiceCollection services)
    {
        services.AddTransient<IPaymentProvider, AdyenPaymentProvider>();
        // services.AddTransient<IPaymentProvider, PayPalPaymentProvider>();

        services.AddSingleton<Func<IEnumerable<IPaymentProvider>>>(x =>
            () => x.GetService<IEnumerable<IPaymentProvider>>()!);

        services.AddSingleton<IPaymentProviderFactory, PaymentProviderFactory>();
    }
}