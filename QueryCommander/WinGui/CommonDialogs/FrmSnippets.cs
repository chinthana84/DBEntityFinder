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

namespace QueryCommander
{
	/// <summary>
	/// Summary description for FrmSnippets.
	/// </summary>
	public class FrmSnippets : FrmBaseDialog
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListView lstvSnippets;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmSnippets()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmSnippets));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lstvSnippets = new System.Windows.Forms.ListView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(400, 96);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lstvSnippets
			// 
			this.lstvSnippets.CheckBoxes = true;
			this.lstvSnippets.Location = new System.Drawing.Point(8, 16);
			this.lstvSnippets.Name = "lstvSnippets";
			this.lstvSnippets.Size = new System.Drawing.Size(368, 208);
			this.lstvSnippets.TabIndex = 1;
			this.lstvSnippets.View = System.Windows.Forms.View.List;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.lstvSnippets);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(384, 264);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Snippets";
			// 
			// btnDelete
			// 
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(296, 232);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(72, 24);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(320, 368);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// FrmSnippets
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(400, 406);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "FrmSnippets";
			this.Text = "Manage snippets";
			this.Load += new System.EventHandler(this.FrmSnippets_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmSnippets_Load(object sender, System.EventArgs e)
		{
			PopulateSnippets();
		}
		private void PopulateSnippets()
		{
			XmlDocument xmlSnippets = new XmlDocument();
			xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
			XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

			lstvSnippets.Items.Clear();

			foreach(XmlNode node in xmlNodeList[0].ChildNodes)
			{
				ListViewItem item = new ListViewItem(node.Attributes["name"].Value);
				item.Checked = false;
				item.Tag = node;
				lstvSnippets.Items.Add(item);
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			XmlDocument xmlSnippets = new XmlDocument();
			xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
			XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");
			XmlNode root = xmlSnippets.DocumentElement;

			foreach(ListViewItem lvi in lstvSnippets.Items)
			{
				if(lvi.Checked)
				{
					foreach(XmlNode node in xmlNodeList[0].ChildNodes)
					{
						if(node.Attributes["name"].Value == ((XmlNode)lvi.Tag).Attributes["name"].Value)
							root.RemoveChild( node );
					}
				}
			}

			xmlSnippets.Save(Application.StartupPath+@"\Snippets.xml");
			PopulateSnippets();

		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
