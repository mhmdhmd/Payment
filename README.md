# `Payment` Test Project 👛💻

Welcome to the `Payment` test project! This project demonstrates a payment system that focuses on multi-payment providers. It utilizes the Factory Pattern and Adapter Pattern to create a flexible environment where new payment providers can be easily added and used for the payment process. The project also includes a simple frontend built with Angular, showcasing a simple shoe store that works with the backend to allow customers to add products to their cart, proceed to checkout, and complete the payment process. Additionally, it provides a payment history feature.

## Project Structure 📁

The `Payment` test project consists of the following components:

### Payment.Domain 🏦

The `Payment.Domain` project contains the domain models that represent the core entities and concepts of the payment system.

### Payment.Application 📝

The `Payment.Application` project is the application layer that contains classes and interfaces related to the application logic. It includes view models, services and repository interfaces, factory and adapter pattern interfaces, and AutoMapper profile classes.

### Payment.Infrastructure 🏢

The `Payment.Infrastructure` project is the infrastructure layer responsible for interacting with external services (such Adyen or PayPal) and managing persistence. It includes services that interact with payment providers and implementations of repositories.

### Payment.Api 🌐

The `Payment.Api` project is the API project that exposes endpoints for interacting with the payment system. It handles request processing, dependency injection registration, and integrates the different layers of the system.

### Payment.Tests ✅

The `Payment.Tests` project contains all the unit tests for the various components of the `Payment` project. It ensures the correctness and reliability of the implemented functionality.

### payment-client 💻

The `payment-client` folder contains the Angular client application that provides the user interface for the shoe store and payment process. It communicates with the backend API to perform payment-related operations and display the payment history.

## Running the Project ▶️

To run the `Payment` project, follow these steps:

### Backend 🖥️🔙

1. Ensure that you have a relational database (such as MySQL) set up and running.

2. Open a command prompt or terminal and navigate to the `Payment` directory (the root directory of all projects).

3. Run the following command to apply the database migrations and create the necessary database and tables:

   ```
   dotnet ef database update -s Payment.Api -p Payment.Infrastructure
   ```

   This will create the database based on the existing database configuration in the backend project.

   If you're using a different database, make sure to install the database provider Nuget package and update the connection string accordingly.
   
   Then update `AddDbContext` registration in `Program.cs` based on selected database.


4. Open the `launchSettings.json` file located in the `Properties` folder of the `Payment.Api` project and set the desired address and port in the `"applicationUrl"` property to specify where the backend API should listen for requests.

6. Build and run the `Payment.Api` project.

### Frontend 💻

1. Open a command prompt or terminal and navigate to the `payment-client` directory.

2. Run the following command to install the necessary dependencies:

   ```
   npm install
   ```

3. Open the `src/environments/environment.ts` file in the `payment-client` project. Set the `baseUrl` property to the address and port where the backend API is running. This will allow the frontend to communicate with the backend.
4. After the dependencies are installed, run the following command to start the Angular development server:

   ```
   ng serve
   ```
5. Open your web browser and navigate to the `http://localhost:4200` to access the `Payment` frontend project.

## Adding a New Payment Provider ➕🏦

To add a new payment provider to the backend, follow these steps:

1. Create a class that implements the `IPaymentProvider` interface. This class will encapsulate the logic for interacting with the specific payment provider's API.

   Make sure to assign a name of new provider to `Name` property of `IPaymentProvider`. Assign it in the constructor of new class (for example, `PayPalPaymentProvider.cs`)
   
   The name should be same as the `Provider` entry value, under the `PaymentSettings` entry of `appsettings.json` file.


2. Create a class that implements the `IPaymentResponse` interface. This class will be responsible for converting the response received from the new payment provider's API into a common response format defined by the domain.

3. Register the new payment provider in the `PaymentProviderFactoryExtension` class. Open the `Web -> Payment.Api -> Extensions -> PaymentProviderFactoryExtension.cs` file and add the registration for the new provider in the `AddPaymentProviderFactory` method.

4. Update the `appsettings.json` file with the necessary settings for the new payment provider. Add the required configuration under the `PaymentSettings` section.

Example configuration for a new payment provider in the `appsettings.json` file:

```json
{
  "PaymentSettings": {
    "Provider": "new-payment-provider-name",
    "NewPaymentProviderSettings": {
      "ApiKey": "your-api-key",
      "OtherSettings": "other-values"
    }
  }
}
```

Make sure to replace `"your-api-key"` and `"OtherSettings"` with the actual values and settings required for the new payment provider.

Thank You! 🙏