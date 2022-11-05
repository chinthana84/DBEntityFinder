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
using IVSSFlags;
using IVSSFunctionLibrary;

namespace QueryCommander.VSS
{
	/// <summary>
	/// Summary description for FrmLogin.
	/// </summary>
	public class FrmLogin : System.Windows.Forms.Form
	{
		VSSConnection _vssConnection=null;
		public bool IsLoggedIn=false;
		public QueryCommander.VSS.Database Database = new Database();
		public clIVSSLibrary IVSS = new clIVSSLibrary();
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label lblDatabase;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.TextBox txtSrcSafeINIPath;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtParentDirectory;
		private System.Windows.Forms.CheckBox chkRemeberPassword;
		private System.Windows.Forms.Label lblTitle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmLogin(VSSConnection vssConnection)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_vssConnection=vssConnection;
			txtSrcSafeINIPath.Text=_vssConnection.VSSDatabasePath;
			txtParentDirectory.Text=_vssConnection.ParentProject;
			txtUserName.Text=_vssConnection.UserName;
			txtPassword.Text=_vssConnection.Password;
			lblTitle.Text="Add [" + _vssConnection.Server +".."+ _vssConnection.Database + "] to Source Control.";

			if(_vssConnection!=null)
			{
				if(_vssConnection.RememberPassword=true &&
					_vssConnection.VSSDatabasePath!=null &&
					_vssConnection.ParentProject!=null &&
					_vssConnection.UserName!=null)
					Finish();
			}
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
			this.chkRemeberPassword = new System.Windows.Forms.CheckBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtSrcSafeINIPath = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.lblTitle = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtParentDirectory = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkRemeberPassword
			// 
			this.chkRemeberPassword.Checked = true;
			this.chkRemeberPassword.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRemeberPassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkRemeberPassword.Location = new System.Drawing.Point(104, 80);
			this.chkRemeberPassword.Name = "chkRemeberPassword";
			this.chkRemeberPassword.Size = new System.Drawing.Size(200, 16);
			this.chkRemeberPassword.TabIndex = 12;
			this.chkRemeberPassword.Text = "Remember password";
			// 
			// btnBrowse
			// 
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBrowse.Location = new System.Drawing.Point(328, 32);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(56, 24);
			this.btnBrowse.TabIndex = 14;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// lblDatabase
			// 
			this.lblDatabase.AutoSize = true;
			this.lblDatabase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDatabase.Location = new System.Drawing.Point(16, 32);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(56, 16);
			this.lblDatabase.TabIndex = 17;
			this.lblDatabase.Text = "Database:";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPassword.Location = new System.Drawing.Point(16, 48);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(57, 16);
			this.lblPassword.TabIndex = 15;
			this.lblPassword.Text = "Password:";
			// 
			// lblUserName
			// 
			this.lblUserName.AutoSize = true;
			this.lblUserName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUserName.Location = new System.Drawing.Point(16, 24);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(63, 16);
			this.lblUserName.TabIndex = 11;
			this.lblUserName.Text = "User name:";
			// 
			// txtSrcSafeINIPath
			// 
			this.txtSrcSafeINIPath.Location = new System.Drawing.Point(112, 32);
			this.txtSrcSafeINIPath.Name = "txtSrcSafeINIPath";
			this.txtSrcSafeINIPath.Size = new System.Drawing.Size(216, 20);
			this.txtSrcSafeINIPath.TabIndex = 10;
			this.txtSrcSafeINIPath.Text = "C:\\Program Files\\Microsoft Visual Studio\\VSS\\srcsafe.ini";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(104, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(224, 20);
			this.txtPassword.TabIndex = 9;
			this.txtPassword.Text = "";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(104, 32);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(224, 20);
			this.txtUserName.TabIndex = 8;
			this.txtUserName.Text = "wmmihaa";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(256, 288);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "Ok";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(336, 288);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 16;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(416, 56);
			this.splitter1.TabIndex = 18;
			this.splitter1.TabStop = false;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(16, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(376, 40);
			this.lblTitle.TabIndex = 19;
			this.lblTitle.Text = "Add...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 16);
			this.label2.TabIndex = 21;
			this.label2.Text = "VSS Project path:";
			// 
			// txtParentDirectory
			// 
			this.txtParentDirectory.Location = new System.Drawing.Point(112, 56);
			this.txtParentDirectory.Name = "txtParentDirectory";
			this.txtParentDirectory.Size = new System.Drawing.Size(216, 20);
			this.txtParentDirectory.TabIndex = 20;
			this.txtParentDirectory.Text = "$\\<VSS Project path>";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtParentDirectory);
			this.groupBox1.Controls.Add(this.txtSrcSafeINIPath);
			this.groupBox1.Controls.Add(this.lblDatabase);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 96);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtUserName);
			this.groupBox2.Controls.Add(this.txtPassword);
			this.groupBox2.Controls.Add(this.chkRemeberPassword);
			this.groupBox2.Controls.Add(this.lblUserName);
			this.groupBox2.Controls.Add(this.lblPassword);
			this.groupBox2.Location = new System.Drawing.Point(8, 168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(400, 112);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "User credentials";
			// 
			// FrmLogin
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 326);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Name = "FrmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Visual SourceSafe Login";
			this.Load += new System.EventHandler(this.FrmLogin_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog BrowseForDBDialog = new OpenFileDialog();
			BrowseForDBDialog.CheckFileExists = true;
			BrowseForDBDialog.Multiselect = false;
			BrowseForDBDialog.DefaultExt = ".ini";
			BrowseForDBDialog.Filter = "Srcsafe.ini (Srcsafe.ini)|Srcsafe.ini|All files (*.*)|*.*";
			BrowseForDBDialog.FilterIndex = 1;
			BrowseForDBDialog.RestoreDirectory = true;

			if(BrowseForDBDialog.ShowDialog()==DialogResult.OK)
				txtSrcSafeINIPath.Text = BrowseForDBDialog.FileName;
			
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(txtParentDirectory.Text.IndexOf(@"$\")<0)
			{
				MessageBox.Show(@"The Vss Project Path is not defined in a correct format. Format: $\<VSS Project Path>","Vss Project Path");
				return ;
			}
														
			Finish();
		}
		
		private void Finish()
		{
			FrmWait frmWait = new FrmWait();
			frmWait.Show();
			Application.DoEvents();

			string localErr;
			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			IVSS.CloseDB();
			localErr = IVSS.OpenDB(txtUserName.Text, txtPassword.Text, txtSrcSafeINIPath.Text);
			if(localErr!="")
			{
				MessageBox.Show(localErr,"Could not open database",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			Database.UserPassword = txtPassword.Text;
			Cursor.Current = System.Windows.Forms.Cursors.Default;
			
			
			_vssConnection.VSSDatabasePath=txtSrcSafeINIPath.Text;
			_vssConnection.ParentProject=txtParentDirectory.Text;
			_vssConnection.UserName=txtUserName.Text;
			_vssConnection.Password=txtPassword.Text;
			_vssConnection.RememberPassword=chkRemeberPassword.Checked;
		
			if(_vssConnection.ParentProject.Substring(_vssConnection.ParentProject.Length-1,1)=="\\")
				_vssConnection.ParentProject=_vssConnection.ParentProject.Substring(0,_vssConnection.ParentProject.Length-1);

			IsLoggedIn=true;
			this.DialogResult=DialogResult.OK;
			frmWait.Close();

		}

		private void FrmLogin_Load(object sender, System.EventArgs e)
		{
			
		}
	}
}
