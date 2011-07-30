using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.PayPal;
using Common.Services.Payment.Interfaces;
using Common.Services.Payment;
using Common.Services.Payment.Gateways.PayPal;
namespace Common.Services.Payment.Gateways
{
    public class ExpressCheckoutCreditCardGateway:BaseCreditCardGateway
    {
        
        public const string GATEWAY_ID_STRING = "2813AEB4-A7EC-4268-9702-04AB58900422";
        public const string GATEWAY_NAME = "PayPal Express Checkout";
        public string API_VERSION = "63";

        PaypalPayer _PayPalPayer;
        PaymentActionCodeType _PaymentActionCodeType;

        #region PayPal methods
        PayPalAPIInterfaceClient GetPayPalInterfaceClient(IPaymentGatewaySettings settings)
        {
            PayPalAPIInterfaceClient result;
            if (settings.TestMode)
                result = new PayPalAPIInterfaceClient("PayPalAPISandbox");
            else
                result = new PayPalAPIInterfaceClient("PayPalAPI");
            return result;
            
        }
        PayPalAPIAAInterfaceClient GetPayPalAAInterfaceClient(IPaymentGatewaySettings settings)
        {
            PayPalAPIAAInterfaceClient result;
            if (settings.TestMode)
                result = new PayPalAPIAAInterfaceClient("PayPalAPIAASandbox");
            else
                result = new PayPalAPIAAInterfaceClient("PayPalAPIAA");
            return result;
        }
        GetExpressCheckoutDetailsResponseType GetExpressCheckoutDetailsRequest(IPaymentGatewaySettings settings, string token)
        {
            GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq();
            GetExpressCheckoutDetailsResponseType result = null;
            var request = new GetExpressCheckoutDetailsRequestType();
            req.GetExpressCheckoutDetailsRequest = request;
            request.Token = token;
            request.Version = API_VERSION;
            var service = GetPayPalAAInterfaceClient(settings);
            CustomSecurityHeaderType customSecurityHeader = new CustomSecurityHeaderType();
            GetExpressCheckoutDetailsResponseType response = service.GetExpressCheckoutDetails(ref customSecurityHeader, req);
            result = response;
            return response;
        }
        CustomSecurityHeaderType GetCustomSecurityHeader(IPaymentGatewaySettings settings)
        {
            CustomSecurityHeaderType result = new CustomSecurityHeaderType();
            result.Credentials.Username = settings.Username;
            result.Credentials.Password = settings.Password;
            result.Credentials.Signature = Properties.PayPalExpress.Default.Signature;
            result.Credentials.Subject = String.Empty;
            return result;
        }
        //public PaypalPayer GetExpressCheckout(IPaymentGatewaySettings settings,string token)
        //{
            
        //    GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq();
        //    GetExpressCheckoutDetailsRequestType request = new GetExpressCheckoutDetailsRequestType();
        //    req.GetExpressCheckoutDetailsRequest = request;
        //    CustomSecurityHeaderType securityHeader = GetCustomSecurityHeader(settings);
        //    request.Token = token;
        //    request.Version = API_VERSION;

        //    GetExpressCheckoutDetailsResponseType response = GetPayPalAAInterfaceClient(settings).GetExpressCheckoutDetails(ref securityHeader, req);

        //    if (response.Ack != AckCodeType.Success || response.Ack != AckCodeType.SuccessWithWarning)
        //        throw new PaymentServiceException(String.Format("Could not obtain Express Checkoutinformation. PayPal response was : {0}",
        //            String.Join(response.Errors.Select(n => String.Format("Error Code: {0} Error Message: {1};", n.ErrorCode, n.LongMessage)).ToArray())));
        //    if (!PaypalHelper.CheckSuccess(response, out error))
        //        throw new NopException(error);

        //    PaypalPayer payer = new PaypalPayer();
        //    payer.PayerEmail = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Payer;
        //    payer.FirstName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerName.FirstName;
        //    payer.LastName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerName.LastName;
        //    payer.CompanyName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerBusiness;
        //    payer.Address1 = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Street1;
        //    payer.Address2 = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Street2;
        //    payer.City = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.CityName;
        //    payer.State = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.StateOrProvince;
        //    payer.Zipcode = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.PostalCode;
        //    payer.Phone = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.ContactPhone;
        //    payer.PaypalCountryName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.CountryName;
        //    payer.CountryCode = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Country.ToString();
        //    payer.PayerID = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
        //    payer.Token = response.GetExpressCheckoutDetailsResponseDetails.Token;
        //    return payer;
        //}
        void DoExpressCheckout(string token)
        {
            DoExpressCheckoutPaymentReq req = new DoExpressCheckoutPaymentReq();
            DoExpressCheckoutPaymentRequestType request = new DoExpressCheckoutPaymentRequestType();
            req.DoExpressCheckoutPaymentRequest = request;
            request.Version = API_VERSION;
            DoExpressCheckoutPaymentRequestDetailsType details = new DoExpressCheckoutPaymentRequestDetailsType();
            request.DoExpressCheckoutPaymentRequestDetails = details;
            details.PaymentAction = _PaymentActionCodeType;
            details.PaymentActionSpecified = true;
            details.Token = token;
            
        }
        #endregion

        #region BaseCreditCardGateway implementation
        public override Guid GatewayId
        {
            get { return new Guid(GATEWAY_ID_STRING); }
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

        public override bool SupportsProfile
        {
            get { return false; }
        }
    }
}
