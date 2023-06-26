using System.ComponentModel;

namespace Payment.Application.ViewModels;

public enum PaymentStatus
{
    [Description("Paid")] Paid = 0,
    [Description("Failed")] Failed = -1
}