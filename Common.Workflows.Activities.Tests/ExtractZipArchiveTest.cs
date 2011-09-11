using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neovolve.Toolkit.Unity;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.Activities;
using Common.Workflows.Activities;
namespace Common.Workflows.Activities.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ExtractZipArchiveTest
    {

        public ExtractZipArchiveTest()
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
        public void TestExtraxctZipArchive_AssertCollectionOfFileSystemInfoReturned()
        {
            //Arrange
            UnityContainerResolver.Resolve();
            var inputs = new Dictionary<string, object>();
            var activity = new ExtractZipArchive();
            
            activity.extractionDirectoryInfo = AppDomain.CurrentDomain.BaseDirectory;
            activity.sourceFileInfo = AppDomain.CurrentDomain.BaseDirectory + "\\Placeholder.txt";
            WorkflowInvoker invoker = new WorkflowInvoker(activity);
            invoker.InvokeCompleted += delegate(object sender, InvokeCompletedEventArgs e)
            {
                if (e.Error != null)
                    throw new System.Exception(e.Error.Message);
            };
            //Act
            var result = invoker.Invoke();
            //Assert
            Assert.IsTrue(((IEnumerable<FileSystemInfo>)result["result"]).Count() == 1);
        }

       

        
    }
}
