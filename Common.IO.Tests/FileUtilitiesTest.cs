using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.IO;
using Common.IO.Interfaces;
namespace Common.IO.Tests
{
    /// <summary>
    /// Summary description for FileUtilitiesTest
    /// </summary>
    [TestClass]
    public class FileUtilitiesTest
    {
        public TestDriver GetTestDriver { get; set; }
        public class TestDriver
        {
            public TestDriver()
            {
                FileUtilities = new FileUtilities();
            }
            public FileUtilities FileUtilities { get; set; }
        }

        public FileUtilitiesTest()
        {
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
        public void ExtractAll_WhenUsingWellKnownArchiveWithFilesAndDirectory_AssertFileSystemInfosExist()
        {
            //Arrange
            IZipFileUtilities target = GetTestDriver.FileUtilities;
            var zipFilePath = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\ZipContents.Zip");
            var extractionLocation = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            Assert.IsTrue(zipFilePath.Exists,"Test pre-requisites failed zipFilePath could not be found");
            Assert.IsTrue(extractionLocation.Exists,"Test pre-requisites failed extractionLocation could not be found");
            //Act
            var actual = target.ExtractAll(zipFilePath,extractionLocation);
            //Assert
            Assert.IsTrue(actual.ToList().TrueForAll(n => n.Exists));
        }
    }
}
