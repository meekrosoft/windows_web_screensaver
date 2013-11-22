using System.Collections.Generic;
using System.Linq;

namespace WebScreenSaver
{
    public class Webpage
    {
        static public int DefaultTimeout = 10;
        public Webpage(string url)
        {
            if (url.Contains(","))
            {
                var temp = url.Split(new[] { ',' }, 2);
                Url = temp[0];
            }
            Url = url;
            Timeout = DefaultTimeout;
        }
        public Webpage(string url, int timeout)
        {
            Url = url;
            Timeout = timeout;
        }
        public string Url { get; private set; }
        public int Timeout { get; private set; }
    }

    public class UrlList
    {
        static public readonly string[] DefaultUrls = new[] { "http://news.ycombinator.com", "http://whatthecommit.com/" };
        private int _listIndex;
        private List<Webpage> _urlList = new List<Webpage>();

        public UrlList()
        {
            Assign(DefaultUrls);
        }

        public UrlList(IEnumerable<string> urls)
        {
            Assign(urls);
        }

        public Webpage GetNext()
        {
            if (_listIndex >= _urlList.Count)
                _listIndex = 0;

            return _urlList[_listIndex++];
        }

        public void Add(string url)
        {
            _urlList.Add(new Webpage(url));
        }

        public void Assign(IEnumerable<string> urls)
        {
            _urlList = urls.Select(url => new Webpage(url)).ToList();

            if (_urlList.Count == 0)
                _urlList.AddRange(DefaultUrls.Select(url => new Webpage(url)));
        }
    }
}