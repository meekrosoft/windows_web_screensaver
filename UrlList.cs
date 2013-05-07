using System.Collections.Generic;

namespace ScreenSaver
{
    internal class UrlList
    {
        private const string _url = "http://news.ycombinator.com";
        private readonly List<string> _urlList = new List<string>();
        private int _listIndex;

        public string get()
        {
            return _urlList[_listIndex];
        }

        public string getNext()
        {
            if (_urlList.Count == 0)
                return _url;

            if (_listIndex >= _urlList.Count)
                _listIndex = 0;

            return _urlList[_listIndex++];
        }

        public void addUrl(string url)
        {
            _urlList.Add(url);
        }
    }
}