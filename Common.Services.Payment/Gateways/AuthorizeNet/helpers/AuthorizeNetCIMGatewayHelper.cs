using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet;
using AuthorizeNet.APICore;
using System.Xml.Serialization;

namespace Common.Services.Payment.Gateways.AuthNet.helpers
{
    public class AuthorizeNetCIMGatewayHelper
    {
        public const string DUPLICATE_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate record with ID ";
        public const string DUPLICATE_PAYMENT_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate customer payment profile already exists.";
        AuthorizeNet.APICore.merchantAuthenticationType _MerchantAuthenticationType;
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
        public long CreateCustomerProfile(string email, string description,out string messages)
        {
            long result = 0;
            messages = string.Empty;
            string profileId = "0";
            AuthorizeNet.APICore.customerProfileType profile = new AuthorizeNet.APICore.customerProfileType();
            profile.email = email;
            profile.description = description;
            AuthorizeNet.APICore.createCustomerProfileRequest req = new AuthorizeNet.APICore.createCustomerProfileRequest();
            req.profile = profile;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);

            try
            {
                var response = (AuthorizeNet.APICore.createCustomerProfileResponse)util.Send(req);
                messages = Serialize(response);
                long.TryParse(response.customerProfileId, out result);
            }
            catch (System.InvalidOperationException ex)
            {
                if (ex.Message.Contains(DUPLICATE_PROFILE_MESSAGE))
                {
                    messages = messages + ex.Message;
                    profileId = ex.Message.Replace(DUPLICATE_PROFILE_MESSAGE, String.Empty).Replace(" already exists.", String.Empty);
                    long.TryParse(profileId, out result);
                }
            }
            return result;
        }
        public AuthorizeNet.APICore.getCustomerProfileResponse GetCustomerProfile(long profileId)
        {
            AuthorizeNet.APICore.getCustomerProfileRequest req = new AuthorizeNet.APICore.getCustomerProfileRequest();
            req.customerProfileId = profileId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.getCustomerProfileResponse)util.Send(req);
        }
        public AuthorizeNet.APICore.customerPaymentProfileMaskedType GetCustomerPaymentProfile(long profileId, long paymentProfileId)
        {
            AuthorizeNet.APICore.getCustomerPaymentProfileRequest req = new AuthorizeNet.APICore.getCustomerPaymentProfileRequest();
            req.merchantAuthentication = MerchantAuthenticationType;
            req.customerProfileId = profileId.ToString();
            req.customerPaymentProfileId = paymentProfileId.ToString();
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
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
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.createCustomerPaymentProfileResponse)util.Send(req);
        }
        public AuthorizeNet.APICore.createCustomerProfileTransactionResponse CreateProfileTransaction(AuthorizeNet.APICore.profileTransactionType transaction)
        {
            AuthorizeNet.APICore.createCustomerProfileTransactionRequest req = new AuthorizeNet.APICore.createCustomerProfileTransactionRequest();
            req.transaction = transaction;
            req.merchantAuthentication = MerchantAuthenticationType;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
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
    }
}
