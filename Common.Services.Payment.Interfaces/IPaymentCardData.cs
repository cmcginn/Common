using System;
namespace Common.Services.Payment.Interfaces
{
    public interface IPaymentCardData
    {
        string CardHolderName { get; set; }
        string CardNumber { get; set; }
        PaymentCardType CardType { get; set; }
        IAddressType BillingAddress { get; set; }
        int ExpirationMonth { get; set; }
        int ExpirationYear { get; set; }
        string SecurityCode { get; set; }
    }
}
