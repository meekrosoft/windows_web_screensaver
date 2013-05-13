using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebScreenSaver
{
    static internal class Config
    {
        public static string Path
        {
            get { return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "url.txt"); }
        }

        public static IEnumerable<string> Urls
        {
            get
            {
                return from url in UrlText
                       where !url.StartsWith("#")
                       select url;
            }
        }

        public static IEnumerable<string> UrlText
        {
            get
            {
                IEnumerable<string> urls = null;

                if (File.Exists(Path))
                {
                    urls = from url in File.ReadAllLines(Path)
                           where !String.IsNullOrEmpty(url)
                           select url.Trim();
                }
                return urls;
            }
        }

        public static void Save(IEnumerable<string> urls)
        {
            string text = urls.Aggregate(String.Empty, (current, url) => current + (url + Environment.NewLine));
            File.WriteAllText(Path, text);
        }
    }
}