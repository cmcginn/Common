using System;

namespace Common.Services.Payment.Interfaces
{

    public interface IPaymentGatewaySettings
    {
        bool EmailCustomer { get; set; }
        string Password { get; set; }
        bool TestMode { get; set; }
        Uri UrlLive { get; set; }
        Uri UrlTest { get; set; }
        string Username { get; set; }
    }
}
