using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Gateways.PayPal
{
    public class PaypalPayer
    {
        /// <summary>
        /// Paypal payer 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Paypal payer identifier
        /// </summary>
        public string PayerID { get; set; }
        /// <summary>
        /// Paypal payer country name
        /// </summary>
        public string PaypalCountryName { get; set; }
        /// <summary>
        /// Paypal payer email
        /// </summary>
        public string PayerEmail { get; set; }
        /// <summary>
        /// Paypal payer first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Paypal payer last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Paypal payer company name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Paypal payer address 1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Paypal payer address 2 
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Paypal payer city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Paypal payer state
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Paypal payer zip
        /// </summary>
        public string Zipcode { get; set; }
        /// <summary>
        /// Paypal payer phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Paypal payer country code
        /// </summary>
        public string CountryCode { get; set; }
    }
}
