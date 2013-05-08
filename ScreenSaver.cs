using System;
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
            // If Windows passes arguments...
            if (args.Length > 0)
            {
                // If argument is /c...
                if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
                {
                    MessageBox.Show("Configuration", "Configuration");
                }
                    // If argument is /s...
                else if (args[0].ToLower() == "/s")
                {
                    StartScreenSaver();
                }
            }
                // If there is no argument just start the screen saver
            else
            {
                StartScreenSaver();
            }
        }

        private static UrlList CreateUrlList()
        {
            var urlList = new UrlList();

            string configFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "url.txt");
            if (File.Exists(configFilePath))
            {
                var urls = File.ReadAllLines(configFilePath).Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s));
                foreach (var url in urls)
                {
                    if (!url.StartsWith("#"))
                        urlList.Add(url);
                }
            }
            else
            {
                urlList.Add(@"http://whatthecommit.com/");
            }
            return urlList;
        }

        private static void StartScreenSaver()
        {
            int screenCount = Screen.AllScreens.Length;
            var screensaverForms = new ScreensaverForm[screenCount];

            // Start the screen saver on all the displays the computer has
            for (int x = 0; x < screenCount; x++)
            {
                screensaverForms[x] = new ScreensaverForm(x, CreateUrlList());
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