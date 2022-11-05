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
using System.Windows.Forms;
using QueryCommander.WinGui.UserControls;
using QueryCommander.Editor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using QueryCommander.Config;

namespace QueryCommander
{
	public class FrmOption : FrmBaseDialog
	{
		private UcOptionsEnviroments	optionsEnviroments = new UcOptionsEnviroments();
		private UcOptionEditor			optionEditor = new UcOptionEditor();
		private UcOptionsQuerySettings	optionsQuerySettings = new UcOptionsQuerySettings();
		private UcOptionStartUp			optionStartUp = new UcOptionStartUp();

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel UserControlContainer;
		private System.ComponentModel.IContainer components = null;
		private MainForm mainForm;
		public FrmOption(MainForm mainForm)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.mainForm=mainForm;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmOption));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.UserControlContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(8, 112);
			this.treeView1.Name = "treeView1";
			this.treeView1.ShowLines = false;
			this.treeView1.ShowPlusMinus = false;
			this.treeView1.ShowRootLines = false;
			this.treeView1.Size = new System.Drawing.Size(152, 320);
			this.treeView1.TabIndex = 0;
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(344, 408);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(72, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(424, 408);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(504, 104);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// UserControlContainer
			// 
			this.UserControlContainer.Location = new System.Drawing.Point(168, 112);
			this.UserControlContainer.Name = "UserControlContainer";
			this.UserControlContainer.Size = new System.Drawing.Size(328, 288);
			this.UserControlContainer.TabIndex = 5;
			// 
			// FrmOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 438);
			this.Controls.Add(this.UserControlContainer);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.treeView1);
			this.Name = "FrmOption";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.FrmOption_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmOption_Load(object sender, System.EventArgs e)
		{
			InitTreeView();

		}
		private void InitTreeView()
		{
			// Top
			TreeNode tnEnviroment = new TreeNode("Enviroment", 0, 1);

			// Childs
			TreeNode tnEditor = new TreeNode("Editor", 2, 3);
			TreeNode tnQuerySettings = new TreeNode("QuerySettings", 2, 3);
			TreeNode StartPage = new TreeNode("StartPage", 2, 3);

			tnEnviroment.Nodes.Add(tnEditor);
			tnEnviroment.Nodes.Add(tnQuerySettings);
			tnEnviroment.Nodes.Add(StartPage);

			this.treeView1.Nodes.Add(tnEnviroment);
			tnEnviroment.Expand();

			this.UserControlContainer.Controls.Add(optionsEnviroments);
			this.UserControlContainer.Controls.Add(optionEditor);
			this.UserControlContainer.Controls.Add(optionsQuerySettings);
			this.UserControlContainer.Controls.Add(optionStartUp);

			ActivateOptionControl(optionsEnviroments);

		}
		private void ActivateOptionControl(System.Windows.Forms.UserControl optionControl)
		{
			foreach(UserControl uc in this.UserControlContainer.Controls)
				uc.Hide();

			optionControl.Show();

//			if(activeOptionControl!=null)
//				activeOptionControl.Hide();
//			activeOptionControl=optionControl;
//			activeOptionControl.Show();
		}
		private void treeView1_Click(object sender, System.EventArgs e)
		{
			TreeNode selectedNode=treeView1.SelectedNode;
			
			switch(selectedNode.Text)
			{
				case "Enviroment":
					ActivateOptionControl(optionsEnviroments);
					break;
				case "Editor":
					ActivateOptionControl(optionEditor);
					break;
				case "QuerySettings":
					ActivateOptionControl(optionsQuerySettings);
					break;
				case "StartPage":
					ActivateOptionControl(optionStartUp);
					break;
			}


//			UcOptionsEnviroments optionsEnviroments = new UcOptionsEnviroments();
//			this.UserControlContainer.Controls.Add(optionsEnviroments);
//			optionsEnviroments.Show();
			
		}


		private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				TreeNode selectedNode = this.treeView1.GetNodeAt(e.X,e.Y);
				if(selectedNode != null)
				{
					switch(selectedNode.Text)
					{
						case "Enviroment":
							ActivateOptionControl(optionsEnviroments);
							break;
						case "Editor":
							ActivateOptionControl(optionEditor);
							break;
						case "QuerySettings":
							ActivateOptionControl(optionsQuerySettings);
							break;
						case "StartPage":
							ActivateOptionControl(optionStartUp);
							break;
					}
				}
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Editor settings
				QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
				settings.ShowEOLMarkers=optionEditor.chbShowEOLMarkers.Checked;
				settings.ShowSpaces=optionEditor.chbShowSpaces.Checked;
				settings.ShowTabs=optionEditor.chbShowTabs.Checked;
				settings.ShowLineNumbers=optionEditor.chbShowLineNumbers.Checked;
				settings.ShowMatchingBracket=optionEditor.chbShowMatchingBrackets.Checked;
				settings.fontFamily=optionEditor.font.FontFamily.Name;
				settings.fontGraphicsUnit=optionEditor.font.Unit;
				settings.fontSize=optionEditor.font.Size;
				settings.fontStyle=optionEditor.font.Style;

				//Startup
				settings.ShowStartPage=optionStartUp.chbShowStartPage.Checked;

				//QuerySettings
				settings.RunWithIOStatistics=optionsQuerySettings.chbRunWithIOStat.Checked;
				settings.ShowFrmDocumentHeader=optionsQuerySettings.chbShowCommentHeader.Checked;

				settings.Save();
				mainForm.UpdateEditorSettings();
			}
			catch{}
			this.Close();
		}
	}
}

