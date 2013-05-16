using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebScreenSaver
{
    internal class WebpageView : UserControl
    {
        private readonly Timer _timer = new Timer();
        private readonly UrlList _urlList;
        private readonly WebBrowser _webBrowser = new WebBrowser();

        public WebpageView(UrlList urlList)
        {
            _urlList = urlList;

            _webBrowser.Dock = DockStyle.Fill;
            _webBrowser.DocumentCompleted += OnWebBrowserDocumentCompleted;
            _webBrowser.PreviewKeyDown += (o, e) => OnPreviewKeyDown(e);
            _webBrowser.ScriptErrorsSuppressed = true;
            Controls.Add(_webBrowser);
            _webBrowser.Dock = DockStyle.Fill;

            _timer.Interval = 2000;
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void OnWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Debug.Assert(_webBrowser.Document != null, "_webBrowser.Document != null");
            SubscriptDocumentEvents(_webBrowser.Document);
        }

        private void SubscriptDocumentEvents(HtmlDocument doc)
        {
            doc.MouseMove += OnDocumentMouseMove;
            doc.Click += OnDocumentClick;
        }

        private void OnDocumentClick(object sender, HtmlElementEventArgs e)
        {
            _timer.Stop();
            DisplayNextPage();
            _timer.Start();
        }

        private void UnsubscriptDocumentEvents(HtmlDocument doc)
        {
            doc.MouseMove -= OnDocumentMouseMove;
        }

        private void OnDocumentMouseMove(object sender, HtmlElementEventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(e.MouseButtonsPressed, 1, e.MousePosition.X, e.MousePosition.Y, 0);
            OnMouseMove(mouseEventArgs);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            DisplayNextPage();
        }

        private void DisplayNextPage()
        {
            if (_webBrowser.Document != null)
                UnsubscriptDocumentEvents(_webBrowser.Document);
            var uri = new Uri(_urlList.GetNext());
            _webBrowser.Navigate(uri);
            _webBrowser.Refresh();
        }
    }
}
