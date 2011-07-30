using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Web;
using Common.Services.Payment.AuthorizeNetCIMGateway;
namespace Common.Services.Payment.Gateways.AuthorizeNet
{
    public class AuthorizeNetCIMCreditCardGateway:BaseCreditCardGateway
    {
        #region SoapAPIUtilities
        
        #endregion

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
        public override bool SupportsProfile
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

        

    }
}
