using System.ComponentModel;

namespace Payment.Application.ViewModels;

public enum CurrencyType
{
    [Description("USD")] USD,
    [Description("EUR")] EUR
}