using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;
namespace Common.Services.Payment
{
    [DataContract( Namespace = "http://Common.Services.Payments" )]
    [KnownType(typeof(CustomerData))]
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
        private IAddressType _Address = new AddressType();
        
        private string _CustomerDescription;
        [DataMember]
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }       
        [DataMember]
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        [DataMember]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [DataMember]
        public string FaxNumber
        {
            get { return _FaxNumber; }
            set { _FaxNumber = value; }
        }
        [DataMember]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [DataMember]
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [DataMember]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        [DataMember]
        public string CustomerDescription
        {
            get { return _CustomerDescription; }
            set { _CustomerDescription = value; }
        }
        [DataMember]
        public IAddressType Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
       
    }
}
