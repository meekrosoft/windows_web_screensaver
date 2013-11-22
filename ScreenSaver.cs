using System;
using System.Collections.Generic;
using System.Linq;
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

            if (args[0].ToLower().Trim().StartsWith("/c"))
            {
                var configForm = new ConfigForm {Urls = Config.UrlText, Path = Config.ConfigPath};
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    Config.Save(configForm.Urls);
                }
            }
            else if (args[0].ToLower() == "/s")
            {
                StartScreenSaver();
            }
        }

        private static void StartScreenSaver()
        {
            int screenCount = Screen.AllScreens.Length;
            var screensaverForms = new List<ScreensaverForm>();

            // Start the screen saver on all the displays the computer has
            for (int x = 0; x < screenCount; x++)
            {
                screensaverForms.Add(new ScreensaverForm(x, Config.UrlList));
                screensaverForms[x].Show();
            }

            while (true)
            {
                Application.DoEvents();
                // if any screen is not visible then return
                if (screensaverForms.Any(t => !t.Visible))
                {
                    return;
                }
            }
        }
    }
}
