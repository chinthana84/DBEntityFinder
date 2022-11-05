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
using System.Windows.Forms;
using IVSSFunctionLibrary;
using System.Xml.Serialization;
using System.IO;
using DifferenceEngine;

namespace QueryCommander.VSS
{
	/// <summary>
	/// Summary description for VSSConnection.
	/// </summary>
	[Serializable]
	public class VSSConnection
	{
		public clIVSSLibrary IVSS = new clIVSSLibrary();

		public enum DBObjectTypes
		{
			Table,
			View,
			StoredProcedure,
			Function,
			None
		}
		
		string pathString =  @"{0}\{1}\Databases\{2}\{3}";
		string _parentProject;
		string _server;
		string _database;
		bool _isLoggedin=false;
		public string VSSDatabasePath;
		public string UserName;
		public string Password;
		public bool RememberPassword;
		public string ParentProject
		{
			get{return _parentProject;}
			set
			{
                 
			IVSS.SetCurrentProject(_parentProject);
								_parentProject = value;
                    
			
			}
		}

		public string Server
		{
			get{return _server;}
			set{_server=value;}
		}
		public string Database
		{
			get{return _database;}
			set{_database=value;}
		}

		public bool IsLogedIn
		{
			get{return _isLoggedin;}
		}


		public void Login()
		{
			if(!this.IsLogedIn)
			{
				FrmLogin frmLogin = new FrmLogin(this);
				if(frmLogin.IsLoggedIn)
				{
					this.IVSS=frmLogin.IVSS;
					CreateWorkSpace(Server, Database,"");
					_isLoggedin=true;
				}
				else if(frmLogin.ShowDialog()==DialogResult.OK)
				{
					this.IVSS=frmLogin.IVSS;
					CreateWorkSpace(Server, Database,"");
					_isLoggedin=true;
				}
			}
		}
		
		public int GetStatus(string filePath)
		{
			int status=0;
			string msg = IVSS.GetCheckOutState(filePath, ref status);
			
			return status;
		}
		public string GetHistory(string filePath)
		{
			string fileName = GetVSSFileName(filePath);
			string[,] hitoryitems;
			
			string ret = IVSS.GetHistory(out hitoryitems,filePath,1073745920);

			if(ret.Length>0)
			{
				MessageBox.Show(ret,"Visual Source Safe");
				return"";
			}
			VSSHitoryItemCollection vssHitoryItemCollection = new VSSHitoryItemCollection();

			for(int count= hitoryitems.GetUpperBound(0);count>0;count--)
			{
				VSSHitoryItem item = new VSSHitoryItem();
				item.Text = hitoryitems[count, (int)VSSEnums.HistoryItems.VersionNumber];
				item.Username = hitoryitems[count, (int)VSSEnums.HistoryItems.UserName];
				item.Date = hitoryitems[count, (int)VSSEnums.HistoryItems.Date];
				item.Action = hitoryitems[count, (int)VSSEnums.HistoryItems.Action];
				vssHitoryItemCollection.Add(item);
			}
			FrmShowHistory frm = new FrmShowHistory(vssHitoryItemCollection);
			
			frm.Text=fileName + " history";
			frm.HistoryLabel.Text=hitoryitems.GetUpperBound(0).ToString() + " history items";
			
			if(frm.ShowDialog()==DialogResult.OK)
			{
				// ShowDiff
				IVSSFlags.VSSFlags flags = new IVSSFlags.VSSFlags();
				int version1 = Convert.ToInt16( frm.HistoryList.SelectedItems[0].Text);
				int version2 = Convert.ToInt16( frm.HistoryList.SelectedItems[1].Text);
				int flag = (int)flags.FlagReplaceLocalReplace();
				IVSS.GetVersionByVersionNumber(filePath,version1,Application.StartupPath + "\\version1",flag);
				IVSS.GetVersionByVersionNumber(filePath,version2,Application.StartupPath + "\\version2",flag);
				
			
				TextDiff(Application.StartupPath + "\\version1",
					Application.StartupPath + "\\version2");

				return Application.StartupPath + "\\version1|"+Application.StartupPath + "\\version1";

			}

			return "";
		}
		public static void TextDiff(string sFile, string dFile)
		{
			DiffList_TextFile file1 = null;
			DiffList_TextFile file2 = null;
			try
			{
				file1 = new DiffList_TextFile(sFile);
				file2 = new DiffList_TextFile(dFile);
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message, "File Error");
				return;
			}
			try
			{
				double num1 = 0;
				DiffEngine engine1 = new DiffEngine();
				num1 = engine1.ProcessDiff(file1, file2, DifferenceEngine.DiffEngineLevel.Medium);
				ArrayList list1 = engine1.DiffReport();
				FrmShowDiff results1 = new FrmShowDiff(file1, file2, list1, num1);
				results1.ShowDialog();
				results1.Dispose();
			}
			catch (Exception exception2)
			{
				string text1 = string.Format("{0}{1}{1}***STACK***{1}{2}", exception2.Message, Environment.NewLine, exception2.StackTrace);
				MessageBox.Show(text1, "Compare Error");
				return;
			}
		}
		private string GetVSSFileName(string filePath)
		{
			return filePath.Substring(filePath.LastIndexOf("\\")+1);
		}
		public void CreateWorkSpace(string serverName, string databaseName, string comment)
		{
			string ret="";
			
			ret=IVSS.CreateProject(ParentProject,serverName,"");
			ret=IVSS.CreateProject(ParentProject+"\\"+serverName,"Databases","");
			ret=IVSS.CreateProject(ParentProject+"\\"+serverName + "\\Databases",databaseName,comment);

			ret=IVSS.CreateProject(ParentProject+"\\"+serverName + "\\Databases\\"+databaseName, "Tables",comment);
			ret=IVSS.CreateProject(ParentProject+"\\"+serverName + "\\Databases\\"+databaseName, "Views",comment);
			ret=IVSS.CreateProject(ParentProject+"\\"+serverName + "\\Databases\\"+databaseName, "Stored procedures",comment);
			ret=IVSS.CreateProject(ParentProject+"\\"+serverName + "\\Databases\\"+databaseName, "Functions",comment);

//			string tablesDir = GetVSSPath(DBObjectTypes.Table);
//			if(!Directory.Exists(tablesDir))	
//				Directory.CreateDirectory(tablesDir);
//
//			string viewsDir = GetVSSPath(DBObjectTypes.View);
//			if(!Directory.Exists(viewsDir))	
//				Directory.CreateDirectory(viewsDir);
//
//			string spDir = GetVSSPath(DBObjectTypes.StoredProcedure);
//			if(!Directory.Exists(spDir))	
//				Directory.CreateDirectory(spDir);
//
//			string fnDir = GetVSSPath(DBObjectTypes.Function);
//			if(!Directory.Exists(fnDir))	
//				Directory.CreateDirectory(fnDir);
			
		}

		public void AddItem(DBObjectTypes type, string ItemToAddPath, string AddComment)
		{
			try
			{
				IVSS.SetCurrentProject(GetVSSPath(type));
				string ret =IVSS.AddItem(GetVSSPath(type),ItemToAddPath,AddComment,0);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		}
		public void AddItem(string VSSPath, string ItemToAddPath, string AddComment)
		{
			try
			{
				IVSS.SetCurrentProject(VSSPath);
				string ret =IVSS.AddItem(VSSPath,ItemToAddPath,AddComment,0);
				int y=0;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		}
		public void CheckOutItem( DBObjectTypes type, string ItemToCheckOut, string CheckOutPath, string Comment)
		{
			try
			{
				ItemToCheckOut= GetVSSPath(type) + "\\" + ItemToCheckOut;
				string ret =IVSS.CheckOutItem(ItemToCheckOut,CheckOutPath,ref Comment,0);
				if(ret.Length>0)
					MessageBox.Show(ret, "Visual SourceSafe");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		
		}
		public void CheckOutItem( string VSSPath, string CheckOutPath, string Comment)
		{
			try
			{
				string ret =IVSS.CheckOutItem(VSSPath,CheckOutPath,ref Comment,0);
				if(ret.Length>0)
					MessageBox.Show(ret, "Visual SourceSafe");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		}
		public void UndoChechOut( string VSSPath, string CheckOutPath, string Comment)
		{
			try
			{
				string ret = IVSS.UnCheckOutItem(VSSPath,CheckOutPath,0);
				if(ret.Length>0)
					MessageBox.Show(ret, "Visual SourceSafe");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		

		}
		public void CheckInItem( DBObjectTypes type, string ItemToCheckIn, string CheckInPath, string Comment)
		{
			try
			{
				ItemToCheckIn= GetVSSPath(type) + "\\" + ItemToCheckIn;
				string ret =IVSS.CheckInItem(ItemToCheckIn,CheckInPath,ref Comment,0);
				if(ret.Length>0)
					MessageBox.Show(ret, "Visual SourceSafe");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		
		}
		public void CheckInItem( string VSSPath,  string CheckInPath, string Comment)
		{
			try
			{
				FrmAddComment frm = new FrmAddComment();
				if(frm.ShowDialog()==DialogResult.Cancel)
					return;
				
				string comment=frm.txtComment.Text;
				string ret =IVSS.CheckInItem(VSSPath,CheckInPath,ref comment,0);
				if(ret.Length>0)
					MessageBox.Show(ret, "Visual SourceSafe");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Visual SourceSafe");
			}
		
		}
		
		public string GetVSSPath(DBObjectTypes type)
		{
			switch(type)
			{
				case DBObjectTypes.Table:
					return String.Format(pathString,ParentProject, _server,_database,"Tables");
					break;
				case DBObjectTypes.View:
					return String.Format(pathString,ParentProject, _server,_database,"Views");
					break;
				case DBObjectTypes.StoredProcedure:
					return String.Format(pathString,ParentProject, _server,_database,"Stored procedures");
					break;
				case DBObjectTypes.Function:
					return String.Format(pathString,ParentProject, _server,_database,"Functions");
					break;
				default:
					return "";
					break;
			}
		}

		
	}
}
