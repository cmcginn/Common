using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{

    public class CustomerData : ICustomerData
    {
        private string _MerchantId = string.Empty;        
        private string _Company = string.Empty;        
        private string _CustomerId = string.Empty;
        private string _EmailAddress = string.Empty;
        private string _FaxNumber = string.Empty;
        private string _FirstName = string.Empty;
        private string _IPAddress = string.Empty;
        private string _LastName = string.Empty;
        private string _PhoneNumber = string.Empty;
        private IAddressType _Address;
        
        private string _CustomerDescription;

        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }       
        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }        
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string FaxNumber
        {
            get { return _FaxNumber; }
            set { _FaxNumber = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }        
        public string CustomerDescription
        {
            get { return _CustomerDescription; }
            set { _CustomerDescription = value; }
        }
        public IAddressType Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
       
    }
}
