using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Types;
using Common.Utils;
namespace Common.Testing
{
    public class DataHelper
    {
        public static PostalAddress GetPostalAddress()
        {
            var result = new PostalAddress();
            result.AddressCountry = new Country();
            result.AddressCountry.Name="US";
            result.AddressLocality = "New York";
            result.AddressRegion = "NY";
            result.StreetAddress = "123 Main Street.";
            result.Telephone = "111-222-3333";
            result.PostalCode = "10003";
            return result;
        }
        public static Person GetPerson()
        {
            var result = new Person();
            result.FirstName = "John";
            result.MiddleName = "Q.";
            result.LastName = "Test";
            result.Address = new List<PostalAddress>();
            result.Address.Add(GetPostalAddress());
            return result;
        }
        public static ElectronicPaymentCardData GetVisaElectronicPaymentCardData()
        {
            var result = new ElectronicPaymentCardData();
            result.Cardholder = "John Q. Test";
            result.CardType = "Visa";
            result.CardNumber = "4111111111111111";
            result.CVV = "123";
            result.ExpirationMonth = 12;
            result.ExpirationYear = 2020;
            return result;
        }
       
    }
}
