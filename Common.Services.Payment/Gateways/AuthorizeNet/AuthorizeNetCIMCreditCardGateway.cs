using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Web;
using System.Diagnostics.Contracts;
using Common.Services.Payment.Gateways.AuthNet.helpers;

namespace Common.Services.Payment.Gateways.AuthNet
{
    public class AuthorizeNetCIMCreditCardGateway : BaseProfileCreditCardGateway
    {


        #region public interface
        public const string GATEWAY_ID_STRING = "7107FFA1-D376-422E-9DF5-A15DD6909E9C";
        const string DUPLICATE_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate record with ID ";
        const string DUPLICATE_PAYMENT_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate customer payment profile already exists.";
        public const string GATEWAY_NAME = "CIMAuthorize.Net";
        public override Guid GatewayId
        {
            get { return Guid.Parse(GATEWAY_ID_STRING); }
        }
        public override string GatewayName
        {
            get { return GATEWAY_NAME; }
        }

        public override bool SupportsAuthorize
        {
            get { return true; }
        }

        public override bool SupportsCapture
        {
            get { return true; }
        }

        public override bool SupportsCharge
        {
            get { return true; }
        }

        public override bool SupportsRefund
        {
            get { return true; }
        }

        public override bool SupportsVoid
        {
            get { return true; }
        }

        public override bool Authorize(IPaymentData data)
        {
            Contract.Requires(data.Customer != null, "Customer required");
            Contract.Requires(data.CardData != null, "CardData Required");
            Contract.Requires(data.CardData.BillingAddress != null, "CardData billing address required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.BillingAddress.AddressLine1), "CardData Street Address Required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.BillingAddress.City), "CardData City Required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.BillingAddress.Country), "CardData country required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.BillingAddress.PostalCode), "CardData postal code required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.CardNumber), "CardData cardnumber required");
            Contract.Requires(data.CardData.ExpirationMonth > 0, "CardData expiration month required");
            Contract.Requires(data.CardData.ExpirationYear >= System.DateTime.Now.Year, "CardData expiration year must be greater than or equal current year.");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.CardHolderName), "CardData carholder name required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.Customer.FirstName), "PaymentData first name required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.Customer.LastName), "PaymentData last name required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.CardHolderFirstName), "PaymentData cardholder first name required");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.CardData.CardHolderLastName), "PaymentData cardholder last name required");
            Contract.Requires(data.Transaction != null, "PaymentData transaction required");
            Contract.Requires(data.Transaction.Amount > 0, "PaymentData transaction amount must be greater than zero");

            if (!SupportsAuthorize)
                throw new System.NotSupportedException("Authorize not supported by gateway");
            //If profile exists then the profile id will be returned
            IGatewayProfile profile = GetOrCreateCustomerProfile(data.Customer);
            var customerPaymentProfile = data.MapPaymentDataToCustomerPaymentProfileType();
            var service = new AuthorizeNetCIMGatewayHelper();
            service.MerchantAuthenticationType = MerchantAuthentication;
            var paymentProfile = GetOrCreateCustomerPaymentProfile(data, long.Parse(profile.ProfileId));
            var transaction = data.MapPaymentDataToProfileTransAuthCaptureType(long.Parse(paymentProfile.PaymentProfileId),long.Parse(profile.ProfileId));
            var transactionResult = service.CreateProfileTransaction(transaction);
            return transactionResult.messages.resultCode == AuthorizeNet.APICore.messageTypeEnum.Ok;
        }

        public override bool Capture(IPaymentData data)
        {
            throw new NotImplementedException();
        }

        public override bool Charge(IPaymentData data)
        {
            throw new NotImplementedException();
        }

        public override bool Refund(IPaymentData data)
        {
            throw new NotImplementedException();
        }

        public override bool Void(IPaymentData data)
        {
            throw new NotImplementedException();
        }

        public override IGatewayProfile GetCustomerProfile(string id)
        {
            long profileId = long.Parse(id);
            IGatewayProfile result = new GatewayProfile();
            var service = new AuthorizeNetCIMGatewayHelper();
            service.MerchantAuthenticationType = MerchantAuthentication;
            var response = service.GetCustomerProfile(profileId);
            result = response.MapCustomerProfileToGatewayProfile();
            return result;

        }

        public override IGatewayProfile GetOrCreateCustomerProfile(ICustomerData customerData)
        {

            IGatewayProfile result = new GatewayProfile();
            var service = new AuthorizeNetCIMGatewayHelper();
            service.MerchantAuthenticationType = MerchantAuthentication;
            var profileId = service.CreateCustomerProfile(customerData.EmailAddress, customerData.CustomerDescription);
            result = service.GetCustomerProfile(profileId).MapCustomerProfileToGatewayProfile();
            return result;

        }

        #endregion

        #region Class Memebers


        AuthorizeNet.APICore.merchantAuthenticationType _MerchantAuthentication;
        AuthorizeNet.APICore.merchantAuthenticationType MerchantAuthentication
        {
            get
            {
                if (_MerchantAuthentication == null)
                {
                    _MerchantAuthentication = new AuthorizeNet.APICore.merchantAuthenticationType();
                    _MerchantAuthentication.name = Properties.AuthorizeNet.Default.APIAccountName;
                    _MerchantAuthentication.transactionKey = Properties.AuthorizeNet.Default.APIAccountPassword;
                }
                return _MerchantAuthentication;
            }
            set { _MerchantAuthentication = value; }
        }
        IGatewayPaymentProfile GetOrCreateCustomerPaymentProfile(IPaymentData data,long customerProfileId)
        {
            var customerPaymentProfile = data.MapPaymentDataToCustomerPaymentProfileType();
            var service = new AuthorizeNetCIMGatewayHelper();
            IGatewayPaymentProfile result = null;
            service.MerchantAuthenticationType = MerchantAuthentication;
            try
            {                
                var paymentProfileResponse = service.CreateCustomerPaymentProfile(customerPaymentProfile, customerProfileId);
                result = service.GetCustomerPaymentProfile(customerProfileId, long.Parse(paymentProfileResponse.customerPaymentProfileId)).MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile();
                
            }
            catch (System.InvalidOperationException ex)
            {
                if (ex.Message == DUPLICATE_PAYMENT_PROFILE_MESSAGE)
                {
                    var lastFour = data.CardData.CardNumber.Substring(data.CardData.CardNumber.Length - 4);
                    //get the existing profile
                    var paymentProfiles = service.GetCustomerPaymentProfiles(customerProfileId).ToList();
                    var paymentProfile = paymentProfiles.SingleOrDefault(n => n.payment.Item as AuthorizeNet.APICore.creditCardMaskedType != null && ((AuthorizeNet.APICore.creditCardMaskedType)n.payment.Item).cardNumber.Contains(lastFour));
                    result = paymentProfile.MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile();
                }
            }
            return result;
        }
        #endregion



    }
}
