
using System.Windows.Forms;

namespace WebScreenSaver
{
    public partial class ScreensaverForm : Form
    {
        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._newPageTimer = new System.Windows.Forms.Timer(this.components);
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // newPageTimer
            // 
            this._newPageTimer.Enabled = true;
            this._newPageTimer.Interval = 20000;
            this._newPageTimer.Tick += new System.EventHandler(this.newPageTimerTick);
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
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);
        }

        #endregion

    }
}