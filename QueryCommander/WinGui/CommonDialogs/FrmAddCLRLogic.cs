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
	/// Summary description for FrmAddCLRLogic.
	/// </summary>
	public class FrmAddCLRLogic : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbSecLevel;
		private System.Windows.Forms.ComboBox cbType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TreeView tvAssembly;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Splitter splitter1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmAddCLRLogic()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbSecLevel = new System.Windows.Forms.ComboBox();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tvAssembly = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(360, 72);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Browse";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select assemlby:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 24);
			this.label3.TabIndex = 3;
			this.label3.Text = "Security level:";
			// 
			// cbSecLevel
			// 
			this.cbSecLevel.Items.AddRange(new object[] {
															"SAFE",
															"EXTERNAL_ACCESS",
															"UNSAFE "});
			this.cbSecLevel.Location = new System.Drawing.Point(104, 112);
			this.cbSecLevel.Name = "cbSecLevel";
			this.cbSecLevel.Size = new System.Drawing.Size(320, 21);
			this.cbSecLevel.TabIndex = 4;
			this.cbSecLevel.Text = "UNSAFE";
			// 
			// cbType
			// 
			this.cbType.Items.AddRange(new object[] {
														"Stored procedure",
														"Trigger",
														"User defined function",
														"User defined type"});
			this.cbType.Location = new System.Drawing.Point(104, 136);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(320, 21);
			this.cbType.TabIndex = 6;
			this.cbType.Text = "Stored procedure";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 24);
			this.label4.TabIndex = 5;
			this.label4.Text = "Type:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tvAssembly);
			this.groupBox1.Location = new System.Drawing.Point(8, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416, 312);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select method";
			// 
			// tvAssembly
			// 
			this.tvAssembly.ImageIndex = -1;
			this.tvAssembly.Location = new System.Drawing.Point(8, 24);
			this.tvAssembly.Name = "tvAssembly";
			this.tvAssembly.SelectedImageIndex = -1;
			this.tvAssembly.Size = new System.Drawing.Size(400, 280);
			this.tvAssembly.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(104, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(256, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "[None]";
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(240, 488);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(88, 24);
			this.btnCreate.TabIndex = 8;
			this.btnCreate.Text = "Create script";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(336, 488);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.Color.White;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(432, 64);
			this.splitter1.TabIndex = 10;
			this.splitter1.TabStop = false;
			// 
			// FrmAddCLRLogic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 526);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbSecLevel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Name = "FrmAddCLRLogic";
			this.Text = "FrmAddCLRLogic";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
