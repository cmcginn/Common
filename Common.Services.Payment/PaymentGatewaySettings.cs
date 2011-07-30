using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{
   
    public class PaymentGatewaySettings : IPaymentGatewaySettings
    {
        private bool _EmailCustomer = false;
        private string _Password = string.Empty;
        private bool _TestMode = false;
        private Uri _UrlLive; //= new Uri(string.Empty);
        private Uri _UrlTest; //= new Uri(string.Empty);        
        private string _Username = string.Empty;


        public bool EmailCustomer
        {
            get { return _EmailCustomer; }
            set { _EmailCustomer = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public bool TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; }
        }
        public Uri UrlLive
        {
            get { return _UrlLive; }
            set { _UrlLive = value; }
        }
        public Uri UrlTest
        {
            get { return _UrlTest; }
            set { _UrlTest = value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }        

    }
}
