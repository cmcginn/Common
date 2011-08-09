using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;
using Common.Utils.Extensions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace Common.Services.Payment
{
    public class RecurringPaymentData : IRecurringPaymentData
    {
        public string Id { get; set; }
        public RecurringPaymentIntervals RecurringPaymentInterval { get; set; }
        public string SubscriptionStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Intervals { get; set; }
        public int? TrialIntervals { get; set; }
        public decimal? TrialIntervalPrice { get; set; }
        public decimal? RecurringIntervalPrice { get; set; }
        public IPaymentCardData CardData { get; set; }
        public ICustomerData Customer { get; set; }
        public ITransactionData Transaction { get; set; }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
