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
using WeifenLuo.WinFormsUI;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class FrmDebug : FrmBaseContent
	{
		public bool Debug;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.ComboBox comboBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmDebug(Form mdiParentForm)
		{
			this.MdiParentForm=mdiParentForm;
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.Location = new System.Drawing.Point(0, 40);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(280, 342);
			this.listBox1.TabIndex = 0;
			// 
			// btnClear
			// 
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClear.Location = new System.Drawing.Point(8, 8);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "[All]",
														   "[None]",
														   "GetStringColor",
														   "SetColorForRange",
														   "SetCommentAndStringColor",
														   "SetColorSyntaxForAllText",
														   "SetCurrentWordColor",
														   "PreProcessMessage",
														   "WndProc",
														   "Refresh",
														   "OnKeyUp",
														   "OnSelectionChanged",
														   "OnTextChanged"});
			this.comboBox1.Location = new System.Drawing.Point(96, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(144, 21);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.Text = "[All]";
			// 
			// FrmDebug
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 394);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.listBox1);
			this.DockPadding.Bottom = 3;
			this.DockPadding.Top = 3;
			this.HideOnClose = true;
			this.Name = "FrmDebug";
			this.Text = "Debug";
			this.ResumeLayout(false);

		}
		#endregion
	
		public void AddContext(string context)
		{
			if(!Debug)
				return;

			if(comboBox1.Text=="[None]")
				return;

			if(comboBox1.Text=="[All]" || context.IndexOf(comboBox1.Text) > -1)// == context)
				listBox1.SelectedIndex =  listBox1.Items.Add(context);

		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			listBox1.Items.Clear();
		}
	}
}
