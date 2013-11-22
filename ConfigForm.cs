using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WebScreenSaver
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        public IEnumerable<string> Urls
        {
            get
            {
                return
                    from line in
                        urlText.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    select line.Trim();
            }
            set
            {
                urlText.Clear();
                foreach (string line in value)
                {
                    urlText.AppendText(line + Environment.NewLine);
                }
            }
        }

        public string Path
        {
            get { return pathLabel.Text; }
            set { pathLabel.Text = value; }
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}