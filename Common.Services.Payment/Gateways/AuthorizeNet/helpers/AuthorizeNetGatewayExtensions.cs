using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet;
using AuthorizeNet.APICore;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment.Gateways.AuthNet.helpers
{
    public static class AuthorizeNetGatewayExtensions
    {
        public static IGatewayProfile MapCustomerProfileToGatewayProfile(this AuthorizeNet.APICore.getCustomerProfileResponse response)
        {
            var result = new GatewayProfile();
            result.ProfileCustomerData = new CustomerData();
            result.ProfileCustomerData.Address = new AddressType();
            result.ProfileId = response.profile.customerProfileId;
            result.ProfileCustomerData.EmailAddress = response.profile.email;
            result.ProfileCustomerData.CustomerDescription = response.profile.description;
            if (response.profile.paymentProfiles != null)
            {
                response.profile.paymentProfiles.ToList().ForEach(n =>
                    {
                        var gatewayPaymentProfile = n.MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile();
                        result.PaymentProfiles.Add(gatewayPaymentProfile);
                    });
            }
            return result;
        }

        public static IGatewayPaymentProfile MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile(this AuthorizeNet.APICore.customerPaymentProfileMaskedType response)
        {
            var result = new GatewayPaymentProfile();
            result.PaymentCardData = new PaymentCardData();
            result.PaymentProfileId = response.customerPaymentProfileId;
            if (response.billTo != null)
            {
                
                result.PaymentCardData.BillingAddress = response.billTo.MapCustomerAddressTypeToAddressType();
                result.PaymentCardData.CardHolderFirstName = response.billTo.firstName;
                result.PaymentCardData.CardHolderLastName = response.billTo.lastName;
            }
            var creditCard = response.payment.Item as AuthorizeNet.APICore.creditCardMaskedType;
            if (creditCard != null)
            {
                result.PaymentCardData.MaskedCardNumber = creditCard.cardNumber;
                PaymentCardType cardType = PaymentCardType.Unknown;
                Enum.TryParse<PaymentCardType>(creditCard.cardType, out cardType);
                result.PaymentCardData.ExpirationMonth = int.Parse(creditCard.expirationDate.Substring(2, 2));
                result.PaymentCardData.ExpirationYear = int.Parse(creditCard.expirationDate.Substring(0, 4));
               
            }

            return result;
            
            
        }
        public static IAddressType MapCustomerAddressTypeToAddressType(this AuthorizeNet.APICore.customerAddressType response)
        {
            var result = new AddressType();
            result.AddressLine1 = response.address;
            result.City = response.city;
            result.Company = response.company;
            result.Country = response.country;
            result.PhoneNumber = response.phoneNumber;
            result.PostalCode = response.zip;
            result.State = response.state;
            return result;
        }
    }
}
