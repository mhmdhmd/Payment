namespace Payment.Application.Interfaces;

public interface IPaymentProviderFactory
{
    IPaymentProvider Create();
}