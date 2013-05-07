using System.Collections.Generic;

namespace WebScreenSaver
{
    internal class UrlList
    {
        private const string Default = "http://news.ycombinator.com";
        private readonly List<string> _urlList = new List<string>();
        private int _listIndex;

        public string GetNext()
        {
            if (_urlList.Count == 0)
                return Default;

            if (_listIndex >= _urlList.Count)
                _listIndex = 0;

            return _urlList[_listIndex++];
        }

        public void Add(string url)
        {
            _urlList.Add(url);
        }
    }
}