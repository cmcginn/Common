using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Net.Interfaces;
using Common.IO.Interfaces;
using Common.Types;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.ServiceLocation;
namespace Common.IO.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HttpFileSystemServiceTest
    {
        public class TestDriver
        {  
            public TestDriver() 
            {
                FileSystemService = new HttpFileSystemService();
                ((HttpFileSystemService)FileSystemService).HttpPost = new Common.Net.HttpPost();
            }
            public HttpFileSystemService FileSystemService { get; set; }
        }
        TestDriver GetTestDriver { get; set; }
        public HttpFileSystemServiceTest()
        {
            IUnityContainer _Container;
            _Container = new UnityContainer();
            UnityConfigurationSection section
                 = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            section.Configure(_Container);
            //GetTestDriver = _Container.Resolve<TestDriver>();
            //Hardwired because code contract debugging slowness
            GetTestDriver = new TestDriver();
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
        public void GetFileStreamTest_WhenRequestingGoogle_AssertFileExists()
        {
            //Arrange
            var target = GetTestDriver; 
            //Act
            var actual = target.FileSystemService.LoadFile(new Uri("http://www.google.com"),new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\targetFile"));
            //Assert
            Assert.IsTrue(actual.Exists);
        }      
        [TestMethod]
        [Description("Requires MVC project executed and running to serve expected DOCX file")]        
        public void GetFileStreamTest_WhenDOCXFile_AssertEncodedCorrect()
        {
            //Arrange
            var target = GetTestDriver;
            //Act
            var actual = target.FileSystemService.LoadFile(new Uri("http://localhost:4321/Content/TestFiles/Sunday.docx"), new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\targetFile.docx"));
            //Assert
            //checking encoding will take some manual intervention as life is not perfect
            Assert.IsTrue(actual.Exists);
        }
    }
}
