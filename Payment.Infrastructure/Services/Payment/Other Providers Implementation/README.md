# Other Payment Providers
The application supports multiple payment providers, and new providers can be easily added by implementing the necessary classes and interfaces.

## Implementing a New Payment Provider
To implement a new payment provider, follow these steps:
1. Create a class that implements the `IPaymentProvider` interface. This class will encapsulate the logic for interacting with the specific payment provider's API.
2. Create a class that implements the `IPaymentResponse` interface. This class will be responsible for converting the response received from the new payment provider's API into a common response format defined by the domain.
3. Register the new payment provider in the `PaymentProviderFactoryExtension` class. Open the `Web -> Payment.Api -> Extensions -> PaymentProviderFactoryExtension.cs` file and add the registration for the new provider in the `AddPaymentProviderFactory` method.
4. Update the `appsettings.json` file with the necessary settings for the new payment provider. Add the required configuration under the `PaymentSettings` section. Make sure to set the `Provider` entry to the same value as the `Name` property in the `IPaymentProvider` implementation.

## AppSettings Configuration
The `appsettings.json` file contains the configuration settings for the payment service. When adding a new payment provider, you'll need to add the specific settings required for that provider. The settings should be added under the `PaymentSettings` section.
Example configuration for a new payment provider:

```json
{
  "PaymentSettings": {
    "Provider": "NewPaymentProvider",
    "NewPaymentProviderSettings": {
      "ApiKey": "your-api-key",
      "OtherSettings": "other-values"
    }
  }
}