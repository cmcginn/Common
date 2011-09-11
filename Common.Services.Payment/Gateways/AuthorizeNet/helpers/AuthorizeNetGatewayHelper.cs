using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet;
using AuthorizeNet.APICore;
using System.Xml.Serialization;

namespace Common.Services.Payment.Gateways.AuthNet.helpers
{
    public class AuthorizeNetGatewayHelper
    {
        public const string DUPLICATE_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate record with ID ";
        public const string DUPLICATE_PAYMENT_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate customer payment profile already exists.";
        AuthorizeNet.APICore.merchantAuthenticationType _MerchantAuthenticationType;
        public AuthorizeNet.ServiceMode ServiceMode { get; set; }
        public AuthorizeNet.APICore.merchantAuthenticationType MerchantAuthenticationType
        {
            get
            {
                if (_MerchantAuthenticationType == null)
                {
                    _MerchantAuthenticationType = new AuthorizeNet.APICore.merchantAuthenticationType();
                }
                return _MerchantAuthenticationType;
            }
            set
            {
                _MerchantAuthenticationType = value;
            }
        }

        #region CIM
        public long CreateCustomerProfile(string email, string description,out AuthorizeNet.APICore.messagesType messages)
        {
            long result = 0;       
            string profileId = "0";
            AuthorizeNet.APICore.customerProfileType profile = new AuthorizeNet.APICore.customerProfileType();
            profile.email = email;
            profile.description = description;
            AuthorizeNet.APICore.createCustomerProfileRequest req = new AuthorizeNet.APICore.createCustomerProfileRequest();
            req.profile = profile;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            AuthorizeNet.APICore.createCustomerProfileResponse response = null;
            try
            {
                response = (AuthorizeNet.APICore.createCustomerProfileResponse)util.Send(req);                
                long.TryParse(response.customerProfileId, out result);
            }
            catch (System.InvalidOperationException ex)
            {
              if( ex.Message.Contains( DUPLICATE_PROFILE_MESSAGE ) ) {
                profileId = ex.Message.Replace( DUPLICATE_PROFILE_MESSAGE, String.Empty ).Replace( " already exists.", String.Empty );
                long.TryParse( profileId, out result );
                if( result > 0 ) {
                  response = new AuthorizeNet.APICore.createCustomerProfileResponse();
                  response.messages = new messagesType();
                  response.messages.message = new messagesTypeMessage[1]{new messagesTypeMessage{ code="00000", text="A duplicate profile was found, existing profile will be used"}};
                  response.messages.resultCode = messageTypeEnum.Ok;
                }
              } else
                throw ( ex );
            }
            messages = response.messages;
            return result;
        }
        public AuthorizeNet.APICore.getCustomerProfileResponse GetCustomerProfile(long profileId)
        {
            AuthorizeNet.APICore.getCustomerProfileRequest req = new AuthorizeNet.APICore.getCustomerProfileRequest();
            req.customerProfileId = profileId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.getCustomerProfileResponse)util.Send(req);
        }
        public AuthorizeNet.APICore.customerPaymentProfileMaskedType GetCustomerPaymentProfile(long profileId, long paymentProfileId)
        {
            AuthorizeNet.APICore.getCustomerPaymentProfileRequest req = new AuthorizeNet.APICore.getCustomerPaymentProfileRequest();
            req.merchantAuthentication = MerchantAuthenticationType;
            req.customerProfileId = profileId.ToString();
            req.customerPaymentProfileId = paymentProfileId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return ((AuthorizeNet.APICore.getCustomerPaymentProfileResponse)util.Send(req)).paymentProfile;
        }
        public AuthorizeNet.APICore.customerPaymentProfileMaskedType[] GetCustomerPaymentProfiles(long profileId)
        {
            var profile = GetCustomerProfile(profileId);
            return profile.profile.paymentProfiles;            
        }
        public AuthorizeNet.APICore.createCustomerPaymentProfileResponse CreateCustomerPaymentProfile(AuthorizeNet.APICore.customerPaymentProfileType paymentProfile, long profileId)
        {
            AuthorizeNet.APICore.createCustomerPaymentProfileRequest req = new AuthorizeNet.APICore.createCustomerPaymentProfileRequest();
            req.customerProfileId = profileId.ToString();
            req.paymentProfile = paymentProfile;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.createCustomerPaymentProfileResponse)util.Send(req);
        }
        public AuthorizeNet.APICore.createCustomerProfileTransactionResponse CreateProfileTransaction(AuthorizeNet.APICore.profileTransactionType transaction)
        {
            AuthorizeNet.APICore.createCustomerProfileTransactionRequest req = new AuthorizeNet.APICore.createCustomerProfileTransactionRequest();
            req.transaction = transaction;
            req.merchantAuthentication = MerchantAuthenticationType;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.createCustomerProfileTransactionResponse)util.Send(req);

        }
        public static string Serialize(object target)
        {
            {
                StringBuilder builder = new StringBuilder();
                System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(builder);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(target.GetType());
                serializer.Serialize(xmlWriter, target);
                return builder.ToString();
            };
        }
        #endregion

        #region ARB
        public AuthorizeNet.APICore.ARBCreateSubscriptionResponse CreateARBSubscription(AuthorizeNet.APICore.ARBSubscriptionType subscription)
        {
            AuthorizeNet.APICore.ARBCreateSubscriptionRequest req = new AuthorizeNet.APICore.ARBCreateSubscriptionRequest();            
            req.subscription = subscription;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.ARBCreateSubscriptionResponse)util.Send(req);
        }
        public AuthorizeNet.APICore.ARBUpdateSubscriptionResponse UpdateARBSubscription(long subscriptionId, AuthorizeNet.APICore.ARBSubscriptionType subscription)
        {
            AuthorizeNet.APICore.ARBUpdateSubscriptionRequest req = new AuthorizeNet.APICore.ARBUpdateSubscriptionRequest();
            req.subscriptionId = subscriptionId.ToString();
            req.subscription = subscription;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.ARBUpdateSubscriptionResponse)util.Send(req);
        }

        public AuthorizeNet.APICore.ARBGetSubscriptionStatusResponse GetSubscriptionStatus(long subscriptionId)
        {
            AuthorizeNet.APICore.ARBGetSubscriptionStatusRequest req = new AuthorizeNet.APICore.ARBGetSubscriptionStatusRequest();
            req.subscriptionId = subscriptionId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.ARBGetSubscriptionStatusResponse)util.Send(req);
        }

        public AuthorizeNet.APICore.ARBCancelSubscriptionResponse CancelSubscription(long subscriptionId)
        {
            AuthorizeNet.APICore.ARBCancelSubscriptionRequest req = new AuthorizeNet.APICore.ARBCancelSubscriptionRequest();
            req.subscriptionId = subscriptionId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(ServiceMode, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.ARBCancelSubscriptionResponse)util.Send(req);
        }
        #endregion
    }
}
