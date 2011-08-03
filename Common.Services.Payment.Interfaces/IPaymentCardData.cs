using System;
namespace Common.Services.Payment.Interfaces
{
    public interface IPaymentCardData
    {
        string CardHolderName { get; set; }
        string CardHolderFirstName { get; set; }
        string CardHolderLastName { get; set; }
        string CardNumber { get; set; }
        string MaskedCardNumber { get; set; }
        PaymentCardType CardType { get; set; }
        IAddressType BillingAddress { get; set; }
        int ExpirationMonth { get; set; }
        int ExpirationYear { get; set; }
        string SecurityCode { get; set; }
    }
}
