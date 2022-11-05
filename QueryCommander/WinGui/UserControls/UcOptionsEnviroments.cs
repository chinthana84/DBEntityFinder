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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace QueryCommander.WinGui.UserControls
{
	/// <summary>
	/// Summary description for UcOptionsEnviroments.
	/// </summary>
	public class UcOptionsEnviroments : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private System.Windows.Forms.Label label9;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UcOptionsEnviroments()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UcOptionsEnviroments));
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblProductName = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.label8 = new System.Windows.Forms.Label();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.label9 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(0, 0);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(328, 280);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lblDescription);
			this.tabPage1.Controls.Add(this.lblVersion);
			this.tabPage1.Controls.Add(this.lblProductName);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(320, 254);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Product Detail";
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(96, 64);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(224, 96);
			this.lblDescription.TabIndex = 11;
			this.lblDescription.Text = "QueryCommader is a SQL editor similar to the Microsoft Query Analyzer, in a Visua" +
				"l Studio type of enviroment. QueryCommander includes features like intellisense," +
				" automatic comment header, XML-documentation helper etc ";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(96, 40);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(192, 16);
			this.lblVersion.TabIndex = 10;
			this.lblVersion.Text = "...";
			// 
			// lblProductName
			// 
			this.lblProductName.Location = new System.Drawing.Point(96, 16);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(192, 16);
			this.lblProductName.TabIndex = 9;
			this.lblProductName.Text = "...";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "Description:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Version:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Product name:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.linkLabel5);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.linkLabel3);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.pictureBox2);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.linkLabel1);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.linkLabel4);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(320, 254);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Credits and thankfulness";
			// 
			// linkLabel3
			// 
			this.linkLabel3.Location = new System.Drawing.Point(152, 40);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(136, 16);
			this.linkLabel3.TabIndex = 19;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "ic#code";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(16, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 24);
			this.label2.TabIndex = 18;
			this.label2.Text = "Christian Halvarsson";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(160, 128);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(56, 32);
			this.pictureBox2.TabIndex = 17;
			this.pictureBox2.TabStop = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(16, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 24);
			this.label3.TabIndex = 16;
			this.label3.Text = "#develop team";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(152, 64);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(136, 16);
			this.linkLabel1.TabIndex = 15;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Use IRichEditOle from C#";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(16, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 16);
			this.label7.TabIndex = 14;
			this.label7.Text = "John Fisher";
			// 
			// linkLabel4
			// 
			this.linkLabel4.Location = new System.Drawing.Point(152, 16);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(136, 16);
			this.linkLabel4.TabIndex = 13;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "The DockManager control";
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(16, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 16);
			this.label8.TabIndex = 12;
			this.label8.Text = "Weifen Luo";
			// 
			// linkLabel5
			// 
			this.linkLabel5.Location = new System.Drawing.Point(152, 88);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(136, 16);
			this.linkLabel5.TabIndex = 21;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "Rockwolf";
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(16, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 16);
			this.label9.TabIndex = 20;
			this.label9.Text = "Bo && Sten";
			// 
			// UcOptionsEnviroments
			// 
			this.Controls.Add(this.tabControl1);
			this.Name = "UcOptionsEnviroments";
			this.Size = new System.Drawing.Size(480, 352);
			this.Load += new System.EventHandler(this.UcOptionsEnviroments_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel4_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "http://www.codeproject.com/cs/miscctrl/DockManager.asp?target=Weifen%7CLuo";
			System.Diagnostics.Process.Start(target);
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "http://www.icsharpcode.net/opensource/sd/";
			System.Diagnostics.Process.Start(target);
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "http://www.codeproject.com/cs/miscctrl/richtextboxplus.asp";
			System.Diagnostics.Process.Start(target);
		}

		private void UcOptionsEnviroments_Load(object sender, System.EventArgs e)
		{
			lblProductName.Text = System.Windows.Forms.Application.ProductName;
			lblVersion.Text = System.Windows.Forms.Application.ProductVersion;
		}

		private void linkLabel5_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "http://www.rockwolf.com";
			System.Diagnostics.Process.Start(target);
		}
	}
}
