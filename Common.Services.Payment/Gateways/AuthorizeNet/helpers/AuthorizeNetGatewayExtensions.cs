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
            result.Id = response.profile.customerProfileId;
            result.ProfileCustomerData.EmailAddress = response.profile.email;
            result.ProfileCustomerData.CustomerDescription = response.profile.description;
            if (response.profile.paymentProfiles != null)
            {
                result.PaymentProfiles = new List<IGatewayPaymentProfile>();
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
            result.Id = response.customerPaymentProfileId;
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
                //result.PaymentCardData.ExpirationMonth = int.Parse(creditCard.expirationDate.Substring(2, 2));
                //result.PaymentCardData.ExpirationYear = int.Parse(creditCard.expirationDate.Substring(0, 4));
               
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
            result.StateProvince = response.state;
            return result;
        }
        public static AuthorizeNet.APICore.customerPaymentProfileType MapPaymentDataToCustomerPaymentProfileType(this IPaymentData request)
        {
            var result = new AuthorizeNet.APICore.customerPaymentProfileType();
            result.billTo = request.CardData.BillingAddress.MapAddressTypeToCustomerAddressType();
            result.customerTypeSpecified = false;
            result.billTo.firstName = request.CardData.CardHolderFirstName;
            result.billTo.lastName = request.CardData.CardHolderLastName;
            result.payment = new AuthorizeNet.APICore.paymentType();
            result.payment.Item = request.CardData.MapIPaymentCardDataToCreditCardType();
            return result;
        }
        public static AuthorizeNet.APICore.customerAddressType MapAddressTypeToCustomerAddressType(this IAddressType request)
        {
            var result = new AuthorizeNet.APICore.customerAddressType();
            result.address = String.Format("{0} {1}", request.AddressLine1, request.AddressLine2);
            result.city = request.City;
            result.country = request.Country;
            result.company = request.Company;
            result.state = request.StateProvince;
            result.zip = request.PostalCode;
            result.phoneNumber = request.PhoneNumber;
            return result;
            
        }
        public static AuthorizeNet.APICore.profileTransactionType MapPaymentDataToProfileTransAuthCaptureType(this IPaymentData request,long paymentProfileId,long customerProfileId)
        {
            var result = new AuthorizeNet.APICore.profileTransactionType();
            AuthorizeNet.APICore.profileTransAuthCaptureType transactionType = new AuthorizeNet.APICore.profileTransAuthCaptureType();
            transactionType.amount = request.Transaction.Amount;
            transactionType.cardCode = request.CardData.SecurityCode;
            transactionType.customerPaymentProfileId = paymentProfileId.ToString();
            transactionType.customerProfileId = customerProfileId.ToString();
            result.Item = transactionType;
            return result;
            
        }
        public static AuthorizeNet.APICore.creditCardType MapIPaymentCardDataToCreditCardType(this IPaymentCardData request)
        {
            var result = new AuthorizeNet.APICore.creditCardType();
            result.cardCode = request.SecurityCode;
            result.cardNumber = request.CardNumber;
            if(request.ExpirationYear.ToString().Length==2)
                request.ExpirationYear=2000+request.ExpirationYear;

            result.expirationDate = request.ExpirationYear + "-" + request.ExpirationMonth.ToString().PadLeft(2, '0');
            return result;
        }
        public static AuthorizeNet.APICore.customerType MapCustomerDataToCustomerType(this ICustomerData request)
        {
            var result = new AuthorizeNet.APICore.customerType();
            result.email = request.EmailAddress;
            result.typeSpecified = false;
            return result;
        }
        public static AuthorizeNet.APICore.nameAndAddressType MapCustomerDataToNameAddressType(this ICustomerData request)
        {
            var result = new AuthorizeNet.APICore.nameAndAddressType();
            result.address = String.Format("{0} {1}", request.Address.AddressLine1, request.Address.AddressLine2);
            result.city = request.Address.City;
            result.state = request.Address.StateProvince;
            result.country = request.Address.Country;
            result.firstName = request.FirstName;
            result.lastName = request.LastName;
            result.zip = request.Address.PostalCode;
            return result;
        }
        public static AuthorizeNet.APICore.ARBSubscriptionType MapRecurringPaymentDataToSubscription(this IRecurringPaymentData request)
        {
            var result = new AuthorizeNet.APICore.ARBSubscriptionType();
            var paymentScheduleType = new AuthorizeNet.APICore.paymentScheduleType();
            var paymentScheduleTypeInterval = new AuthorizeNet.APICore.paymentScheduleTypeInterval();
            paymentScheduleTypeInterval.length = (short)request.Intervals;
            paymentScheduleTypeInterval.unit = request.RecurringPaymentInterval == RecurringPaymentIntervals.Days ? AuthorizeNet.APICore.ARBSubscriptionUnitEnum.days : AuthorizeNet.APICore.ARBSubscriptionUnitEnum.months;
                   
            paymentScheduleType.interval = paymentScheduleTypeInterval;
            paymentScheduleType.startDateSpecified = request.StartDate.HasValue;
            if (request.StartDate.HasValue)
                paymentScheduleType.startDate = request.StartDate.Value;
            //Defaults to 9999 no end
            paymentScheduleType.totalOccurrences = 9999;
            paymentScheduleType.totalOccurrencesSpecified = true;
            if(request.Intervals.HasValue)
                paymentScheduleType.totalOccurrences= (short)request.Intervals.Value;
            paymentScheduleType.trialOccurrencesSpecified = request.TrialIntervals.HasValue;
            if (request.TrialIntervals.HasValue)
            {
                paymentScheduleType.trialOccurrences = (short)request.TrialIntervals;
                result.trialAmountSpecified = request.TrialIntervalPrice.HasValue;
                if (request.TrialIntervalPrice.HasValue)
                    result.trialAmount = request.TrialIntervalPrice.Value;
            }
            result.paymentSchedule = paymentScheduleType;
            result.payment = new AuthorizeNet.APICore.paymentType();
            result.payment.Item = request.CardData.MapIPaymentCardDataToCreditCardType();
            result.customer = request.Customer.MapCustomerDataToCustomerType();
            result.billTo = request.Customer.MapCustomerDataToNameAddressType();
            result.amountSpecified = request.Transaction.Amount > 0;
            if (request.Transaction.Amount>0)
                result.amount = request.Transaction.Amount;

            return result;
            

            
        }
    }
}
