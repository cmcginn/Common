using System;
using System.Collections.Generic;
using System.Text;
namespace Common.Services.Payment.Interfaces
{

    public class CustomerData : ICustomerData
    {
        private string _AddressLine1 = string.Empty;
        private string _AddressLine2 = string.Empty;
        private string _City = string.Empty;
        private string _Company = string.Empty;
        private string _Country = string.Empty;
        private string _CustomerId = string.Empty;
        private string _EmailAddress = string.Empty;
        private string _FaxNumber = string.Empty;
        private string _FirstName = string.Empty;
        private string _IPAddress = string.Empty;
        private string _LastName = string.Empty;
        private string _PhoneNumber = string.Empty;
        private string _PostalCode = string.Empty;
        private string _State = string.Empty;

        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }
        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
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
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
     
    }
}
