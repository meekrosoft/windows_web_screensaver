using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebScreenSaver
{
    public partial class ScreensaverForm
    {
        // Store the number of displays
        private readonly int _thisDisplayIdId;
        private readonly WebpageView _view;
        // Store the mouse coordinates
        private Point _mouseCoords;

        internal ScreensaverForm(int thisDisplayId, WebpageView view)
        {
            InitializeComponent();
            // Assign the number to an accessible variable
            _thisDisplayIdId = thisDisplayId;
            _view = view;
            LostFocus += (o, e) => Close();
        }

        public bool CloseWhenMouseMove { get; set; }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // Set the bounds of the form, fill all the screen
            Bounds = Screen.AllScreens[_thisDisplayIdId].Bounds;

            SuspendLayout();
            Controls.Add(_view);
            _view.Left = 0;
            _view.Top = 0;
            _view.Width = Width;
            _view.Height = Height;
            _view.PreviewKeyDown += OnWebBrowserPreviewKeyDown;
            _view.MouseMove += OnViewMouseMove;
            _view.Visible = true;
            ResumeLayout();

            TopMost = true;
            Cursor.Hide();
        }

        private void OnViewMouseMove(object sender, MouseEventArgs e)
        {
            if (!CloseWhenMouseMove)
                return;
            // If mouseCoords is empty don't close the screen saver
            if (!_mouseCoords.IsEmpty)
            {
                // If the mouse actually moved more than 10 pixes in any direction
                if (Math.Abs(_mouseCoords.X - e.X) > 10
                    || Math.Abs(_mouseCoords.Y - e.Y) > 10)
                {
                    Close();
                }
            }
            // Set the new point where the mouse is
            _mouseCoords = new Point(e.X, e.Y);
        }

        private void OnWebBrowserPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Close();
        }
    }
}