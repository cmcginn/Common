using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public enum RecurringPaymentIntervals
    {
        Days,
        Months
    }
    public interface IRecurringPaymentData : IPaymentData
    {
        RecurringPaymentIntervals RecurringPaymentInterval { get; set; }
        int? Intervals { get; set; }
        decimal? RecurringIntervalPrice { get; set; }
        DateTime? StartDate { get; set; }
        string SubscriptionStatus { get; set; }
        decimal? TrialIntervalPrice { get; set; }
        int? TrialIntervals { get; set; }
    }
}
