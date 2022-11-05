// *******************************************************************************
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// *******************************************************************************
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QueryCommander
{

	/// <summary>
	///  Exception dialog
	/// </summary>
	/// <revision author="wmmabet" date="2004-01-13">Removed warning/fixed xml-comments.</revision>
	public class FrmExceptionMessage :  System.Windows.Forms.Form
	{
		private object currentForm = null;
		private Exception exception = null;
		private bool _continue;

		private System.Windows.Forms.TextBox TxtDetail;
		private System.Windows.Forms.Button BtnQuit;
		private System.Windows.Forms.Button BtnContinue;
		private System.Windows.Forms.Button BtnDetails;
		private System.Windows.Forms.Label LblMessage;
		private System.Windows.Forms.ImageList ImgList1;
		private System.Windows.Forms.PictureBox PicIcon0;
		private System.Windows.Forms.PictureBox PicIcon1;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="currentForm">Current form</param>
		/// <param name="e">Exception</param>
		public FrmExceptionMessage(object currentForm, Exception e)
		{
			this.currentForm=currentForm;
			this.exception=e;

			InitializeComponent();
			this.Height=280;
			displayError();

			
			BtnContinue.Focus();
		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmExceptionMessage));
			this.TxtDetail = new System.Windows.Forms.TextBox();
			this.BtnQuit = new System.Windows.Forms.Button();
			this.BtnContinue = new System.Windows.Forms.Button();
			this.BtnDetails = new System.Windows.Forms.Button();
			this.LblMessage = new System.Windows.Forms.Label();
			this.PicIcon0 = new System.Windows.Forms.PictureBox();
			this.ImgList1 = new System.Windows.Forms.ImageList(this.components);
			this.PicIcon1 = new System.Windows.Forms.PictureBox();
			this.Cancel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// TxtDetail
			// 
			this.TxtDetail.Location = new System.Drawing.Point(8, 264);
			this.TxtDetail.Multiline = true;
			this.TxtDetail.Name = "TxtDetail";
			this.TxtDetail.ReadOnly = true;
			this.TxtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDetail.Size = new System.Drawing.Size(424, 192);
			this.TxtDetail.TabIndex = 11;
			this.TxtDetail.Text = "textBox1";
			this.TxtDetail.WordWrap = false;
			// 
			// BtnQuit
			// 
			this.BtnQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnQuit.Location = new System.Drawing.Point(328, 192);
			this.BtnQuit.Name = "BtnQuit";
			this.BtnQuit.Size = new System.Drawing.Size(96, 24);
			this.BtnQuit.TabIndex = 2;
			this.BtnQuit.Text = "Close";
			this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
			// 
			// BtnContinue
			// 
			this.BtnContinue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnContinue.Location = new System.Drawing.Point(216, 192);
			this.BtnContinue.Name = "BtnContinue";
			this.BtnContinue.Size = new System.Drawing.Size(96, 24);
			this.BtnContinue.TabIndex = 0;
			this.BtnContinue.Text = "Continue";
			this.BtnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
			// 
			// BtnDetails
			// 
			this.BtnDetails.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnDetails.Location = new System.Drawing.Point(8, 192);
			this.BtnDetails.Name = "BtnDetails";
			this.BtnDetails.Size = new System.Drawing.Size(96, 24);
			this.BtnDetails.TabIndex = 1;
			this.BtnDetails.Text = "Details";
			this.BtnDetails.Click += new System.EventHandler(this.BtnDetails_Click);
			// 
			// LblMessage
			// 
			this.LblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblMessage.Location = new System.Drawing.Point(8, 128);
			this.LblMessage.Name = "LblMessage";
			this.LblMessage.Size = new System.Drawing.Size(424, 64);
			this.LblMessage.TabIndex = 0;
			this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PicIcon0
			// 
			this.PicIcon0.Location = new System.Drawing.Point(8, 16);
			this.PicIcon0.Name = "PicIcon0";
			this.PicIcon0.Size = new System.Drawing.Size(31, 31);
			this.PicIcon0.TabIndex = 6;
			this.PicIcon0.TabStop = false;
			this.PicIcon0.Visible = false;
			// 
			// ImgList1
			// 
			this.ImgList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ImgList1.ImageSize = new System.Drawing.Size(9, 6);
			this.ImgList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PicIcon1
			// 
			this.PicIcon1.Location = new System.Drawing.Point(8, 16);
			this.PicIcon1.Name = "PicIcon1";
			this.PicIcon1.Size = new System.Drawing.Size(31, 31);
			this.PicIcon1.TabIndex = 12;
			this.PicIcon1.TabStop = false;
			this.PicIcon1.Visible = false;
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Cancel.Location = new System.Drawing.Point(128, 192);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(24, 16);
			this.Cancel.TabIndex = 15;
			this.Cancel.Text = "button1";
			this.Cancel.Visible = false;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(448, 120);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 16;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(464, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(448, 120);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 17;
			this.pictureBox2.TabStop = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(8, 224);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(280, 16);
			this.linkLabel1.TabIndex = 18;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Please report this bug";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// FrmExceptionMessage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(442, 464);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.PicIcon1);
			this.Controls.Add(this.TxtDetail);
			this.Controls.Add(this.PicIcon0);
			this.Controls.Add(this.BtnQuit);
			this.Controls.Add(this.BtnContinue);
			this.Controls.Add(this.BtnDetails);
			this.Controls.Add(this.LblMessage);
			this.Controls.Add(this.Cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmExceptionMessage";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QueryCommander";
			this.Load += new System.EventHandler(this.FrmException_Load);
			this.ResumeLayout(false);

		}
		
		
		#endregion


		private void BtnDetails_Click(object sender, System.EventArgs e)
		{
			this.Height=(this.Height==496)?280:496;
		}

		private void BtnQuit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void FrmException_Load(object sender, System.EventArgs e)
		{
			if(this.exception is System.Data.SqlClient.SqlException)
			{
				_continue=true;
				BtnQuit.Visible=false;
				BtnContinue.Location = BtnQuit.Location;
				pictureBox2.Location = pictureBox1.Location;
				pictureBox1.Visible = false;
				linkLabel1.Visible=false;
			}
			this.Refresh();
		}
		private void displayError()
		{
			string Message="";
			string Detail="";
			Cursor.Current = Cursors.Default;
			
			//BtnDetails.Image=ImgList1.Images[0];
			Detail = (exception.InnerException==null)?exception.StackTrace.ToString():exception.InnerException.ToString();
			Message= exception.Message;
			
			TxtDetail.Lines=formatMessage(Detail);
			int numberOfBreaks = Message.Split('\n').Length-1;
			
			LblMessage.Text=String.Format(Message);

			LblMessage.Height = LblMessage.Height + (numberOfBreaks * 5);
			this.Height = this.Height + (numberOfBreaks * 5);
			TxtDetail.Top = TxtDetail.Top + (numberOfBreaks * 5);
			//PnlButtons.Top = PnlButtons.Top + (numberOfBreaks * 5);
			
			this.Focus();
		}

			
		private void SetCaption(string text)
		{
			this.Text = text + " - " + exception.GetType().Name;
		}

		private void BtnContinue_Click(object sender, System.EventArgs e)
		{
			if(!_continue)
				((Form)currentForm).Close();
	
			this.Close();
		}

		private string[] formatMessage(string message)
		{
			// Formatts an string, and returns a string array.
			// Delimiter=\n
			return message.Split('\n');
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string body = "Exception report:" + DateTime.Now + "%0D%0APlease add some comments to this exception.%0D%0A%0D%0AQueryCommander Version:%0D%0A"+System.Windows.Forms.Application.ProductVersion+"%0D%0A%0D%0AMessage:%0D%0A" + LblMessage.Text+"%0D%0A%0D%0AStack trace:" +TxtDetail.Text.Replace("at ","%0D%0Aat ");
			string target = "mailto:qcsupport@rockwolf.com?subject=QueryCommander Bugreport&body=" + body;//"http://workspaces.gotdotnet.com/QueryCommander";
			System.Diagnostics.Process.Start(target);
		}
	}
}
