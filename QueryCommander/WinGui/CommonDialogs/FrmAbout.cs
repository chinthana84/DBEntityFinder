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
	/// Summary description for FrmAbout.
	/// </summary>
	public class FrmAbout : FrmBaseDialog
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblVersion;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			lblProductName.Text = System.Windows.Forms.Application.ProductName;
			lblVersion.Text = System.Windows.Forms.Application.ProductVersion;
			//lblDescription.Text = System.Windows.Forms.Application.pro
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmAbout));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.btnOk = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblProductName = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pictureBox2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.linkLabel3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.linkLabel2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 288);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(584, 128);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Credits and thankfulness";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(168, 76);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(56, 40);
			this.pictureBox2.TabIndex = 11;
			this.pictureBox2.TabStop = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(24, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 24);
			this.label3.TabIndex = 10;
			this.label3.Text = "Christian Halvarsson";
			// 
			// linkLabel3
			// 
			this.linkLabel3.Location = new System.Drawing.Point(160, 52);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(320, 16);
			this.linkLabel3.TabIndex = 9;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "SharpDevelop is an open source IDE for the .NET platform.";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "#develop";
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(160, 28);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(320, 16);
			this.linkLabel2.TabIndex = 7;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "The DockPanel control";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Weifen Luo";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(600, 128);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(16, 432);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(128, 16);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Please report bugs";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(496, 424);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 24);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblDescription);
			this.groupBox2.Controls.Add(this.lblVersion);
			this.groupBox2.Controls.Add(this.lblProductName);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(8, 136);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(584, 144);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Product detail";
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(112, 80);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(464, 48);
			this.lblDescription.TabIndex = 5;
			this.lblDescription.Text = "QueryCommader is a SQL editor similar to the Microsoft Query Analyzer, in a Visua" +
				"l Studio type of enviroment. QueryCommander includes features like intellisense," +
				" automatic comment header, XML-documentation helper etc ";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(112, 56);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(328, 16);
			this.lblVersion.TabIndex = 4;
			this.lblVersion.Text = "...";
			// 
			// lblProductName
			// 
			this.lblProductName.Location = new System.Drawing.Point(112, 32);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(328, 16);
			this.lblProductName.TabIndex = 3;
			this.lblProductName.Text = "...";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 16);
			this.label6.TabIndex = 2;
			this.label6.Text = "Description:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Version:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Product name:";
			// 
			// FrmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 462);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FrmAbout";
			this.Text = "About QueryCommander";
			this.Load += new System.EventHandler(this.FrmAbout_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "http://www.freewebs.com/weifenluo/DockPanel.htm";
			System.Diagnostics.Process.Start(target);
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "https://sourceforge.net/projects/sharpdevelop/";
			System.Diagnostics.Process.Start(target);
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string target = "mailto:qcsupport@rockwolf.com?subject=QueryCommander Support";
			System.Diagnostics.Process.Start(target);

		}

		private void FrmAbout_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
