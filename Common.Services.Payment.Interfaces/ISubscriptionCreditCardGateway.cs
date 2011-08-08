using System;
using System.Collections.Generic;
namespace Common.Services.Payment.Interfaces
{
    public interface ISubscriptionCreditCardGateway : ICreditCardGateway
    {
        bool CreateSubscription(IRecurringPaymentData paymentData);
        bool UpdateSubscription(IRecurringPaymentData paymentData);
        bool GetSubscriptionStatus(IRecurringPaymentData paymentData);
        bool CancelSubscription(IRecurringPaymentData paymentData);
    }
}