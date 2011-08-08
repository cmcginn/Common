using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
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
    }
}
