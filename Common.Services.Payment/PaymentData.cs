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
        

        public string Id { get; set; }
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
        public ITransactionData Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
        

    }
}
