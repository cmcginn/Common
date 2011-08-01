using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Gateways.AuthNet.helpers
{
    public static class AuthorizeNetExtensions
    {
        public static AddressType MapAddressType(this AuthorizeNet.Address address)
        {
            var result = new AddressType();

            result.AddressLine1 = address.Street;
            result.City = address.City;
            result.Company = address.Company;
            result.Country = address.Country;
            result.PhoneNumber = address.Phone;
            result.PostalCode = address.Zip;
            result.State = address.State;
            return result;
        }

        public static PaymentCardData MapPaymentCardData(this AuthorizeNet.PaymentProfile paymentProfile)
        {
            var result = new PaymentCardData();
            result.CardNumber = paymentProfile.CardNumber;
            Interfaces.PaymentCardType cardType = Interfaces.PaymentCardType.Unknown;
            Enum.TryParse(paymentProfile.CardType,out cardType);
            result.ExpirationMonth = int.Parse(paymentProfile.CardExpiration.Substring(0, 2));
            result.ExpirationYear = int.Parse(paymentProfile.CardExpiration.Substring(2));
            result.SecurityCode = paymentProfile.CardCode;
            if (paymentProfile.BillingAddress != null)
            {
                result.BillingAddress = paymentProfile.BillingAddress.MapAddressType();
                result.CardHolderName = string.Format("{0} {1}", paymentProfile.BillingAddress.First, paymentProfile.BillingAddress.Last);
            }
            return result;
        }
    }
}
