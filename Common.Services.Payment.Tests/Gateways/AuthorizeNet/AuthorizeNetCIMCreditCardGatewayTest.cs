using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Types;
using Common.Services.Payment.Interfaces;
using Common.Services.Payment.Gateways;
using Common.Services.Payment.Gateways.AuthNet;
namespace Common.Services.Payment.Tests.Gateways.AuthorizeNet
{
    [TestClass]
    public class AuthorizeNetCIMCreditCardGatewayTest
    {
        static IPaymentData GetPaymentData()
        {
            var result = new PaymentData();
            result.CardData = new PaymentCardData();
            result.CardData.CardHolderName = "Test Cardholder";
            result.CardData.CardNumber = "4111111111111111";
            result.CardData.CardType = PaymentCardType.Visa;
            result.CardData.ExpirationMonth = 1;
            result.CardData.ExpirationYear = 2020;
            result.CardData.SecurityCode = "123";
            result.GatewaySettings = new PaymentGatewaySettings();
            result.GatewaySettings.EmailCustomer = false;
            result.GatewaySettings.Password = Properties.AuthorizeNet.Default.APIAccountPassword;
            result.GatewaySettings.Username = Properties.AuthorizeNet.Default.APIAccountName;
            result.GatewaySettings.TestMode = true;

            result.Customer = new CustomerData();
            result.Customer.Address = new AddressType();
            result.Customer.Address.AddressLine1 = "123 Test Street";
            result.Customer.Address.City = "Testville";
            result.Customer.Address.Country = "US";
            result.Customer.Address.PostalCode = "32323";
            result.Customer.FirstName = "Test";
            result.Customer.LastName = "Cardholder";
            result.Customer.EmailAddress = Guid.NewGuid().ToString().Replace("-",String.Empty) + "@sxddasdasddonotresolve.com";
            result.Transaction = new TransactionData();
            result.Transaction.Amount = (decimal)0.01;
            result.Transaction.PreviousTransactionReferenceNumber = "0";
            result.CardData.CardHolderFirstName = "Test";
            result.CardData.CardHolderLastName = "Cardholder";
            result.CardData.BillingAddress = result.Customer.Address;
            return result;
        }


        #region Test Helper Methods
        public static IGatewayProfile CreateProfile(ICustomerData data)
        {
            IProfileCreditCardGateway target = new AuthorizeNetCIMCreditCardGateway();
            return target.GetOrCreateCustomerProfile(data);
        }
        public static IGatewayProfile GetGatewayProfile(IGatewayProfile profile)
        {
            IProfileCreditCardGateway target = new AuthorizeNetCIMCreditCardGateway();
            return target.GetCustomerProfile(profile.ProfileId);
        }
        #endregion

        [TestMethod]
        public void AuthorizeTest_WhenValidData_CheckResultIsTrue()
        {
            //Arrange
            var target = new AuthorizeNetCIMCreditCardGateway();
            var paymentData = GetPaymentData();
            //Act
            var actual = target.Authorize(paymentData);
            //Assert
            Assert.IsTrue(actual);
        }      

        [TestMethod]
        public void CreateProfileTest_AssertProfileResponseLengthGreaterThanZero()
        {
            //Arrange
            IGatewayProfile actual = CreateProfile(GetPaymentData().Customer);
            //Assert
            Assert.IsTrue(int.Parse(actual.ProfileId)>0);
        }

        [TestMethod]
        public void GetCustomerProfileTest_WhenProfileExists_AssertProfileIdsMatch()
        {
            //Arrange
            var customer = CreateProfile(GetPaymentData().Customer);
            //Act
            var actual = GetGatewayProfile(customer);
            //Assert
            Assert.IsTrue(customer.ProfileId == actual.ProfileId);
            
        }

        [TestMethod]
        public void AuthorizeTest_WhenDuplicate_CheckResponseStillOk()
        {
            //Arrange
            var target = new AuthorizeNetCIMCreditCardGateway();
            var paymentData = GetPaymentData();
            paymentData.Transaction.Amount = (new System.Random().Next(1, 99) * (decimal)0.01);
            var expected = target.Authorize(paymentData);
            //PreQualify
            Assert.IsTrue(expected);
            paymentData.Transaction.Amount = paymentData.Transaction.Amount + (decimal)0.10;
            //Act
            var actual = target.Authorize(paymentData);
            //Assert
            Assert.IsTrue(actual);

           
        }
    }

   
}
