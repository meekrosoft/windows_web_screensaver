using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WebScreenSaver
{
    public class ScreenSaver
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                StartScreenSaver();
                return;
            }

            if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
            {
                var configForm = new ConfigForm();
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else if (args[0].ToLower() == "/s")
            {
                StartScreenSaver();
            }
        }

        private static UrlList LoadUrlList()
        {
            IEnumerable<string> urls = null;

            string configFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "url.txt");
            if (File.Exists(configFilePath))
            {
                urls = from url in File.ReadAllLines(configFilePath)
                       where !string.IsNullOrEmpty(url) && !url.Trim().StartsWith("#")
                       select url.Trim();
            }

            return new UrlList(urls);
        }

        private static void StartScreenSaver()
        {
            int screenCount = Screen.AllScreens.Length;
            var screensaverForms = new ScreensaverForm[screenCount];

            // Start the screen saver on all the displays the computer has
            for (int x = 0; x < screenCount; x++)
            {
                screensaverForms[x] = new ScreensaverForm(x, LoadUrlList());
                screensaverForms[x].Show();
            }

            while (true)
            {
                Application.DoEvents();
                // if any screen is not visible then return
                for (int x = 0; x < screenCount; x++)
                {
                    if (screensaverForms[x].Visible == false) return;
                }
            }
        }
    }
}