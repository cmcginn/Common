using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Collections.Generic;
using Common.Services.Fax.Interfaces;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Common.Utils.Extensions;
namespace Common.Services.Fax.Tests
{
    
    
    /// <summary>
    ///This is a test class for FaxServiceTest and is intended
    ///to contain all FaxServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FaxServiceTest
    {
        UnityTestDriver _UnityTestDriver;
        public class UnityTestDriver
        {
            FaxService _FaxService;
            [Dependency]
            public FaxService FaxService
            {
                get { return _FaxService; }
                set { _FaxService = value; }
            }
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            var bs = new TestBootstrapper();
            bs.Run();
           // bs.Container.RegisterType<IFaxService>();
            _UnityTestDriver = bs.Container.Resolve<UnityTestDriver>();
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual FaxService CreateFaxService()
        {
            return _UnityTestDriver.FaxService as FaxService;
        }
        static Dictionary<string, FileSystemInfo> GetValidationSchemaInfo()
        {
            var result = new Dictionary<string, FileSystemInfo>();
            result.Add(FaxService.TransmitRequestPreProcessValidationKey, new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\PreProcess"));
            result.Add(FaxService.TransmitRequestPostProcessValidationKey, new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\PostProcess"));
            return result;
        }
        /// <summary>
        ///A test for Transmit
        ///</summary>
        ///<remarks>
        ///When TransmitRequest is called, assuming no exceptions are thrown, a ResponseStatusType must be assigned indicating "Pending"
        /// </remarks>
        [TestMethod()]
        public void TransmitRequestTest_CheckFaxJobResponseExists()
        {
            //Arrange
            FaxService target = CreateFaxService();
            target.Message = XElement.Load("ValidFaxType.xml").Deserialize<FaxMessage>();
            target.ValidationSchemaInfo = GetValidationSchemaInfo();
            //PreQualify
            Assert.IsTrue(target.Message.ResponseStatusType == null);
            //Act
            target.Transmit();
            //Assert
            Assert.IsTrue(target.Message.ResponseStatusType!= null);
           
        }
        /// <summary>
        ///A test for Transmit
        ///</summary>
        ///<remarks>
        ///All fax jobs should be validated against an xsd file before physically transmitting the request. If this validation fails, the request should
        ///result in a FaxServiceException containing the details of the validation errors, in this case ValidationEventArgs messages.
        /// </remarks>
        [TestMethod()]
        [ExpectedException(typeof(FaxServiceException))]
        public void TransmitRequestTest_WhenInvalidSchema_ExpectException()
        {
            //Arrange
            FaxService target = CreateFaxService();
            target.Message = XElement.Load("InvalidFaxType.xml").Deserialize<FaxMessage>();
            target.ValidationSchemaInfo = GetValidationSchemaInfo();
            //PreQualify
            Assert.IsTrue(target.Message.ResponseStatusType == null);
            //Act
            target.Transmit();
            //Assert
            //Self asserting expect exception
        }

        /// <summary>
        ///A test for UpdateStatus
        ///</summary>
        [TestMethod()]
        public void UpdateResponseTest()
        {
            //Arrange
            FaxService target = CreateFaxService();
            target.Message = XElement.Load("PendingResponseType.xml").Deserialize<FaxMessage>();            
            //Act
            target.QueryResponse();
            Assert.IsInstanceOfType(target.Message.ResponseStatusType, typeof(Common.Types.ResponseStatusType));
        }
    }
}
