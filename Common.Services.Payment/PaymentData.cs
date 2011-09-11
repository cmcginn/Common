using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;
using Common.Utils.Extensions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
namespace Common.Services.Payment
{
    /// <summary>
    /// Holds all transactional data needed to perform actions and return results
    /// </summary>
    [DataContract( Namespace = "http://Common.Services.Payments" )]
    [KnownType(typeof(PaymentData))]
    public class PaymentData : IPaymentData
    {
        
        private IPaymentCardData _CardData = new PaymentCardData();
        private ICustomerData _Customer = new CustomerData();
        private IPaymentGatewaySettings _GatewaySettings = new PaymentGatewaySettings();
        private ITransactionData _Transaction = new TransactionData();
        

        public string Id { get; set; }
        [DataMember]
        public IPaymentCardData CardData
        {
            get { return _CardData; }
            set { _CardData = value; }
        }
        [DataMember]        
        public ICustomerData Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        [DataMember]        
        public ITransactionData Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
    }
}
