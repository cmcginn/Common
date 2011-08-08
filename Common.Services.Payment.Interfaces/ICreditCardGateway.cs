using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public interface ICreditCardGateway
    {
        // generate a unique ID for your processor for this method
        Guid GatewayId { get; }
        IPaymentGatewaySettings GatewaySettings { get; set; }
        // a user friendly display name for your gateway
        string GatewayName { get; }

        // Use these properties to help out your users. Hide buttons
        // that your gateway doesn't support. No need to display a VOID
        // button to the user if they can't ever get a successful response.
        bool SupportsAuthorize { get; }
        bool SupportsCapture { get; }
        bool SupportsCharge { get; }
        bool SupportsRefund { get; }
        bool SupportsVoid { get; }
        bool SupportsProfile { get;}
        bool SupportsRecurring { get; }
        // These methods do the real work of communicating with 
        // outside services based on the 
        bool Authorize(IPaymentData data);
        bool Capture(IPaymentData data);
        bool Charge(IPaymentData data);
        bool Refund(IPaymentData data);
        bool Void(IPaymentData data);

    }
}
