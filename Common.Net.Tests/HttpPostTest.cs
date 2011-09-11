using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Net.Interfaces;
using Common.Net;
namespace Common.Net.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HttpPostTest
    {
        public IHttpPost GetDefaultHttpPost()
        {
            return new HttpPost();
        }
        public HttpPostTest()
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
        public void Post_WhenUsingFullOverloadParametersWithGoogleURI_NoExceptionsThrown()
        {
            //Arrange
            var target = GetDefaultHttpPost();
            target.PostUri = new Uri("http://www.google.com");
            target.PostEncodingType = PostEncodingTypes.UrlEncoded;
            target.PostType = PostTypes.Post;
            target.Parameters = new Dictionary<string, string>();
            //Act
            target.Post();
            //Assert
            //Self Asserting
        }
        [TestMethod]
        public void Get_WhenUsingFullOverloadParametersWithGoogleURI_AssertResponseNotNull()
        {
            //Arrange
            var target = GetDefaultHttpPost();
            target.PostUri = new Uri("http://www.google.com");
            target.PostEncodingType = PostEncodingTypes.UrlEncoded;
            target.PostType = PostTypes.Get;
            target.Parameters = new Dictionary<string, string>();
            //Act
            var actual = target.Post();
            //Assert
            Assert.IsTrue(actual.Length>0);
        }
    }
}
