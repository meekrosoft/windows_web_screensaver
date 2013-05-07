using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaver
{
    public class ScreensaverForm : Form
    {
        // Store the number of displays
        private readonly int _thisDisplayIdId;
        private readonly UrlList _urlList;
        private WebBrowser _webBrowser;
        private IContainer components;
        // Store the mouse coordinates
        private Point mouseCoords;
        private Timer newPageTimer;

        internal ScreensaverForm(int thisDisplayId, UrlList urlList)
        {
            InitializeComponent();
            // Assign the number to an accessible variable
            _thisDisplayIdId = thisDisplayId;
            _urlList = urlList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the bounds of the form, fill all the screen
            Bounds = Screen.AllScreens[_thisDisplayIdId].Bounds;
            _webBrowser.Left = 0;
            _webBrowser.Top = 0;
            _webBrowser.Width = Width;
            _webBrowser.Height = Height;
            _webBrowser.ScriptErrorsSuppressed = true;
            _webBrowser.PreviewKeyDown += webBrowser1_PreviewKeyDown;
            _webBrowser.DocumentCompleted += webBrowser1_DocumentCompleted;

            displayNextPage();
            // The form should be on top of all
            TopMost = true;
            // We don't need the cursor
            Cursor.Hide();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = _webBrowser.Document;
            doc.MouseMove += doc_MouseMove;
        }

        private void doc_MouseMove(object sender, HtmlElementEventArgs e)
        {
            // If mouseCoords is empty don't close the screen saver
            if (!mouseCoords.IsEmpty)
            {
                // If the mouse actually moved more than 10 pixes in any direction
                if (Math.Abs(mouseCoords.X - e.MousePosition.X) > 10
                    || Math.Abs(mouseCoords.Y - e.MousePosition.Y) > 10)
                {
                    // Close
                    Close();
                }
            }
            // Set the new point where the mouse is
            mouseCoords = new Point(e.MousePosition.X, e.MousePosition.Y);
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // ...close the screen saver
            Close();
        }

        // Triggered by timer component every 20 seconds...
        private void newPageTimerTick(object sender, EventArgs e)
        {
            displayNextPage();
        }

        private void displayNextPage()
        {
            var uri = new Uri(_urlList.getNext());
            _webBrowser.Navigate(uri);
            if (_webBrowser.Url == uri)
                _webBrowser.Refresh();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.newPageTimer = new System.Windows.Forms.Timer(this.components);
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // newPageTimer
            // 
            this.newPageTimer.Enabled = true;
            this.newPageTimer.Interval = 20000;
            this.newPageTimer.Tick += new System.EventHandler(this.newPageTimerTick);
            // 
            // _webBrowser
            // 
            this._webBrowser.Location = new System.Drawing.Point(0, 0);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(480, 462);
            this._webBrowser.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this._webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}