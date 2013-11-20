using System.Collections.Generic;

namespace WebScreenSaver
{
    internal class UrlList
    {
        static public readonly string[] DefaultUrls = new[] { "http://news.ycombinator.com", "http://whatthecommit.com/" };
        private int _listIndex;
        private List<string> _urlList = new List<string>();

        public UrlList()
        {
            Assign(DefaultUrls);
        }

        public UrlList(IEnumerable<string> urls)
        {
            Assign(urls);
        }

        public string GetNext()
        {
            if (_listIndex >= _urlList.Count)
                _listIndex = 0;

            return _urlList[_listIndex++];
        }

        public void Add(string url)
        {
            _urlList.Add(url);
        }

        public void Assign(IEnumerable<string> urls)
        {
            _urlList = new List<string>(urls);
            if (_urlList.Count == 0)
                _urlList.AddRange(DefaultUrls);
        }
    }
}