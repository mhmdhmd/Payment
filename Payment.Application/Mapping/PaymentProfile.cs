using AutoMapper;
using Payment.Application.Extensions;
using Payment.Application.Interfaces;
using Payment.Application.ViewModels;
using Payment.Domain.DomainModels.Payment;
using Payment.Domain.DomainModels.Shopper;

namespace Payment.Application.Mapping;

/// <summary>
/// Represents a mapping profile for payment-related entities and models (AutoMapper)
/// </summary>
public class PaymentProfile: Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentRequest, PaymentInfo>()
            .ForMember(dest => dest.CurrencyType, opt => opt.MapFrom(src => src.Currency.ToEnum<CurrencyType>(true)))
            .ForMember(dest => dest.Card, opt => opt.MapFrom(src => new Card
            {
                EncryptedCardNumber = src.CardNumber,
                EncryptedExpiryMonth = src.ExpiryMonth,
                EncryptedExpiryYear = src.ExpiryYear,
                EncryptedSecurityCode = src.SecurityCode
            }));

        CreateMap<IPaymentResponse, PaymentResult>()
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => new PaymentResultModel
            {
                Currency = src.Currency,
                Amount = src.Amount,
                TransactionId = src.TransactionId,
                Status = src.Status.GetDescription(),
                ResultType = src.ResultType
            }));

        CreateMap<PaymentRequest, ShopperEntity>();

        CreateMap<PaymentHistoryEntity, PaymentHistoryModel>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PayDate))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OrderEntity.TotalPrice))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.OrderEntity.ShopperEntity.FName} {src.OrderEntity.ShopperEntity.LName}"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status == 0 ? "Paid" : "Failed"));



        CreateMap<IEnumerable<PaymentHistoryEntity>, PaymentHistoryResult>()
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.DataList, opt => opt.MapFrom(src => src.ToList()));
    }
}