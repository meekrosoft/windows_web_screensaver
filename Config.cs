using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebScreenSaver
{
    internal static class Config
    {
        static private List<string> _urls;
        public const int Timeout = 10000;

        static Config()
        { 
            _urls = Urls;
            CloseWhenMouseMove = true;
        }

        public static string ConfigPath
        {
            get
            {
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location),
                                              "url.txt");
            }
        }

        public static List<string> Urls
        {
            get
            {
                return (from url in UrlText
                       where !url.StartsWith("#")
                       select url).ToList();
            }
        }

        public static IEnumerable<string> UrlText
        {
            get
            {
                IEnumerable<string> urls = new List<string>();

                if (File.Exists(ConfigPath))
                {
                    urls = from url in File.ReadAllLines(ConfigPath)
                           where !String.IsNullOrEmpty(url)
                           select url.Trim();
                }
                else
                {
                    File.WriteAllLines(ConfigPath, UrlList.DefaultUrls);
                }
                return urls;
            }
        }

        public static UrlList UrlList { get { return new UrlList(_urls); } }

        public static void Save(IEnumerable<string> urls)
        {
            var text = string.Join(Environment.NewLine, urls.ToArray());
            File.WriteAllText(ConfigPath, text);
        }

        public static bool CloseWhenMouseMove { get; set; }
    }
}
