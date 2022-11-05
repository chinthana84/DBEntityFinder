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
	/// Summary description for UcOptionsQuerySettings.
	/// </summary>
	public class UcOptionsQuerySettings : System.Windows.Forms.UserControl
	{
		public System.Windows.Forms.CheckBox chbShowCommentHeader;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtDiffPercent;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox1;
		public System.Windows.Forms.CheckBox chbRunWithIOStat;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UcOptionsQuerySettings()
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
			this.chbShowCommentHeader = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtDiffPercent = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.chbRunWithIOStat = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// chbShowCommentHeader
			// 
			this.chbShowCommentHeader.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chbShowCommentHeader.Location = new System.Drawing.Point(8, 56);
			this.chbShowCommentHeader.Name = "chbShowCommentHeader";
			this.chbShowCommentHeader.Size = new System.Drawing.Size(280, 16);
			this.chbShowCommentHeader.TabIndex = 7;
			this.chbShowCommentHeader.Text = "Show document header window";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtDiffPercent);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Enabled = false;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 168);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Information message";
			// 
			// txtDiffPercent
			// 
			this.txtDiffPercent.Location = new System.Drawing.Point(144, 56);
			this.txtDiffPercent.Name = "txtDiffPercent";
			this.txtDiffPercent.Size = new System.Drawing.Size(32, 20);
			this.txtDiffPercent.TabIndex = 5;
			this.txtDiffPercent.Text = "101";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Differencial percentage:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(280, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Enabeling this option will inform the user about ineffective query plans. ";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(264, 72);
			this.label2.TabIndex = 3;
			this.label2.Text = "Setting the differencial percentage will affect when the alarm is given. 101% (de" +
				"fault) means the alarm will be set of when the table scan exceed the rowcount.  " +
				"";
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Enabled = false;
			this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox1.Location = new System.Drawing.Point(8, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(264, 16);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "Use upper case on reserved words recognizion";
			// 
			// chbRunWithIOStat
			// 
			this.chbRunWithIOStat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chbRunWithIOStat.Location = new System.Drawing.Point(8, 32);
			this.chbRunWithIOStat.Name = "chbRunWithIOStat";
			this.chbRunWithIOStat.Size = new System.Drawing.Size(176, 16);
			this.chbRunWithIOStat.TabIndex = 5;
			this.chbRunWithIOStat.Text = "Run query with IO statistics";
			// 
			// UcOptionsQuerySettings
			// 
			this.Controls.Add(this.chbShowCommentHeader);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.chbRunWithIOStat);
			this.Name = "UcOptionsQuerySettings";
			this.Size = new System.Drawing.Size(720, 624);
			this.Load += new System.EventHandler(this.UcOptionsQuerySettings_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void UcOptionsQuerySettings_Load(object sender, System.EventArgs e)
		{
			QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
			if(settings.Exists())
			{
				this.chbRunWithIOStat.Checked=settings.RunWithIOStatistics;
				this.chbShowCommentHeader.Checked=settings.ShowFrmDocumentHeader;
			}
			else
			{
				this.chbRunWithIOStat.Checked=false;
				this.chbShowCommentHeader.Checked=true;
			
			}
		}
	}
}
