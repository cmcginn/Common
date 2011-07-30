using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Common.Services.Fax.Interfaces;
using Common.Types;
namespace Common.Services.Fax.Tests
{
    /// <summary>
    /// Summary description for FaxServiceInterfaceTests
    /// </summary>
    [TestClass]
    public class FaxServiceInterfaceTests
    {
        public class TestDriver
        {
            public TestDriver()
            {
                var catalog = new DirectoryCatalog(System.AppDomain.CurrentDomain.BaseDirectory);
                var container = new CompositionContainer(catalog);
                container.ComposeParts(new object[] { this });
            }
            //[Import]
            //public IFaxServiceRequest FaxServiceRequest { get; set; }
        }
        public FaxServiceInterfaceTests()
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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SendFaxTest_WhenUsingValidFaxServiceRequest()
        {
            //var driver = new TestDriver();
            //var faxServiceRequest = driver.FaxServiceRequest;
            //Assert.IsInstanceOfType(faxServiceRequest, typeof(IFaxServiceRequest));
        }
    }
}
