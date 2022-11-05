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

namespace QueryCommander.VSS
{
	/// <summary>
	/// Summary description for FrmShowHistory.
	/// </summary>
	public class FrmShowHistory : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ListView HistoryList;
		protected System.Windows.Forms.ColumnHeader clmVersionLabel;
		protected System.Windows.Forms.ColumnHeader clmUser;
		protected System.Windows.Forms.ColumnHeader clmDate;
		protected System.Windows.Forms.ColumnHeader clmAction;
		private VSSHitoryItemCollection _vssHitoryItemCollection;
		public System.Windows.Forms.Label HistoryLabel;
		private System.Windows.Forms.Button btnDiff;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmShowHistory(VSSHitoryItemCollection vssHitoryItemCollection)
		{
			_vssHitoryItemCollection=vssHitoryItemCollection;
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
			this.HistoryList = new System.Windows.Forms.ListView();
			this.clmVersionLabel = new System.Windows.Forms.ColumnHeader();
			this.clmUser = new System.Windows.Forms.ColumnHeader();
			this.clmDate = new System.Windows.Forms.ColumnHeader();
			this.clmAction = new System.Windows.Forms.ColumnHeader();
			this.HistoryLabel = new System.Windows.Forms.Label();
			this.btnDiff = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// HistoryList
			// 
			this.HistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.clmVersionLabel,
																						  this.clmUser,
																						  this.clmDate,
																						  this.clmAction});
			this.HistoryList.FullRowSelect = true;
			this.HistoryList.HideSelection = false;
			this.HistoryList.Location = new System.Drawing.Point(8, 32);
			this.HistoryList.Name = "HistoryList";
			this.HistoryList.Size = new System.Drawing.Size(264, 280);
			this.HistoryList.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.HistoryList.TabIndex = 1;
			this.HistoryList.View = System.Windows.Forms.View.Details;
			this.HistoryList.SelectedIndexChanged += new System.EventHandler(this.HistoryList_SelectedIndexChanged);
			// 
			// clmVersionLabel
			// 
			this.clmVersionLabel.Text = "Version";
			// 
			// clmUser
			// 
			this.clmUser.Text = "User";
			// 
			// clmDate
			// 
			this.clmDate.Text = "Date";
			// 
			// clmAction
			// 
			this.clmAction.Text = "Action";
			// 
			// HistoryLabel
			// 
			this.HistoryLabel.Location = new System.Drawing.Point(8, 8);
			this.HistoryLabel.Name = "HistoryLabel";
			this.HistoryLabel.Size = new System.Drawing.Size(256, 16);
			this.HistoryLabel.TabIndex = 2;
			this.HistoryLabel.Text = "label1";
			// 
			// btnDiff
			// 
			this.btnDiff.Enabled = false;
			this.btnDiff.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDiff.Location = new System.Drawing.Point(104, 320);
			this.btnDiff.Name = "btnDiff";
			this.btnDiff.Size = new System.Drawing.Size(80, 24);
			this.btnDiff.TabIndex = 3;
			this.btnDiff.Text = "Diff";
			this.btnDiff.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(192, 320);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(80, 24);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			// 
			// FrmShowHistory
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 358);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDiff);
			this.Controls.Add(this.HistoryLabel);
			this.Controls.Add(this.HistoryList);
			this.Name = "FrmShowHistory";
			this.Text = "FrmShowHistory";
			this.Load += new System.EventHandler(this.FrmShowHistory_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmShowHistory_Load(object sender, System.EventArgs e)
		{
			foreach(VSSHitoryItem item in _vssHitoryItemCollection)
			{
				ListViewItem lvi = HistoryList.Items.Add(item.Text);
				lvi.SubItems.Add(item.Username);
				lvi.SubItems.Add(item.Date);
				lvi.SubItems.Add(item.Action);
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{

			this.DialogResult=DialogResult.OK;
			this.Close();
		}	

		private void HistoryList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(HistoryList.SelectedItems.Count>2)
				foreach(ListViewItem lvi in HistoryList.Items)
				{
					lvi.Selected=false;
				}

			if(HistoryList.SelectedItems.Count==2)
				btnDiff.Enabled=true;
			else
				btnDiff.Enabled=false;
		}
	}
}
