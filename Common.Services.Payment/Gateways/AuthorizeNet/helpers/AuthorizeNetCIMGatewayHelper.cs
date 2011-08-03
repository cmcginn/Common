using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet;
using AuthorizeNet.APICore;
namespace Common.Services.Payment.Gateways.AuthNet.helpers
{
    public class AuthorizeNetCIMGatewayHelper
    {
        const string E00039_DUPLICATE_PROFILE = "Error processing request: E00039 - A duplicate record with ID";
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

        public long CreateCustomerProfile(string email, string description)
        {
            long result = 0;
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
                long.TryParse(response.customerProfileId, out result);
            }
            catch (System.InvalidOperationException ex)
            {
                if (ex.Message.Contains(E00039_DUPLICATE_PROFILE))
                {
                    profileId = ex.Message.Replace(E00039_DUPLICATE_PROFILE, String.Empty).Replace(" already exists.", String.Empty);
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
        public AuthorizeNet.APICore.createCustomerPaymentProfileResponse CreateCustomerPaymentProfile(AuthorizeNet.APICore.customerPaymentProfileType paymentProfile, long profileId)
        {
            AuthorizeNet.APICore.createCustomerPaymentProfileRequest req = new AuthorizeNet.APICore.createCustomerPaymentProfileRequest();
            req.customerProfileId = profileId.ToString();
            req.paymentProfile = paymentProfile;
            AuthorizeNet.HttpXmlUtility util = new AuthorizeNet.HttpXmlUtility(AuthorizeNet.ServiceMode.Test, MerchantAuthenticationType.name, MerchantAuthenticationType.transactionKey);
            return (AuthorizeNet.APICore.createCustomerPaymentProfileResponse)util.Send(req);
        }
    }
}
