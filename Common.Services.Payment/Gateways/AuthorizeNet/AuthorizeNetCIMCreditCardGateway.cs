using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Web;
using Common.Services.Payment.AuthorizeNetCIMGateway;
using AuthorizeNet;
using AuthorizeNet.APICore;
using AuthorizeNet.Helpers;
using Common.Services.Payment.Gateways.AuthNet.helpers;
namespace Common.Services.Payment.Gateways.AuthNet
{
    public class AuthorizeNetCIMCreditCardGateway:BaseProfileCreditCardGateway
    {
        

        #region public interface
        public const string GATEWAY_ID_STRING = "7107FFA1-D376-422E-9DF5-A15DD6909E9C";
        const string DUPLICATE_PROFILE_MESSAGE = "Error processing request: E00039 - A duplicate record with ID ";
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
            throw new NotImplementedException();
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
        #endregion


        #region Class Memebers

        ICustomerGateway _CustomerGateway;
        ICustomerGateway CustomerGateway
        {
            get 
            {
                if(_CustomerGateway==null)
                    _CustomerGateway = new CustomerGateway(Properties.AuthorizeNet.Default.APIAccountName, Properties.AuthorizeNet.Default.APIAccountPassword);
                return _CustomerGateway; 
            }
            set { _CustomerGateway = value; }
        }
        //{
        //    ICustomerGateway result = 
        //    return result;
        //}
        #endregion
        public IGatewayProfile GetCustomerProfile(string id)
        {
            IGatewayProfile result = new GatewayProfile();
            var customer = CustomerGateway.GetCustomer(id);
            result.ProfileCustomerData = new CustomerData();
            if (customer.BillingAddress != null)
            {
                result.ProfileCustomerData.Address = customer.BillingAddress.MapAddressType();
                result.ProfileCustomerData.FirstName = customer.BillingAddress.First;
                result.ProfileCustomerData.LastName = customer.BillingAddress.Last;
            }
            result.ProfileId = customer.ProfileID;
            result.ProfileCustomerData.CustomerId = customer.ID;
            result.ProfileCustomerData.EmailAddress = customer.Email;
            result.PaymentProfiles = new List<IGatewayPaymentProfile>();
            customer.PaymentProfiles.ToList().ForEach(paymentProfile =>
                {
                    var gatewayPaymentProfile = new GatewayPaymentProfile();
                    gatewayPaymentProfile.CustomerData = new CustomerData();
                    gatewayPaymentProfile.CustomerData.Address= paymentProfile.BillingAddress.MapAddressType();
                    gatewayPaymentProfile.PaymentCardData = paymentProfile.MapPaymentCardData();
                    gatewayPaymentProfile.CustomerData.CustomerId = customer.ID;
                    gatewayPaymentProfile.PaymentProfileId = paymentProfile.ProfileID;
                    result.PaymentProfiles.Add(gatewayPaymentProfile);
                });
            return result;

        }
        public override IGatewayProfile GetOrCreateProfile(ICustomerData customerData)
        {
            IGatewayProfile result = new GatewayProfile();
            try
            {
                var customer = CustomerGateway.CreateCustomer(customerData.EmailAddress, customerData.CustomerDescription);
                CustomerGateway.CreateCustomer(customerData.EmailAddress, customerData.CustomerDescription);
                result.ProfileId = customer.ProfileID;
                return result;
            }
            catch (System.InvalidOperationException ex)
            {
                if(ex.Message.Contains(DUPLICATE_PROFILE_MESSAGE))
                {
                    var profileId = ex.Message.Replace(DUPLICATE_PROFILE_MESSAGE,"").Replace(" already exists.","");
                    result = GetCustomerProfile(profileId);                  
                  
                }
              
            }
            return result;
        }
    }
}
