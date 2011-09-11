using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Utils.Extensions;
using Common.Services.Payment.Interfaces;
using Common.Services.Payment;
using Microsoft.Practices.Unity;
namespace Common.Services.Payment.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CreditCardGatewayTest
    {
        public class UnityTestDriver
        {
            [Dependency]
            public IPaymentGatewaySettings PaymentGatewaySettings { get; set; }
            [Dependency]
            public ITransactionData TransactionData { get; set; }
            [Dependency]
            public IPaymentData PaymentCardData { get; set; }
            [Dependency]
            public ICustomerData CustomerData { get; set; }
        }
        UnityTestDriver _UnityTestDriver;

        public CreditCardGatewayTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            var bs = new TestBootstrapper();
            bs.Run();
            _UnityTestDriver = bs.Container.Resolve<UnityTestDriver>();
        }
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
            

        }
    }
}
