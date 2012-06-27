using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenSaver;

namespace ScreenSaver
{
    /// <summary>
    /// Summary description for UrlListTests
    /// </summary>
    [TestClass]
    public class UrlListTests
    {
        UrlList list = new UrlList();
        public UrlListTests()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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
        public void GIVEN_new_list_WHEN_getNext_called_THEN_returns_HackerNews_url()
        {
            Assert.AreEqual("http://news.ycombinator.com", list.getNext());
        }

        [TestMethod]
        public void GIVEN_list_with_one_url_WHEN_getNext_called_THEN_returns_correct_url()
        {   
            list.addUrl("http://www.slb.com");
            Assert.AreEqual("http://www.slb.com", list.getNext());
        }

        [TestMethod]
        public void GIVEN_list_with_two_urls_WHEN_getNext_called_THEN_returns_urls_in_sequence()
        {
            list.addUrl("http://www.slb.com");
            list.addUrl("http://www.google.com");
            Assert.AreEqual("http://www.slb.com", list.getNext());
            Assert.AreEqual("http://www.google.com", list.getNext());
            Assert.AreEqual("http://www.slb.com", list.getNext());
        }

        [TestMethod]
        public void GIVEN_list_with_one_url_WHEN_getNext_called_twice_THEN_returns_same_url()
        {
            list.addUrl("http://www.slb.com");
            Assert.AreEqual("http://www.slb.com", list.getNext());
            Assert.AreEqual("http://www.slb.com", list.getNext());
        }

        [TestMethod]
        public void GIVEN_list_with_two_urls_WHEN_get_called_twice_THEN_returns_first_url_twice()
        {
            list.addUrl("http://www.slb.com");
            list.addUrl("http://www.google.com");
            Assert.AreEqual("http://www.slb.com", list.get());
            Assert.AreEqual("http://www.slb.com", list.get());
        }
    }
}
