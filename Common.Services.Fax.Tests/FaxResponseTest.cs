using Common.Services.Fax.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Common.Types;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
namespace Common.Services.Fax.Tests
{
    
    
    /// <summary>
    ///This is a test class for FaxResponseTest and is intended
    ///to contain all FaxResponseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FaxResponseTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for FaxResponse Constructor
        ///</summary>
        [TestMethod()]
        public void FaxResponseConstructorTest()
        {
            FaxResponse target = new FaxResponse();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Enumeration
        ///</summary>
        [TestMethod()]
        public void EnumerationTest()
        {
            FaxResponse target = new FaxResponse(); // TODO: Initialize to an appropriate value
            Thing expected = null; // TODO: Initialize to an appropriate value
            Thing actual;
            target.Enumeration = expected;
            actual = target.Enumeration;
            Assert.AreEqual(expected, actual);
            Assert.Fail("Should be enumeration not thing");
        }

        /// <summary>
        ///A test for FaxResponse Constructor
        ///</summary>
        [TestMethod()]
        public void FaxResponseConstructorTest1()
        {
            FaxResponse target = new FaxResponse();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Enumeration
        ///</summary>
        [TestMethod()]
        public void EnumerationTest1()
        {
            FaxResponse target = new FaxResponse(); // TODO: Initialize to an appropriate value
            Thing expected = null; // TODO: Initialize to an appropriate value
            Thing actual;
            target.Enumeration = expected;
            actual = target.Enumeration;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
