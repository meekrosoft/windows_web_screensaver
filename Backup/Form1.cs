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
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		// Store the mouse coordinates
		private Point mouseCoords;
		// Store the number of displays
		private int displayNum;
		// Random number that will change the position of the PictureBox
		Random rand = new Random();

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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(80, 80);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 7000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.Text = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Set the bounds of the form, fill all the screen
			this.Bounds = Screen.AllScreens[displayNum].Bounds;
			// The form should be on top of all
			TopMost = true;
			// We don't need the cursor
			Cursor.Hide();
		}

		private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// If mouseCoords is empty don't close the screen saver
			if(!mouseCoords.IsEmpty)
			{
				// If the mouse actually moved
				if(mouseCoords != new Point(e.X, e.Y))
				{
					// Close
					this.Close();
				}
			}
			// Set the new point where the mouse is
			mouseCoords = new Point(e.X, e.Y);
		}

		// If a key was pressed...
		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// ...close the screen saver
			this.Close();
		}

		// Every 7 seconds...
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			// set the new X position of the PictureBox to random number
			int newX = rand.Next(0, (this.Size.Width - pictureBox1.Size.Width));
			// set the new Y position of the PictureBox to random number
			int newY = rand.Next(0, (this.Size.Height - pictureBox1.Size.Height));
			// and actually move the PictureBox to the new position
			pictureBox1.Location = new Point(newX, newY);
		}
	}
}
