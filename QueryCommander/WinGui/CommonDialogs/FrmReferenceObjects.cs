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
	/// Summary description for FrmReferenceObjects.
	/// </summary>
	public class FrmReferenceObjects : FrmBaseDialog
	{
		public string ReferencedObject = "";
		private System.Windows.Forms.ListView lstObject;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.ComponentModel.IContainer components;

		public FrmReferenceObjects( ArrayList objects, string objectName )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			groupBox1.Text= "Objects using [" + objectName + "]";

			foreach(Database.DBObject dbObject in objects)
			{
				switch(dbObject.Type.ToUpper())
				{
					case "V ": //Tables
						lstObject.Items.Add(dbObject.Name,4);
						break;
					case "U ": //Tables
						lstObject.Items.Add(dbObject.Name,4);
						break;
					case "P ": //Stored Procedures
						lstObject.Items.Add(dbObject.Name,1);
						break;
					case "FN": //Functions
						lstObject.Items.Add(dbObject.Name,0);
						break;
					case "TF": //Functions
						lstObject.Items.Add(dbObject.Name,0);
						break;
					default:
						lstObject.Items.Add(dbObject.Name,2);
						break;
				}
			}
			if(lstObject.Items.Count==0)
				btnOK.Enabled=false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmReferenceObjects));
			this.lstObject = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstObject
			// 
			this.lstObject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1});
			this.lstObject.LargeImageList = this.imageList1;
			this.lstObject.Location = new System.Drawing.Point(16, 24);
			this.lstObject.MultiSelect = false;
			this.lstObject.Name = "lstObject";
			this.lstObject.Size = new System.Drawing.Size(352, 240);
			this.lstObject.SmallImageList = this.imageList1;
			this.lstObject.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstObject.StateImageList = this.imageList1;
			this.lstObject.TabIndex = 2;
			this.lstObject.View = System.Windows.Forms.View.Details;
			this.lstObject.DoubleClick += new System.EventHandler(this.lstObject_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Referenced object";
			this.columnHeader1.Width = 333;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(312, 392);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(224, 392);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 24);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(400, 104);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lstObject);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 104);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(384, 280);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// FrmReferenceObjects
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(400, 438);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmReferenceObjects";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Go to reference";
			this.Load += new System.EventHandler(this.FrmReferenceObjects_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(lstObject.SelectedItems.Count == 0)
			{
				MessageBox.Show("No object is selected (marked).\nPlease choose by highlighting an object.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				return;
			}
			ReferencedObject = lstObject.SelectedItems[0].Text;
			DialogResult=DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
			this.Close();
		}

		private void lstObject_DoubleClick(object sender, System.EventArgs e)
		{
			ReferencedObject = lstObject.SelectedItems[0].Text;
			DialogResult=DialogResult.OK;
			this.Close();
		}

		private void FrmReferenceObjects_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
