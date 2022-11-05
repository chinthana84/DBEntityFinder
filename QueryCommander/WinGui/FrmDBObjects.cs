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
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI;
using System.Data;
using System.Data.SqlClient;
using QueryCommander.General;
using QueryCommander.Database;
using QueryCommander.General.WorkSpace;

namespace QueryCommander
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class FrmDBObjects : FrmBaseContent
	{
		#region Members
		bool _isLoaded;
		private Hashtable DBTreeViewTypes = new Hashtable();
		
		private System.Windows.Forms.TreeView TvDBObjects;
		private System.Windows.Forms.ImageList imglDataObjects;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem miAddServer;
		private System.Windows.Forms.MenuItem miUseDatabase;
		private System.Windows.Forms.MenuItem miScriptCreate;
		private System.Windows.Forms.MenuItem miScriptAlter;
		private System.Windows.Forms.MenuItem miScriptDrop;
		private System.Windows.Forms.MenuItem miScriptSelect;
		private System.Windows.Forms.MenuItem miScriptUpdate;
		private System.Windows.Forms.MenuItem miScriptInsert;
		private System.Windows.Forms.MenuItem miScriptDelete;
		private System.Windows.Forms.MenuItem miNew;
		private System.Windows.Forms.MenuItem miDelete;
		private System.Windows.Forms.MenuItem miRemoveServer;
		private System.Windows.Forms.MenuItem miScript;
		private System.Windows.Forms.MenuItem miCreateInsertScript;
		private System.Windows.Forms.MenuItem miCreateUpdateScript;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem miRefreshDataObjects;
		private System.Windows.Forms.MenuItem mi_SourceControl;
		private System.Windows.Forms.MenuItem mi_CheckIn;
		private System.Windows.Forms.MenuItem mi_CheckOut;
		private System.Windows.Forms.MenuItem mi_AddToSourceControl;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mi_DetachFromSourceControl;
		private System.Windows.Forms.MenuItem miOpenScript;
		private System.Windows.Forms.MenuItem mi_VSSSettings;
		private System.Windows.Forms.MenuItem mi_UndoCheckOut;
		private System.Windows.Forms.MenuItem mi_ShowHistory;
		private TreeNode CurrentTreeNode;
		#endregion
		#region Default
		public FrmDBObjects(Form mdiParentForm)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmDBObjects));
			this.imglDataObjects = new System.Windows.Forms.ImageList(this.components);
			this.TvDBObjects = new System.Windows.Forms.TreeView();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.miAddServer = new System.Windows.Forms.MenuItem();
			this.miRemoveServer = new System.Windows.Forms.MenuItem();
			this.miUseDatabase = new System.Windows.Forms.MenuItem();
			this.miScript = new System.Windows.Forms.MenuItem();
			this.miScriptCreate = new System.Windows.Forms.MenuItem();
			this.miScriptAlter = new System.Windows.Forms.MenuItem();
			this.miScriptDrop = new System.Windows.Forms.MenuItem();
			this.miScriptSelect = new System.Windows.Forms.MenuItem();
			this.miScriptUpdate = new System.Windows.Forms.MenuItem();
			this.miScriptInsert = new System.Windows.Forms.MenuItem();
			this.miScriptDelete = new System.Windows.Forms.MenuItem();
			this.miCreateInsertScript = new System.Windows.Forms.MenuItem();
			this.miCreateUpdateScript = new System.Windows.Forms.MenuItem();
			this.miNew = new System.Windows.Forms.MenuItem();
			this.miDelete = new System.Windows.Forms.MenuItem();
			this.miRefreshDataObjects = new System.Windows.Forms.MenuItem();
			this.miOpenScript = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mi_SourceControl = new System.Windows.Forms.MenuItem();
			this.mi_CheckIn = new System.Windows.Forms.MenuItem();
			this.mi_CheckOut = new System.Windows.Forms.MenuItem();
			this.mi_AddToSourceControl = new System.Windows.Forms.MenuItem();
			this.mi_DetachFromSourceControl = new System.Windows.Forms.MenuItem();
			this.mi_VSSSettings = new System.Windows.Forms.MenuItem();
			this.mi_UndoCheckOut = new System.Windows.Forms.MenuItem();
			this.mi_ShowHistory = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// imglDataObjects
			// 
			this.imglDataObjects.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imglDataObjects.ImageSize = new System.Drawing.Size(16, 16);
			this.imglDataObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglDataObjects.ImageStream")));
			this.imglDataObjects.TransparentColor = System.Drawing.Color.Black;
			// 
			// TvDBObjects
			// 
			this.TvDBObjects.BackColor = System.Drawing.Color.White;
			this.TvDBObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TvDBObjects.ImageList = this.imglDataObjects;
			this.TvDBObjects.ItemHeight = 16;
			this.TvDBObjects.Location = new System.Drawing.Point(0, 2);
			this.TvDBObjects.Name = "TvDBObjects";
			this.TvDBObjects.Size = new System.Drawing.Size(221, 361);
			this.TvDBObjects.TabIndex = 0;
			this.TvDBObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TvDBObjects_MouseUp);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.miAddServer,
																						 this.miRemoveServer,
																						 this.miUseDatabase,
																						 this.miScript,
																						 this.miNew,
																						 this.miDelete,
																						 this.miRefreshDataObjects,
																						 this.miOpenScript,
																						 this.menuItem1,
																						 this.mi_SourceControl});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// miAddServer
			// 
			this.miAddServer.Index = 0;
			this.miAddServer.Text = "&Edit server connecctions";
			this.miAddServer.Click += new System.EventHandler(this.AddConnection_Click);
			// 
			// miRemoveServer
			// 
			this.miRemoveServer.Index = 1;
			this.miRemoveServer.Text = "&Remove server connection";
			this.miRemoveServer.Click += new System.EventHandler(this.RemoveConnection_Click);
			// 
			// miUseDatabase
			// 
			this.miUseDatabase.Index = 2;
			this.miUseDatabase.Text = "&Use database";
			this.miUseDatabase.Click += new System.EventHandler(this.UseDatabase_Click);
			// 
			// miScript
			// 
			this.miScript.Index = 3;
			this.miScript.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.miScriptCreate,
																					 this.miScriptAlter,
																					 this.miScriptDrop,
																					 this.miScriptSelect,
																					 this.miScriptUpdate,
																					 this.miScriptInsert,
																					 this.miScriptDelete,
																					 this.miCreateInsertScript,
																					 this.miCreateUpdateScript});
			this.miScript.Text = "&Script";
			// 
			// miScriptCreate
			// 
			this.miScriptCreate.Index = 0;
			this.miScriptCreate.Text = "Create";
			this.miScriptCreate.Click += new System.EventHandler(this.ScriptCreate_Click);
			// 
			// miScriptAlter
			// 
			this.miScriptAlter.Index = 1;
			this.miScriptAlter.Text = "Alter";
			this.miScriptAlter.Click += new System.EventHandler(this.ScriptAlter_Click);
			// 
			// miScriptDrop
			// 
			this.miScriptDrop.Index = 2;
			this.miScriptDrop.Text = "Drop";
			this.miScriptDrop.Click += new System.EventHandler(this.ScriptDrop_Click);
			// 
			// miScriptSelect
			// 
			this.miScriptSelect.Index = 3;
			this.miScriptSelect.Text = "Select";
			this.miScriptSelect.Click += new System.EventHandler(this.ScriptSelect_Click);
			// 
			// miScriptUpdate
			// 
			this.miScriptUpdate.Index = 4;
			this.miScriptUpdate.Text = "Update";
			this.miScriptUpdate.Click += new System.EventHandler(this.ScriptUpdate_Click);
			// 
			// miScriptInsert
			// 
			this.miScriptInsert.Index = 5;
			this.miScriptInsert.Text = "Insert";
			this.miScriptInsert.Click += new System.EventHandler(this.ScriptInsert_Click);
			// 
			// miScriptDelete
			// 
			this.miScriptDelete.Index = 6;
			this.miScriptDelete.Text = "Delete";
			// 
			// miCreateInsertScript
			// 
			this.miCreateInsertScript.Index = 7;
			this.miCreateInsertScript.Text = "Create insert script";
			this.miCreateInsertScript.Click += new System.EventHandler(this.miCreateInsertScript_Click);
			// 
			// miCreateUpdateScript
			// 
			this.miCreateUpdateScript.Index = 8;
			this.miCreateUpdateScript.Text = "Create update script";
			this.miCreateUpdateScript.Click += new System.EventHandler(this.miCreateUpdateScript_Click);
			// 
			// miNew
			// 
			this.miNew.Index = 4;
			this.miNew.Text = "&New";
			// 
			// miDelete
			// 
			this.miDelete.Index = 5;
			this.miDelete.Text = "&Delete";
			// 
			// miRefreshDataObjects
			// 
			this.miRefreshDataObjects.Index = 6;
			this.miRefreshDataObjects.Text = "Refresh";
			this.miRefreshDataObjects.Click += new System.EventHandler(this.RefreshDataObjects_Click);
			// 
			// miOpenScript
			// 
			this.miOpenScript.Index = 7;
			this.miOpenScript.Text = "Open document";
			this.miOpenScript.Click += new System.EventHandler(this.miOpenScript_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 8;
			this.menuItem1.Text = "-";
			// 
			// mi_SourceControl
			// 
			this.mi_SourceControl.Index = 9;
			this.mi_SourceControl.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.mi_CheckIn,
																							 this.mi_CheckOut,
																							 this.mi_AddToSourceControl,
																							 this.mi_DetachFromSourceControl,
																							 this.mi_VSSSettings,
																							 this.mi_UndoCheckOut,
																							 this.mi_ShowHistory});
			this.mi_SourceControl.Text = "Source control";
			// 
			// mi_CheckIn
			// 
			this.mi_CheckIn.Index = 0;
			this.mi_CheckIn.Text = "Check in";
			this.mi_CheckIn.Click += new System.EventHandler(this.mi_CheckIn_Click);
			// 
			// mi_CheckOut
			// 
			this.mi_CheckOut.Index = 1;
			this.mi_CheckOut.Text = "Check out";
			this.mi_CheckOut.Click += new System.EventHandler(this.mi_CheckOut_Click);
			// 
			// mi_AddToSourceControl
			// 
			this.mi_AddToSourceControl.Index = 2;
			this.mi_AddToSourceControl.Text = "Add Database to Source Control";
			this.mi_AddToSourceControl.Click += new System.EventHandler(this.mi_AddToSourceControl_Click);
			// 
			// mi_DetachFromSourceControl
			// 
			this.mi_DetachFromSourceControl.Index = 3;
			this.mi_DetachFromSourceControl.Text = "Detach Database from Source Control";
			this.mi_DetachFromSourceControl.Click += new System.EventHandler(this.mi_DetachFromSourceControl_Click);
			// 
			// mi_VSSSettings
			// 
			this.mi_VSSSettings.Index = 4;
			this.mi_VSSSettings.Text = "Settings";
			this.mi_VSSSettings.Click += new System.EventHandler(this.mi_VSSSettings_Click);
			// 
			// mi_UndoCheckOut
			// 
			this.mi_UndoCheckOut.Index = 5;
			this.mi_UndoCheckOut.Text = "Undo Check out";
			this.mi_UndoCheckOut.Click += new System.EventHandler(this.mi_UndoCheckOut_Click);
			// 
			// mi_ShowHistory
			// 
			this.mi_ShowHistory.Index = 6;
			this.mi_ShowHistory.Text = "Show history";
			this.mi_ShowHistory.Click += new System.EventHandler(this.mi_ShowHistory_Click);
			// 
			// FrmDBObjects
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(221, 365);
			this.Controls.Add(this.TvDBObjects);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft) 
				| WeifenLuo.WinFormsUI.DockAreas.DockRight) 
				| WeifenLuo.WinFormsUI.DockAreas.DockTop) 
				| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.DockPadding.Bottom = 2;
			this.DockPadding.Top = 2;
			this.HideOnClose = true;
			this.Name = "FrmDBObjects";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockLeftAutoHide;
			this.Text = "Server Explorer";
			this.Click += new System.EventHandler(this.FrmDBObjects_Click);
			this.Load += new System.EventHandler(this.FrmDBObjects_Load);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion
		#region Events
		private void FrmDBObjects_Load(object sender, System.EventArgs e)
		{
			//RefreashTreeView();
			_isLoaded=true;
		}

		private void FrmDBObjects_Click(object sender, System.EventArgs e)
		{
			
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
		
		}
		
		private void TvDBObjects_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				CurrentTreeNode = TvDBObjects.GetNodeAt(e.X,e.Y);
				if(CurrentTreeNode != null)
				{
					if( ((QCTreeNode)CurrentTreeNode).objecttype==QCTreeNode.ObjectType.WorkSpaceItem ||
						((QCTreeNode)CurrentTreeNode).objecttype==QCTreeNode.ObjectType.WorkSpace ||
						((QCTreeNode)CurrentTreeNode).objecttype==QCTreeNode.ObjectType.WorkSpaces)
					{
						SetContextMenu((QCTreeNode)CurrentTreeNode);
						//contextMenuWorkSpace.Show(TvDBObjects,new Point(e.X,e.Y));
						return;
					}
					SetContextMenu(CurrentTreeNode);
					contextMenu1.Show(TvDBObjects,new Point(e.X,e.Y));
				}
			}
		}
		
		#endregion
		#region Context menu
		private void AddConnection_Click(object sender, System.EventArgs e)
		{
			FrmDBConnections frm = new FrmDBConnections();
			frm.ShowDialogWindow(this);
			RefreashTreeView();

		}

		private void RemoveConnection_Click(object sender, System.EventArgs e)
		{
		
		}

		private void UseDatabase_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
			if(frm.ActiveQueryForm == null)
				return;

			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				string connectionName=CurrentTreeNode.Parent.Text;
				
				if(CurrentTreeNode.Parent.Text.IndexOf(" [")>0)
					connectionName = CurrentTreeNode.Parent.Text.Substring(0,CurrentTreeNode.Parent.Text.IndexOf(" ["));
				

				if(c.ConnectionName == connectionName)
				{
					frm.ActiveQueryForm.SetDatabaseConnection(CurrentTreeNode.Text,c.Connection);
					break;
				}
			}
		}

		private void ScriptCreate_Click(object sender, System.EventArgs e)
		{
			string CreateScript;
//			Database db = new Database();
			MainForm frm =  (MainForm)MdiParentForm;
			
			if(frm.ActiveQueryForm == null || frm.ActiveQueryForm.Content.Length>0)
				frm.NewQueryform();

			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				
				if(c.ConnectionName == ((QCTreeNode)CurrentTreeNode).server)
				{
					string dbName = CurrentTreeNode.Parent.Parent.Text;
					frm.ActiveQueryForm.SetDatabaseConnection(dbName,c.Connection);
					break;
				}
			}

			IDatabaseManager db = DatabaseFactory.CreateNew(frm.ActiveQueryForm.dbConnection);
			// CurrentTreeNode.Parent.Text
			if(CurrentTreeNode.Parent.Text.ToUpper()=="TABLES")
				CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
					CurrentTreeNode.Text,
					frm.ActiveQueryForm.dbConnection,
					DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.TABLE);
			else
				CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
					CurrentTreeNode.Text,
					frm.ActiveQueryForm.dbConnection,
					DBCommon.ScriptType.CREATE, DBCommon.ScriptObjectType.PROCEDURE);

			if(CreateScript.IndexOf("</member>",0)>0)
				frm.ActiveQueryForm.Content = AddRevisionCommentSection(CreateScript);
			else
				frm.ActiveQueryForm.Content = CreateScript;

			
			//Set database connection
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				string databaseConnection  = CurrentTreeNode.Parent.Parent.Parent.Text;
				if(c.ConnectionName == databaseConnection)
				{
					frm.ActiveQueryForm.SetDatabaseConnection(CurrentTreeNode.Parent.Parent.Text,c.Connection);
					break;
				}
			}
			
			frm.ActiveQueryForm.Text = CurrentTreeNode.Text;
		}

		private void ScriptAlter_Click(object sender, System.EventArgs e)
		{
			
//			Database db = new Database();
			string type;
			MainForm frm =  (MainForm)MdiParentForm;
			
			if( ((QCTreeNode)CurrentTreeNode).objecttype == QCTreeNode.ObjectType.Table)
			{
				ScriptCreate_Click(sender,e);
				return;
			}

			if(frm.ActiveQueryForm == null || frm.ActiveQueryForm.Content.Length>0)
				frm.NewQueryform();

			IDatabaseManager db = DatabaseFactory.CreateNew(frm.ActiveQueryForm.dbConnection);
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				
				if(c.ConnectionName == ((QCTreeNode)CurrentTreeNode).server)
				{
					string dbName = CurrentTreeNode.Parent.Parent.Text;
					frm.ActiveQueryForm.SetDatabaseConnection(dbName,c.Connection);
					break;
				}
			}

			string CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
				CurrentTreeNode.Text,
				frm.ActiveQueryForm.dbConnection,
				DBCommon.ScriptType.CREATE, DBCommon.ScriptObjectType.PROCEDURE);


			if(CurrentTreeNode.Parent.Text == "Stored procedures")
				type = "PROCEDURE";
			else
				type = "FUNCTION";

			frm.ActiveQueryForm.Content = Create2Alter(CreateScript,type);
			int startpos = CreateScript.IndexOf("</member>",0);
			if(startpos>0)
				frm.ActiveQueryForm.AddRevisionCommentSection();
			
			//Set database connection
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				string databaseConnection  = CurrentTreeNode.Parent.Parent.Parent.Text;
				if(c.ConnectionName == databaseConnection)
				{
					frm.ActiveQueryForm.SetDatabaseConnection(CurrentTreeNode.Parent.Parent.Text,c.Connection);
					break;
				}
			}
			frm.ActiveQueryForm.Text = CurrentTreeNode.Text;
			//frm.ActiveQueryForm.CheckForReservedWords();
		}

		private void ScriptDrop_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ScriptSelect_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ScriptUpdate_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ScriptInsert_Click(object sender, System.EventArgs e)
		{
			
		}
		private void miCreateInsertScript_Click(object sender, System.EventArgs e)
		{
//			Database db = new Database();
			MainForm frm =  (MainForm)MdiParentForm;
			
			if(frm.ActiveQueryForm == null || frm.ActiveQueryForm.Content.Length>0)
				frm.NewQueryform();

			IDatabaseManager db = DatabaseFactory.CreateNew(frm.ActiveQueryForm.dbConnection);

			foreach(MainForm.DBConnection c in frm.DBConnections)
			{	
				if(c.ConnectionName == CurrentTreeNode.Parent.Parent.Parent.Text)
				{
					string dbName = CurrentTreeNode.Parent.Parent.Text;
					frm.ActiveQueryForm.SetDatabaseConnection(dbName,c.Connection);
					break;
				}
			}

			frm.ActiveQueryForm.Content="SELECT * FROM " + CurrentTreeNode.Text;
			frm.ActiveQueryForm.CreateInsertStatement();

		}
		private void miCreateUpdateScript_Click(object sender, System.EventArgs e)
		{
//			Database db = new Database();
			MainForm frm =  (MainForm)MdiParentForm;
			
			if(frm.ActiveQueryForm == null || frm.ActiveQueryForm.Content.Length>0)
				frm.NewQueryform();
		
			IDatabaseManager db = DatabaseFactory.CreateNew(frm.ActiveQueryForm.dbConnection);

			foreach(MainForm.DBConnection c in frm.DBConnections)
			{	
				if(c.ConnectionName == CurrentTreeNode.Parent.Parent.Parent.Text)
				{
					string dbName = CurrentTreeNode.Parent.Parent.Text;
					frm.ActiveQueryForm.SetDatabaseConnection(dbName,c.Connection);
					break;
				}
			}

			frm.ActiveQueryForm.Content="SELECT * FROM " + CurrentTreeNode.Text;
			frm.ActiveQueryForm.CreateUpdateStatement();
		}
		private void ScriptDelete_Click(object sender, System.EventArgs e)
		{
		
		}
		private void RefreshDataObjects_Click(object sender, System.EventArgs e)
		{
			RefreashTreeView();
		}

		private void mi_CheckIn_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
		
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				if(c.ConnectionName == ((QCTreeNode) CurrentTreeNode).server)
				{
					((QCTreeNode) CurrentTreeNode).CheckIn(c.Connection);
					break;
				}
			}
		}

		private void mi_CheckOut_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
		
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				if(c.ConnectionName == ((QCTreeNode) CurrentTreeNode).server)
				{
					((QCTreeNode) CurrentTreeNode).CheckOut(c.Connection);
					ScriptAlter_Click(sender,e);
					break;
				}
			}
		}

		private void mi_AddToSourceControl_Click(object sender, System.EventArgs e)
		{
			QueryCommander.VSS.VSSConnection vssConnection = new QueryCommander.VSS.VSSConnection();
			vssConnection.Database=((QCTreeNode)CurrentTreeNode).database;
			vssConnection.Server=((QCTreeNode)CurrentTreeNode).server;
			vssConnection.Login();
			((MainForm)MdiParentForm).vssConnectionCollection.Add(vssConnection);
			
			this.RefreashTreeView();
			
		}

		private void mi_DetachFromSourceControl_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<((MainForm)MdiParentForm).vssConnectionCollection.Count;i++)
			{
				if(((MainForm)MdiParentForm).vssConnectionCollection[i].Server == ((QCTreeNode) CurrentTreeNode).vssConnection.Server &&
					((MainForm)MdiParentForm).vssConnectionCollection[i].Database == ((QCTreeNode) CurrentTreeNode).vssConnection.Database)
				{
					((MainForm)MdiParentForm).vssConnectionCollection.RemoveAt(i);
					((QCTreeNode) CurrentTreeNode).vssConnection=null;
					this.RefreashTreeView();
					break;
				}
			}
			

		}
	
		private void miOpenScript_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
			string database = frm.ActiveQueryForm.DatabaseName;
			IDbConnection sqlConnection = frm.ActiveQueryForm.dbConnection;

			string fileName = ((QCTreeNode) CurrentTreeNode).database;
		
			StreamReader sr = new StreamReader(fileName);
			string content = "";
			string line;
			while ((line = sr.ReadLine()) != null) 
			{
				content += "\n" + line;
			}
			sr.Close();

			if(frm.ActiveQueryForm == null || frm.ActiveQueryForm.Content.Length>0)
				frm.NewQueryform();

			frm.ActiveQueryForm.SetDatabaseConnection(database,sqlConnection);
			frm.ActiveQueryForm.Content=content;
			frm.ActiveQueryForm.Text = ((QCTreeNode) CurrentTreeNode).Text;
			frm.ActiveQueryForm.FileName=fileName;
		}
		private void mi_ShowHistory_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
		
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				if(c.ConnectionName == ((QCTreeNode) CurrentTreeNode).server)
				{
					((QCTreeNode) CurrentTreeNode).ShowHistory(c.Connection);
					break;
				}
			}
		}
		
		private void mi_VSSSettings_Click(object sender, System.EventArgs e)
		{
			QueryCommander.VSS.FrmLogin frmLogin = new QueryCommander.VSS.FrmLogin(((QCTreeNode)CurrentTreeNode).vssConnection);
			frmLogin.ShowDialog();

		}

		private void mi_UndoCheckOut_Click(object sender, System.EventArgs e)
		{
			MainForm frm =  (MainForm)MdiParentForm;
		
			foreach(MainForm.DBConnection c in frm.DBConnections)
			{
				if(c.ConnectionName == ((QCTreeNode) CurrentTreeNode).server)
				{
					((QCTreeNode) CurrentTreeNode).UndoCheckOut(c.Connection);
					break;
				}
			}
		}

		#endregion
		#region Methods
		public void RefreashTreeView()
		{
			try
			{
				TvDBObjects.BeginUpdate();
				TvDBObjects.Nodes.Clear();

				QCTreeNode node=new QCTreeNode("SQL servers",QCTreeNode.ObjectType.Top,null,null,null);
				TvDBObjects.Nodes.Add(node);
				DBTreeViewTypes.Add(node,QCTreeNode.ObjectType.Top);
			
				MainForm frm = (MainForm)MdiParentForm;

				//if(_isLoaded)
					frm.RefreashDBConnections();
			
				ArrayList allDatabases = new ArrayList();
				foreach(MainForm.DBConnection dbConnection in frm.DBConnections)
				{
					QCTreeNode serverNode;

					if(dbConnection.FrienlyName==null)
						serverNode = new QCTreeNode(dbConnection.ConnectionName,QCTreeNode.ObjectType.Server,null,null,null);
					else if(dbConnection.FrienlyName.Length==0)
						serverNode = new QCTreeNode(dbConnection.ConnectionName,QCTreeNode.ObjectType.Server,null,null,null);
					else
						serverNode = new QCTreeNode(dbConnection.ConnectionName + " ["+ dbConnection.FrienlyName +"]",QCTreeNode.ObjectType.Server,null,null,null);

					DBTreeViewTypes.Add(serverNode,QCTreeNode.ObjectType.Server);
					node.Nodes.Add(serverNode);
					
					
					IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection.Connection);

					ArrayList dbArr =  db.GetDatabasesObjects(dbConnection.ConnectionName,dbConnection.Connection);
					foreach(QueryCommander.Database.DB database in dbArr)
						allDatabases.Add(database);

					foreach(Database.DB serverDB in dbArr)
					{
						QueryCommander.VSS.VSSConnection vssConnection = frm.vssConnectionCollection.FindByDataBase(serverDB.Server, serverDB.Name);
						if(vssConnection!=null)
						{
							vssConnection.Login();
							if(!vssConnection.IsLogedIn)
								vssConnection=null;
							else
								vssConnection.CreateWorkSpace(serverDB.Server, serverDB.Name,"");
						}
						
						QCTreeNode dbNode = new QCTreeNode(serverDB.Name,QCTreeNode.ObjectType.Database,vssConnection,dbConnection.ConnectionName,serverDB.Name);
						DBTreeViewTypes.Add(dbNode,QCTreeNode.ObjectType.Database );

						QCTreeNode TableNode = new QCTreeNode("Tables",QCTreeNode.ObjectType.Tables,null,null,null);
						DBTreeViewTypes.Add(TableNode,QCTreeNode.ObjectType.Tables);

						QCTreeNode ViewNode = new QCTreeNode("Views",QCTreeNode.ObjectType.Views,null,null,null);
						DBTreeViewTypes.Add(ViewNode,QCTreeNode.ObjectType.Views);

						QCTreeNode SpNode = new QCTreeNode("Stored procedures",QCTreeNode.ObjectType.StoredProcedures,null,null,null);
						DBTreeViewTypes.Add(SpNode,QCTreeNode.ObjectType.StoredProcedures);

						QCTreeNode FnNode = new QCTreeNode("Functions",QCTreeNode.ObjectType.Functions,null,null,null);
						DBTreeViewTypes.Add(FnNode,QCTreeNode.ObjectType.Functions);

						dbNode.Nodes.Add(TableNode);
						dbNode.Nodes.Add(ViewNode);
						dbNode.Nodes.Add(SpNode);
						dbNode.Nodes.Add(FnNode);
						
					
						serverNode.Nodes.Add(dbNode);
						foreach(Database.DBObject dbObject in serverDB.dbObjects)
						{
							QCTreeNode oNode=null;

							switch(dbObject.Type.ToUpper())
							{
								case "U ": //Tables
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.Table,vssConnection,dbConnection.ConnectionName, serverDB.Name);
									DBTreeViewTypes.Add(oNode,QCTreeNode.ObjectType.Table);
									TableNode.Nodes.Add(oNode);
									break;
								case "V ": //Views
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.View,vssConnection,dbConnection.ConnectionName, serverDB.Name);
									DBTreeViewTypes.Add(oNode,QCTreeNode.ObjectType.View);
									ViewNode.Nodes.Add(oNode);
									break;
								case "P ": //Stored Procedures
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.StoredProcedure,vssConnection,dbConnection.ConnectionName, serverDB.Name);
									DBTreeViewTypes.Add(oNode,QCTreeNode.ObjectType.StoredProcedure);
									SpNode.Nodes.Add(oNode);
									break;
								case "FN": //Functions
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.Function,vssConnection,dbConnection.ConnectionName, serverDB.Name);
									DBTreeViewTypes.Add(oNode,QCTreeNode.ObjectType.Function);
									FnNode.Nodes.Add(oNode);
									break;
								case "TF": //Functions
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.Function,vssConnection,dbConnection.ConnectionName, serverDB.Name);
									DBTreeViewTypes.Add(oNode,QCTreeNode.ObjectType.Function);
									FnNode.Nodes.Add(oNode);
									break;
								default:
									oNode=new QCTreeNode(dbObject.Name,QCTreeNode.ObjectType.Table,null,null,null);
									dbNode.Nodes.Add(oNode);
									break;
							}	
							
						}
					}
					node.Expand();
					frm.AlterDatabaseMenuItem(allDatabases );
					
				}
				TvDBObjects.EndUpdate();
				return;
				// Script files
				QCTreeNode ScriptfilesNode = new QCTreeNode("Scriptfiles",QCTreeNode.ObjectType.Tables,null,null,null);
				DBTreeViewTypes.Add(ScriptfilesNode,QCTreeNode.ObjectType.Tables);
				node.Nodes.Add(ScriptfilesNode);

				string directoryPath = Application.StartupPath + @"\Scriptfiles";
				if(!Directory.Exists(directoryPath))
					Directory.CreateDirectory(directoryPath);

				DirectoryInfo di = new DirectoryInfo(directoryPath);
				FileInfo[] files = di.GetFiles("*.sql");
				foreach(FileInfo file in files)
				{
					QCTreeNode n = new QCTreeNode(file.Name,QCTreeNode.ObjectType.ScriptFile,null,null,file.FullName);
					DBTreeViewTypes.Add(n,QCTreeNode.ObjectType.ScriptFile);
					ScriptfilesNode.Nodes.Add(n);
				}

				
				// WorkSpace
				QCTreeNode WorkSpaceCollectionNode = new QCTreeNode("WorkSpaces",QCTreeNode.ObjectType.WorkSpaces,null,null,null);
				DBTreeViewTypes.Add(WorkSpaceCollectionNode,QCTreeNode.ObjectType.WorkSpaces);
				node.Nodes.Add(WorkSpaceCollectionNode);

				foreach(WorkSpace ws in frm.workSpaceCollection)
				{
					QCTreeNode WorkSpaceNode = new QCTreeNode(ws.Name,QCTreeNode.ObjectType.WorkSpace,null,null,ws.Name);
					DBTreeViewTypes.Add(WorkSpaceNode,QCTreeNode.ObjectType.WorkSpace);
					WorkSpaceCollectionNode.Nodes.Add(WorkSpaceNode);

					foreach(WorkSpaceItem file in ws.WorkSpaceItems)
					{
						QCTreeNode nn = new QCTreeNode(file.FileName,QCTreeNode.ObjectType.WorkSpaceItem,null,ws.Name,file.FilePath);
						DBTreeViewTypes.Add(nn,QCTreeNode.ObjectType.WorkSpaceItem);
						WorkSpaceNode.Nodes.Add(nn);
					}
				}

				TvDBObjects.EndUpdate();
			}
			catch(Exception ex)
			{
				TvDBObjects.EndUpdate();
				throw ex;

			}
		}
		
		private bool IsCheckout(QueryCommander.VSS.VSSConnection vssConnection, string type, string objectName)
		{
			if(vssConnection == null)
				return false;

			switch(type.Trim())
			{
				case "U":
					return vssConnection.GetStatus(vssConnection.GetVSSPath(VSS.VSSConnection.DBObjectTypes.Table) + "\\" + objectName)==2;
					break;
				case "V":
					return vssConnection.GetStatus(vssConnection.GetVSSPath(VSS.VSSConnection.DBObjectTypes.View) + "\\" + objectName)==2;
					break;
				case "P":
					return vssConnection.GetStatus(vssConnection.GetVSSPath(VSS.VSSConnection.DBObjectTypes.StoredProcedure) + "\\" + objectName)==2;
					break;
				case "FN":
					return vssConnection.GetStatus(vssConnection.GetVSSPath(VSS.VSSConnection.DBObjectTypes.Function) + "\\" + objectName)==2;
					break;
				case "TF":
					return vssConnection.GetStatus(vssConnection.GetVSSPath(VSS.VSSConnection.DBObjectTypes.Function) + "\\" + objectName)==2;
					break;
				default:
					return false;
			}
		}
		private int ParseText(WordAndPosition[] words, string s)
		{
			words.Initialize();
			int count = 0;
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch()) 
			{
				words[count].Word = m.Value;
				words[count].Position = m.Index;
				words[count].Length = m.Length;
				count++;
			}
			return count;
		}
		private string Create2Alter(string script, string type)
		{
			string returnString=script;
			WordAndPosition[] words  = new WordAndPosition[20000];
			int count = ParseText(words,script);
			
			for(int i=0;i<count;i++)
			{
				if((words[i].Word.ToUpper()=="CREATE") && (
					(words[i+1].Word.ToUpper()=="PROCEDURE" || 
					words[i+1].Word.ToUpper()=="FUNCTION")|| 
					words[i+1].Word.ToUpper()=="VIEW"||
					words[i+1].Word.ToUpper()=="TABLE"))
				{
					returnString = returnString.Substring(0,words[i].Position) + "ALTER" + returnString.Substring(words[i].Position + words[i].Length, returnString.Length-(words[i].Position + words[i].Length));
					break;
				}
			}
			return returnString;
			
		}
//		private void SetContextMenu(QCTreeNode node)
//		{
//			//disabe all items
//			foreach(MenuItem item in contextMenuWorkSpace.MenuItems)
//				item.Visible= false;
//			switch(node.objecttype)
//			{
//				case QCTreeNode.ObjectType.WorkSpaces:
//					menuItemAddWorkspace.Visible=true;
//					break;
//				case QCTreeNode.ObjectType.WorkSpace:
//					menuItemAddActiveDocument.Visible=true;
//					menuItemAddAllDocuments.Visible=true;
//					menuItemDeleteWorkspace.Visible=true;
//					break;
//				case QCTreeNode.ObjectType.WorkSpaceItem:
//					menuItemDeleteItem.Visible=true;
//					menuItemOpenDocument.Visible=true;
//					break;
//			}
//		}
		private void SetContextMenu(TreeNode node)
		{
			//disabe all items
			foreach(MenuItem item in contextMenu1.MenuItems)
				item.Visible= false;
			
			switch((int)DBTreeViewTypes[node])
			{
				case (int)QCTreeNode.ObjectType.Top:
					SetContextMenuItems(true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Server:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Database:
					SetContextMenuItems(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Tables:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.StoredProcedures:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Functions:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Table:
					SetContextMenuItems(false, false, false, false, false, false, false, true, false, false, false, false, false, true, true, false);
					break;
				case (int)QCTreeNode.ObjectType.View:
					SetContextMenuItems(false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.StoredProcedure:
					SetContextMenuItems(false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.Function:
					SetContextMenuItems(false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false);
					break;
				case (int)QCTreeNode.ObjectType.ScriptFile:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);
					break;
				default:
					SetContextMenuItems(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
					break;
			}
			// Source control
			if(node is QCTreeNode)
			{
				mi_DetachFromSourceControl.Visible=false;
				if(((QCTreeNode)node).objecttype==QCTreeNode.ObjectType.Database)
				{
					mi_SourceControl.Visible=true;
					mi_CheckIn.Visible=false;
					mi_CheckOut.Visible=false;
					mi_UndoCheckOut.Visible=false;
					mi_ShowHistory.Visible=false;
					
					if( !((QCTreeNode)node).IsUnderSourceControl)
					{
						mi_AddToSourceControl.Visible=true;
						mi_DetachFromSourceControl.Visible=false;
						mi_VSSSettings.Visible=false;
					}
					else
					{
						mi_AddToSourceControl.Visible=false;
						mi_DetachFromSourceControl.Visible=true;
						mi_VSSSettings.Visible=true;
					}	
					return;
					
				}
				else
				{
					mi_CheckIn.Visible=true;
					mi_CheckOut.Visible=true;
					mi_UndoCheckOut.Visible=true;
					mi_AddToSourceControl.Visible=false;
					mi_SourceControl.Visible=false;
					mi_VSSSettings.Visible=false;
					mi_ShowHistory.Visible=true;;
				}

				
				if( ((QCTreeNode)node).IsUnderSourceControl )
					mi_SourceControl.Visible=true;

				if( ((QCTreeNode)node).IsCheckedOut && ((QCTreeNode)node).objecttype!=QCTreeNode.ObjectType.Database )
				{
					mi_CheckIn.Enabled=true;
					mi_CheckOut.Enabled=false;
					mi_UndoCheckOut.Enabled=true;
				}
				else
				{
					mi_CheckIn.Enabled=false;
					mi_CheckOut.Enabled=true;
					mi_UndoCheckOut.Enabled=false;
				}
				
				if( ((QCTreeNode)node).IsCheckedOutByOtherUser && ((QCTreeNode)node).objecttype!=QCTreeNode.ObjectType.Database )
				{
					mi_CheckIn.Enabled=false;
					mi_CheckOut.Enabled=false;
					mi_UndoCheckOut.Enabled=false;
				}

			}
			
		}
		private void SetContextMenuItems(bool AddServerVisible,
			bool RefreshDataObjects,
			bool RemoveServerVisible,
			bool UseDatabaseVisible,
			bool NewVisible,
			bool DeleteVisible,
			bool ScriptAlterVisible,
			bool ScriptCreateVisible,
			bool ScriptDeleteVisible,
			bool ScriptDropVisible,
			bool ScriptInsertVisible,
			bool ScriptSelectVisible,
			bool ScriptUpdateVisible,
			bool CreateInsertScript,
			bool CreateUpdateScript,
			bool OpenScriptFile)
		{
			miAddServer.Visible = AddServerVisible;
			miRefreshDataObjects.Visible = RefreshDataObjects;
			miRemoveServer.Visible = RemoveServerVisible;
			miUseDatabase.Visible = UseDatabaseVisible;
			miDelete.Visible = DeleteVisible;
			miNew.Visible = NewVisible;
			miScriptAlter.Visible = ScriptAlterVisible;
			miScriptCreate.Visible = ScriptCreateVisible;
			miScriptDelete.Visible = ScriptDeleteVisible;
			miScriptDrop.Visible = ScriptDropVisible;
			miScriptInsert.Visible = ScriptInsertVisible;
			miScriptSelect.Visible = ScriptSelectVisible;
			miScriptUpdate.Visible = ScriptUpdateVisible;
			miCreateInsertScript.Visible=CreateInsertScript;
			miCreateUpdateScript.Visible=CreateUpdateScript;
			miOpenScript.Visible=OpenScriptFile;

			if(ScriptAlterVisible || ScriptCreateVisible|| ScriptDeleteVisible || ScriptDropVisible || ScriptInsertVisible)
				miScript.Visible = true;
			else
				miScript.Visible = false;

			
			
		
		
		}

		public void CreateConstructorString(string objectName)
		{
			MainForm frm =  (MainForm)MdiParentForm;
			frm.statusBar.Panels[3].Text = "Querying for [" + objectName + "]...";
			IDatabaseManager db = DatabaseFactory.CreateNew(frm.ActiveQueryForm.dbConnection);
//			Database db = new Database();

			string CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
				objectName,
				frm.ActiveQueryForm.dbConnection,
				DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.PROCEDURE);

			if(CreateScript.Length==0)
			{
				CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
					objectName,
					frm.ActiveQueryForm.dbConnection,
					DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.TABLE);
			}
			if(CreateScript.Length==0)
			{
				CreateScript = db.GetObjectConstructorString(frm.ActiveQueryForm.DatabaseName,
					objectName,
					frm.ActiveQueryForm.dbConnection,
					DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.VIEW);
			}

			if(CreateScript.Length == 0)
			{
				MessageBox.Show("Stored procedure or function [" + objectName + "] \nnot found in the [" + frm.ActiveQueryForm.DatabaseName + "] database.","Reference",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				frm.statusBar.Panels[3].Text="";
				return;
			}
			CreateScript = Create2Alter(CreateScript,"");
			// Use database
			if(frm.ActiveQueryForm == null)
			{
				frm.statusBar.Panels[3].Text="";
				return;
			}

			IDbConnection c = frm.ActiveQueryForm.dbConnection;
			string dbName = frm.ActiveQueryForm.DatabaseName;

			frm.NewQueryform();
			
			frm.ActiveQueryForm.SetDatabaseConnection(dbName, c);
			
			if(CreateScript.IndexOf("</member>",0)>0)
				frm.ActiveQueryForm.Content = AddRevisionCommentSection(CreateScript);
			else
				frm.ActiveQueryForm.Content = CreateScript;

			frm.ActiveQueryForm.Text = objectName;
			
			frm.statusBar.Panels[3].Text="";
			
		}
		public string  AddRevisionCommentSection(string content)
		{
			int startpos = content.IndexOf("</member>",0);
			if(startpos<1)
				return content;

			startpos = content.LastIndexOf("</revision>") + 11;
			return content.Substring(0,startpos) + "\n\t<revision author='" + SystemInformation.UserName.ToString()  + "' date='" + DateTime.Now.ToString() + "'>Altered</revision>" + content.Substring(startpos);
		
		}

		
		#endregion		
	}
}
