using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebScreenSaver;

namespace ScreensaverTests
{
    [TestClass]
    public class UrlListTests
    {
        private readonly UrlList list = new UrlList();

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GIVEN_new_list_WHEN_getNext_called_THEN_returns_HackerNews_url()
        {
            Assert.AreEqual("http://news.ycombinator.com", list.GetNext());
        }

        [TestMethod]
        public void GIVEN_list_with_one_url_WHEN_getNext_called_THEN_returns_correct_url()
        {
            list.Add("http://www.slb.com");
            Assert.AreEqual("http://www.slb.com", list.GetNext());
        }

        [TestMethod]
        public void GIVEN_list_with_two_urls_WHEN_getNext_called_THEN_returns_urls_in_sequence()
        {
            list.Add("http://www.slb.com");
            list.Add("http://www.google.com");
            Assert.AreEqual("http://www.slb.com", list.GetNext());
            Assert.AreEqual("http://www.google.com", list.GetNext());
            Assert.AreEqual("http://www.slb.com", list.GetNext());
        }

        [TestMethod]
        public void GIVEN_list_with_one_url_WHEN_getNext_called_twice_THEN_returns_same_url()
        {
            list.Add("http://www.slb.com");
            Assert.AreEqual("http://www.slb.com", list.GetNext());
            Assert.AreEqual("http://www.slb.com", list.GetNext());
        }
    }
}