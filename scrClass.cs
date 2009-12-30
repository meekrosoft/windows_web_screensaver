using System;
using System.Windows.Forms;

namespace scrSaver
{
	public class scrClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			// If Windows passes arguments...
			if (args.Length > 0)
			{
				// If argument is /c...
				if (args[0].ToLower().Trim().Substring(0,2) == "/c")
				{
					// We will add code here later
				}
				// If argument is /s...
				else if (args[0].ToLower() == "/s")
				{
					// Start the screen saver on all the displays the computer has
					for (int x = Screen.AllScreens.GetLowerBound(0); x <= Screen.AllScreens.GetUpperBound(0); x++) 
					{
						// Pass the number of display to Form1()
						System.Windows.Forms.Application.Run(new Form1(x));
					}
				}
			}
			// If there is no argument just start the screen saver
			else
			{
				for (int x = Screen.AllScreens.GetLowerBound(0); x <= Screen.AllScreens.GetUpperBound(0); x++)
				{
					System.Windows.Forms.Application.Run(new Form1(x));				
				}
			}
		}
	}
}