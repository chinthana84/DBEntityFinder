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
	/// Summary description for FrmAddToSnippet.
	/// </summary>
	public class FrmAddToSnippet : FrmBaseDialog
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox qcEditor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmAddToSnippet(string text)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			qcEditor.Text=text;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmAddToSnippet));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.qcEditor = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(408, 88);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Caption:";
			// 
			// txtCaption
			// 
			this.txtCaption.Location = new System.Drawing.Point(72, 104);
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(320, 20);
			this.txtCaption.TabIndex = 2;
			this.txtCaption.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(240, 384);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(72, 24);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(320, 384);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			// 
			// qcEditor
			// 
			this.qcEditor.Location = new System.Drawing.Point(8, 128);
			this.qcEditor.Multiline = true;
			this.qcEditor.Name = "qcEditor";
			this.qcEditor.Size = new System.Drawing.Size(384, 248);
			this.qcEditor.TabIndex = 6;
			this.qcEditor.Text = "textBox1";
			// 
			// FrmAddToSnippet
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(400, 422);
			this.Controls.Add(this.qcEditor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.txtCaption);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "FrmAddToSnippet";
			this.Text = "Manage snippets";
			this.Load += new System.EventHandler(this.FrmAddToSnippet_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmAddToSnippet_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if(txtCaption.Text.Length==0)
				return;

			XmlDocument xmlSnippets = new XmlDocument();
			xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
			XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

			foreach(XmlNode node in xmlNodeList[0].ChildNodes)
			{
				if(node.Attributes["name"].Value.ToUpper()==txtCaption.Text.ToUpper())
				{
					MessageBox.Show("Name is not unique.");
					return;
				}
			}
			
			XmlNode root = xmlSnippets.DocumentElement;

			//Create a new node.
			XmlElement elem = xmlSnippets.CreateElement("snippet");
			elem.InnerText=qcEditor.Text;
			
			XmlAttribute nameAttr = xmlSnippets.CreateAttribute("name");
			nameAttr.Value = txtCaption.Text;

			elem.Attributes.Append(nameAttr);


			//Add the node to the document.
			root.AppendChild(elem);

			xmlSnippets.Save(Application.StartupPath+@"\Snippets.xml");

			this.Close();
		}
	}
}
