//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Common.Types;
//using Common.Services.Payment.Interfaces;
//using Common.Services.Payment.Gateways;

//namespace Common.Services.Payment.Tests.Gateways.AuthorizeNet
//{
//    [TestClass]
//    public class AuthorizeNetCreditCardGatewayTest
//    {
//        static IPaymentData GetPaymentData()
//        {
//            var result = new PaymentData();
//            result.CardData = new PaymentCardData();
//            result.CardData.CardHolderName = "Test Cardholder";
//            result.CardData.CardNumber = "4111111111111111";
//            result.CardData.CardType = PaymentCardType.Visa;
//            result.CardData.ExpirationMonth = 1;
//            result.CardData.ExpirationYear = 2020;
//            result.CardData.SecurityCode = "123";
//            //result.GatewaySettings = new PaymentGatewaySettings();
//            //result.GatewaySettings.EmailCustomer = false;
//            //result.GatewaySettings.Password = Properties.AuthorizeNet.Default.APIAccountPassword;
//            //result.GatewaySettings.Username = Properties.AuthorizeNet.Default.APIAccountName;
//            //result.GatewaySettings.TestMode = true;

//            result.Customer = new CustomerData();
//            result.Customer.Address.AddressLine1 = "123 Test Street";
//            result.Customer.Address.City = "Testville";
//            result.Customer.Address.Country = "US";
//            result.Customer.Address.PostalCode = "32323";
//            result.Customer.FirstName = "Test";
//            result.Customer.LastName = "Cardholder";
//            result.Transaction = new TransactionData();
//            result.Transaction.Amount = (decimal)0.01;
//            result.Transaction.PreviousTransactionReferenceNumber = "0";
//            return result;
//        }
//        [TestMethod]
//        public void AuthorizeTest_WhenValidData_CheckResultIsTrue()
//        {
//            //Arrange
//            var target = new AuthorizeNetCreditCardGateway();
//            var paymentData = GetPaymentData();
//            //Act
//            var actual = target.Authorize(paymentData);
//            //Assert
//            Assert.IsTrue(actual);
//        }
//    }
//}
