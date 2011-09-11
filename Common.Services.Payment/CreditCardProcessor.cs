using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{

    public class CreditCardProcessor : BaseCreditCardGateway
    {
        private BaseCreditCardGateway _CurrentProcessor = null;

        public BaseCreditCardGateway CurrentProcessor
        {
            get { return _CurrentProcessor; }
            set { _CurrentProcessor = value; }
        }

        public override bool SupportsRecurring
        {
            get 
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsRecurring;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string GatewayName
        {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.GatewayName;
                }
                else
                {
                    return "Uknown";
                }
            }
        }
        public override Guid GatewayId
        {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.GatewayId;
                }
                else
                {
                    return new Guid();
                }
            }
        }

        public override bool SupportsAuthorize
        {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsAuthorize;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool SupportsCapture 
            {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsCapture;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool SupportsCharge
            {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsCharge;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool SupportsRefund
            {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsRefund;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool SupportsVoid
        {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsVoid;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool SupportsProfile
        {
            get
            {
                if (_CurrentProcessor != null)
                {
                    return _CurrentProcessor.SupportsProfile;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool Authorize(IPaymentData data)
        {
            if (_CurrentProcessor != null)
            {
                return _CurrentProcessor.Authorize(data);
            }
            else
            {
                return false;
            }
        }
        public override bool Capture(IPaymentData data)
        {
            if (_CurrentProcessor != null)
            {
                return _CurrentProcessor.Capture(data);
            }
            else
            {
                return false;
            }
        }
        public override bool Charge(IPaymentData data)
        {
            if (_CurrentProcessor != null)
            {
                return _CurrentProcessor.Charge(data);
            }
            else
            {
                return false;
            }
        }
        public override bool Refund(IPaymentData data)
        {
            if (_CurrentProcessor != null)
            {
                return _CurrentProcessor.Refund(data);
            }
            else
            {
                return false;
            }
        }
        public override bool Void(IPaymentData data)
        {
            if (_CurrentProcessor != null)
            {
                return _CurrentProcessor.Void(data);
            }
            else
            {
                return false;
            }
        }
        public CreditCardProcessor()
        {
        }
        
    }
}
