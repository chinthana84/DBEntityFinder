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
using System.Xml;
using System.Data;
using QueryCommander.Database;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for FrmDBConnections.
	/// </summary>
	public class FrmDBConnections : FrmBaseDialog
	{
		#region Private members
		private DataSourceCollection _dataSourceCollection;
		XmlDocument xmlDBConnections = new XmlDocument();
		ArrayList DBConnections = new ArrayList();
		private System.Windows.Forms.ListView lstvConnections;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button BtnEdit;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnCansel;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.ComponentModel.IContainer components;
		#endregion
		#region Default
		public FrmDBConnections()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			PopulateDBConnections();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmDBConnections));
			this.lstvConnections = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.BtnEdit = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnCansel = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstvConnections
			// 
			this.lstvConnections.CheckBoxes = true;
			this.lstvConnections.Location = new System.Drawing.Point(16, 24);
			this.lstvConnections.Name = "lstvConnections";
			this.lstvConnections.Size = new System.Drawing.Size(352, 192);
			this.lstvConnections.TabIndex = 0;
			this.lstvConnections.View = System.Windows.Forms.View.List;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// BtnEdit
			// 
			this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnEdit.Location = new System.Drawing.Point(200, 224);
			this.BtnEdit.Name = "BtnEdit";
			this.BtnEdit.Size = new System.Drawing.Size(80, 24);
			this.BtnEdit.TabIndex = 3;
			this.BtnEdit.Text = "Edit";
			this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(224, 368);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 24);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnNew
			// 
			this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNew.Location = new System.Drawing.Point(112, 224);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(80, 24);
			this.btnNew.TabIndex = 5;
			this.btnNew.Text = "New";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnCansel
			// 
			this.btnCansel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCansel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCansel.Location = new System.Drawing.Point(312, 368);
			this.btnCansel.Name = "btnCansel";
			this.btnCansel.Size = new System.Drawing.Size(80, 24);
			this.btnCansel.TabIndex = 6;
			this.btnCansel.Text = "Cancel";
			this.btnCansel.Click += new System.EventHandler(this.btnCansel_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(288, 224);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(80, 24);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "Remove";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(400, 96);
			this.pictureBox1.TabIndex = 21;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.lstvConnections);
			this.groupBox1.Controls.Add(this.BtnEdit);
			this.groupBox1.Controls.Add(this.btnNew);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(384, 264);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Data sources";
			// 
			// FrmDBConnections
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCansel;
			this.ClientSize = new System.Drawing.Size(400, 406);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnCansel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmDBConnections";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Data sources";
			this.Load += new System.EventHandler(this.FrmDBConnections_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion


		private void PopulateDBConnections()
		{			
			_dataSourceCollection = DataSourceFactory.GetDataSources();
			lstvConnections.Items.Clear();

			foreach(DataSource ds in _dataSourceCollection)
			{
				ListViewItem item;
				if(ds.FriendlyName ==null || ds.FriendlyName.Length==0)
					item = new ListViewItem(ds.Name);
				else
					item = new ListViewItem(ds.Name + " [" + ds.FriendlyName + "]");

				item.Checked = ds.IsConnected;
				item.Tag = ds.ID;
				lstvConnections.Items.Add(item);
				
			}

		}

		
		public void btnOk_Click(object sender, System.EventArgs e)
		{
			bool connected=false;
			foreach(ListViewItem item in lstvConnections.Items)
			{
				DataSource ds = _dataSourceCollection.FindByID((Guid)item.Tag);
				if(item.Checked)
				{
					ds.IsConnected=true;
					connected =true;
				}
				else
					ds.IsConnected=false;
			}
			_dataSourceCollection.Save();

			if(connected)
			{
				this.DialogResult=DialogResult.OK;
				this.Close();
			}
			else
				MessageBox.Show("No Datasource selected.", this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);

		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			FrmDBConnection frm = new FrmDBConnection();
			frm.ShowDialogWindow(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				_dataSourceCollection.Add(frm.dataSource);
				_dataSourceCollection.Save();
			}
			PopulateDBConnections();
		
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			_dataSourceCollection.Delete(_dataSourceCollection.FindByID((Guid)lstvConnections.SelectedItems[0].Tag));
			PopulateDBConnections();
			return;
		}

		private void btnCansel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
//			Application.Exit();
		}

		private void BtnEdit_Click(object sender, System.EventArgs e)
		{
			if(lstvConnections.SelectedItems.Count == 0)
			{
				MessageBox.Show("No data sourse selected (marked).\nPlease choose by highlighting one.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				return;
			}
			string DSN = lstvConnections.SelectedItems[0].Text;
			FrmDBConnection frm = new FrmDBConnection(_dataSourceCollection.FindByID((Guid)lstvConnections.SelectedItems[0].Tag) );
			frm.ShowDialogWindow(this);
			if(frm.DialogResult==DialogResult.OK)
			{
				_dataSourceCollection.Delete(_dataSourceCollection.FindByID((Guid)lstvConnections.SelectedItems[0].Tag));
				_dataSourceCollection.Add(frm.dataSource);
				_dataSourceCollection.Save();
			}
			
			PopulateDBConnections();
			
		}

		private void FrmDBConnections_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
