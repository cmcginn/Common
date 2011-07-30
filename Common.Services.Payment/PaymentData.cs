using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;

namespace Common.Services.Payment
{
    /// <summary>
    /// Holds all transactional data needed to perform actions and return results
    /// </summary>
    public class PaymentData : IPaymentData
    {
        
        private IPaymentCardData _CardData = new PaymentCardData();
        private ICustomerData _Customer = new CustomerData();
        private IPaymentGatewaySettings _GatewaySettings = new PaymentGatewaySettings();
        private ITransactionData _Transaction = new TransactionData();
        private ITransactionResultData _TransactionResult = new TransactionResultData();
        
        public IPaymentCardData CardData
        {
            get { return _CardData; }
            set { _CardData = value; }
        }
        public ICustomerData Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        public IPaymentGatewaySettings GatewaySettings
        {
            get { return _GatewaySettings; }
            set { _GatewaySettings = value; }
        }
        public ITransactionData Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
        public ITransactionResultData TransactionResult
        {
            get { return _TransactionResult; }
            set { _TransactionResult = value; }
        }

    }
}
