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
            //Pre Conditions
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
            //Post Conditions
            Contract.Ensures(data.Transaction.TransactionMessages.Count > 0, "A critical error was encountered left control of authorize without assigning  transaction result messages");

            if (!SupportsAuthorize)
                throw new System.NotSupportedException("Authorize not supported by gateway");
            //If profile exists then the profile id will be returned            
            IGatewayProfile profile = GetOrCreateCustomerProfile(data);
            var customerPaymentProfile = data.MapPaymentDataToCustomerPaymentProfileType();                   
            var paymentProfile = GetOrCreateCustomerPaymentProfile(profile, data);
            var transaction = data.MapPaymentDataToProfileTransAuthCaptureType(long.Parse(paymentProfile.PaymentProfileId), long.Parse(profile.ProfileId));
            var transactionResult = GatewayHelper.CreateProfileTransaction(transaction);
            data.Transaction.TransactionMessages.AddRange(GetTransactionMessage(transactionResult.messages,"createCustomerProfileTransactionResponse"));
           

            
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
            //Pre Conditions
            Contract.Requires(data.Transaction != null, "A valid transaction is required for refunds");
            Contract.Requires(data.Transaction.Amount > 0, "A refund requires a transaction amount greater than 0");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.Id), "A refund requires a PaymentProfileId assigned to the Id property of PaymentData");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.Transaction.PreviousTransactionReferenceNumber), "A refund requires a previous transaction number");
            Contract.Requires(data.Customer != null, "A refund requires a Customer");
            Contract.Requires(!String.IsNullOrWhiteSpace(data.Customer.CustomerId), "A refund requires a customer profile id assigned as CustomerId");
            //Post Conditions 
            Contract.Ensures(data.Transaction.TransactionMessages.Count > 0, "A critical error was encountered left control of refund without assigning  transaction result messages");            
            var refundTransactionType = new AuthorizeNet.APICore.profileTransRefundType();
            refundTransactionType.customerProfileId = data.Customer.CustomerId;
            refundTransactionType.customerPaymentProfileId = data.Id;
            refundTransactionType.transId = data.Transaction.PreviousTransactionReferenceNumber;
            refundTransactionType.amount = data.Transaction.Amount;
            var profileTransactionType = new AuthorizeNet.APICore.profileTransactionType();
            profileTransactionType.Item = refundTransactionType;            
            var result = GatewayHelper.CreateProfileTransaction(profileTransactionType);           
            data.Transaction.TransactionMessages.AddRange(GetTransactionMessage(result.messages,"createCustomerProfileTransactionResponse"));
            return result.messages.resultCode == AuthorizeNet.APICore.messageTypeEnum.Ok;
        }

        public override bool Void(IPaymentData data)
        {
            throw new NotImplementedException();
        }

        public override IGatewayProfile GetCustomerProfile(string id)
        {
            long profileId = long.Parse(id);
            IGatewayProfile result = new GatewayProfile();           
            var response = GatewayHelper.GetCustomerProfile(profileId);
            result = response.MapCustomerProfileToGatewayProfile();
            return result;

        }

        public override IGatewayProfile GetOrCreateCustomerProfile(IPaymentData data)
        {

            IGatewayProfile result = new GatewayProfile();            
            AuthorizeNet.APICore.messagesType messages = null;
            var profileId = GatewayHelper.CreateCustomerProfile(data.Customer.EmailAddress, data.Customer.CustomerDescription, out messages);
            result = GatewayHelper.GetCustomerProfile(profileId).MapCustomerProfileToGatewayProfile();          
            data.Transaction.TransactionMessages.AddRange(GetTransactionMessage(messages, "createCustomerProfileRequest"));
            return result;

        }

        #endregion

        #region Class Memebers
        IEnumerable<ITransactionMessage> GetTransactionMessage(AuthorizeNet.APICore.messagesType messages,string description)
        {
            TransactionMessageType messageType = messages.resultCode == AuthorizeNet.APICore.messageTypeEnum.Ok ? TransactionMessageType.Information : TransactionMessageType.Error;
            var result = messages.message.Select(n=>new TransactionMessage{ Code=n.code, MessageType=messageType, TransactionMessageResult=n.text, Description=description});
            return result;
        }
        AuthorizeNetCIMGatewayHelper _GatewayHelper;
        public AuthorizeNetCIMGatewayHelper GatewayHelper
        {
            get
            {
                if (_GatewayHelper == null)
                {
                    _GatewayHelper = new AuthorizeNetCIMGatewayHelper();
                    _GatewayHelper.MerchantAuthenticationType = MerchantAuthentication;
                    _GatewayHelper.ServiceMode = GatewaySettings.TestMode ? AuthorizeNet.ServiceMode.Test : AuthorizeNet.ServiceMode.Live;
                }
                return _GatewayHelper;
            }
            set { _GatewayHelper = value; }
        }
        AuthorizeNet.APICore.merchantAuthenticationType _MerchantAuthentication;
        AuthorizeNet.APICore.merchantAuthenticationType MerchantAuthentication
        {
            get
            {
                if (_MerchantAuthentication == null)
                {
                    _MerchantAuthentication = new AuthorizeNet.APICore.merchantAuthenticationType();
                    _MerchantAuthentication.name = GatewaySettings.Username;
                    _MerchantAuthentication.transactionKey = GatewaySettings.Password;
                }
                return _MerchantAuthentication;
            }
            set { _MerchantAuthentication = value; }
        }
        IGatewayPaymentProfile GetOrCreateCustomerPaymentProfile(IGatewayProfile gatewayProfile, IPaymentData data)
        {
            IGatewayPaymentProfile result = null;
            if (gatewayProfile.PaymentProfiles != null && gatewayProfile.PaymentProfiles.Count > 0)
            {
                var lastFour = data.CardData.CardNumber.Substring(data.CardData.CardNumber.Length - 4);
                var paymentProfileMatch = gatewayProfile.PaymentProfiles.SingleOrDefault(n => n.PaymentCardData.MaskedCardNumber.Contains(lastFour));
                if (paymentProfileMatch != null)
                    result = paymentProfileMatch;
            }
            if (result == null)
            {
                result = GetOrCreateCustomerPaymentProfile(data, long.Parse(gatewayProfile.ProfileId));
            }
            return result;
        }
        IGatewayPaymentProfile GetOrCreateCustomerPaymentProfile(IPaymentData data, long customerProfileId)
        {
            var customerPaymentProfile = data.MapPaymentDataToCustomerPaymentProfileType();            
            IGatewayPaymentProfile result = null;
            try
            {
                var paymentProfileResponse = GatewayHelper.CreateCustomerPaymentProfile(customerPaymentProfile, customerProfileId);
                result = GatewayHelper.GetCustomerPaymentProfile(customerProfileId, long.Parse(paymentProfileResponse.customerPaymentProfileId)).MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile();

            }
            catch (System.InvalidOperationException ex)
            {
                if (ex.Message == AuthorizeNetCIMGatewayHelper.DUPLICATE_PAYMENT_PROFILE_MESSAGE)
                {
                    var lastFour = data.CardData.CardNumber.Substring(data.CardData.CardNumber.Length - 4);
                    //get the existing profile
                    var paymentProfiles = GatewayHelper.GetCustomerPaymentProfiles(customerProfileId).ToList();
                    var paymentProfile = paymentProfiles.SingleOrDefault(n => n.payment.Item as AuthorizeNet.APICore.creditCardMaskedType != null && ((AuthorizeNet.APICore.creditCardMaskedType)n.payment.Item).cardNumber.Contains(lastFour));
                    result = paymentProfile.MapCustomerPaymentProfileMaskTypeToGatewayPaymentProfile();
                }
            }
            return result;
        }
        #endregion
    }
}
