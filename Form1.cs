using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace scrSaver
{
	public class Form1 : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		// Store the mouse coordinates
		private Point mouseCoords;
		// Store the number of displays
		private int displayNum;
        private WebBrowser webBrowser1;
		// Random number that will change the position of the PictureBox
		Random rand = new Random();

        private String[] pages = {
                                   "http://news.google.com/",
                                   "http://news.bbc.co.uk/",
                                   "http://www.timesonline.co.uk/tol/news/"
                                 };
        private int pageIndex = 0;


		// Accept one argurment - the number of displays
		public Form1(int display)
		{
			InitializeComponent();
			// Assign the number to an accessible variable
			displayNum = display;
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(400, 400);
            this.webBrowser1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Set the bounds of the form, fill all the screen
			this.Bounds = Screen.AllScreens[displayNum].Bounds;
            webBrowser1.Bounds = Screen.AllScreens[displayNum].Bounds;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.PreviewKeyDown += new PreviewKeyDownEventHandler(webBrowser1_PreviewKeyDown);
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

            nextPage();
 			// The form should be on top of all
			TopMost = true;
			// We don't need the cursor
			Cursor.Hide();
		}

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            doc.MouseMove += new HtmlElementEventHandler(doc_MouseMove);
        }

        void doc_MouseMove(object sender, HtmlElementEventArgs e)
        {
            // If mouseCoords is empty don't close the screen saver
            if (!mouseCoords.IsEmpty)
            {
                // If the mouse actually moved more than 10 pixes in any direction
                if (Math.Abs(mouseCoords.X - e.MousePosition.X) > 10 
                    ||  Math.Abs(mouseCoords.Y - e.MousePosition.Y) > 10)
                {
                    // Close
                    this.Close();
                }
            }
            // Set the new point where the mouse is
            mouseCoords = new Point(e.MousePosition.X, e.MousePosition.Y);
        }

        void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // ...close the screen saver
            this.Close();
        }

		// Every 20 seconds...
		private void timer1_Tick(object sender, System.EventArgs e)
		{
            nextPage();
		}

        private void nextPage()
        {
            Uri uri = new Uri(pages[pageIndex]);
            webBrowser1.Navigate(uri);
            pageIndex++;
            pageIndex = pageIndex % pages.Length;
        }
	}
}
