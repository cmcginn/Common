using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Web;
using Common.Services.Payment.AuthorizeNetCIMGateway;

namespace Common.Services.Payment.Gateways.AuthNet
{
    public class CIMHelper
    {
        public static MerchantAuthenticationType GetMerchantAuthenticationType(string merchantName,string transactionKey)
        {
            MerchantAuthenticationType result = new MerchantAuthenticationType();
            result.name = merchantName;
            result.transactionKey = transactionKey;
            return result;
        }
        public static ServiceSoapClient GetService()
        {
            var result = new ServiceSoapClient();
            return result;
        }

        public static CreateCustomerProfileResponseType CreateCustomerProfile(MerchantAuthenticationType merchantAuthenticationType,string email,string description)
        {
            CustomerProfileType result = new CustomerProfileType();
            result.email = email;
            result.description = description;            
            CreateCustomerProfileResponseType response = GetService().CreateCustomerProfile(merchantAuthenticationType, result, ValidationModeEnum.none);
            return response;
        } 
    }
}
