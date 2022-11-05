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
using System.Data;
using QueryCommander.General;
using QueryCommander.Database;

namespace QueryCommander.General
{
	/// <summary>
	/// Summary description for QCTreeNode.
	/// </summary>
	public class QCTreeNode : System.Windows.Forms.TreeNode
	{
		#region Enums
		public enum ObjectType
		{
			Top = 0, 
			Server=1,
			Database=2, 
			Tables=3, 
			StoredProcedures=4, 
			Functions=5, 
			Table=6, 
			StoredProcedure=7, 
			Function=8,
			Views=9,
			View=10,
			Project=11,
			Unknown=12,
			CheckedOutItem=13,
			ScriptFile=14,
			MyWorkSpaces,
			Folder,
			WorkSpaces,
			WorkSpace,
			WorkSpaceItem
		};
		#endregion
		#region Constructor		
		public QCTreeNode(string objectName, ObjectType objecttype, VSS.VSSConnection vssConnection,string server, string database)
		{
			this.Text=objectName;
			this.objectName=objectName;
			this.objecttype=objecttype;
			this.vssConnection=vssConnection;
			this.server=server;
			this.database=database;
			this.ImageIndex=GetImageIndex();
			this.SelectedImageIndex=GetImageIndex();
				
		}
		#endregion
		#region Fields
		public string objectName;
		public string server;
		public string database;
		public ObjectType objecttype;
		public VSS.VSSConnection vssConnection;
		#endregion
		#region Properies
		public bool IsUnderSourceControl
		{
			get
			{
				if(vssConnection==null)
					return false;
					
				return true;
			}
		}
		public bool IsCheckedOut
		{
			get
			{
				if(vssConnection==null)
					return false;

				if(vssConnection.GetStatus(vssPath)==2)
					return true;

				return false;
			}
		}
		public bool IsCheckedOutByOtherUser
		{
			get
			{
				if(vssConnection==null)
					return false;

				if(vssConnection.GetStatus(vssPath)==1)
					return true;

				return false;
			}
		}
	
		public string filePath
		{
			get{return Application.StartupPath + "\\" +objectName + ".SQL";}
		}

		public string vssPath
		{
			get{return vssConnection.GetVSSPath(vssObjectType) + "\\" + objectName+".SQL";}
		}
		private VSS.VSSConnection.DBObjectTypes vssObjectType
		{
			get
			{
				switch(this.objecttype)
				{
					case ObjectType.Table:
						return VSS.VSSConnection.DBObjectTypes.Table;
					case ObjectType.View:
						return VSS.VSSConnection.DBObjectTypes.View;
					case ObjectType.StoredProcedure:
						return VSS.VSSConnection.DBObjectTypes.StoredProcedure;
					case ObjectType.Function:
						return VSS.VSSConnection.DBObjectTypes.Function;
					default:
						return VSS.VSSConnection.DBObjectTypes.None;
				}
			}
		}
			
		#endregion
		#region private Methods
		private int GetImageIndex()
		{
			if(this.IsCheckedOut)
				return 9;
			if(this.IsCheckedOutByOtherUser)
				return 10;

			switch(this.objecttype)
			{
				case ObjectType.Top:
					return 7;
				case ObjectType.Server:
					return 0;
				case ObjectType.Database:
					if(this.IsUnderSourceControl)
						return 11;
					else
						return 1;
				case ObjectType.Tables:
					return 6;
				case ObjectType.StoredProcedures:
					return 6;
				case ObjectType.Views:
					return 6;
				case ObjectType.Functions:
					return 6;
				case ObjectType.Table:
					return 4;
				case ObjectType.View:
					return 4;
				case ObjectType.StoredProcedure:
					return 3;
				case ObjectType.Function:
					return 2;
				case ObjectType.ScriptFile:
					return 4;

				case ObjectType.WorkSpaces:
					return 0;
				case ObjectType.WorkSpace:
					return 1;
				case ObjectType.WorkSpaceItem:
					return 2;

			}
			return 6;
		}
		private string GetScript(IDbConnection sqlConnection)
		{
			IDatabaseManager db = DatabaseFactory.CreateNew(sqlConnection);
			string CreateScript;
			//Database db = new Database();				
				
			if(this.objecttype==ObjectType.Table)
				CreateScript = db.GetObjectConstructorString(this.database,
					this.objectName,
					sqlConnection,
					DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.TABLE);
			else
				CreateScript = db.GetObjectConstructorString(this.database,
					this.objectName,
					sqlConnection,
					DBCommon.ScriptType.CREATE,DBCommon.ScriptObjectType.PROCEDURE);

			return CreateScript;
		}

		private void CreateFile(string CreateScript)
		{
			if(File.Exists(filePath))
			{
				FileInfo fi = new FileInfo(filePath);

				// remove readonly attribute
				if ((fi.Attributes & System.IO.FileAttributes.ReadOnly) != 0)
					fi.Attributes -= System.IO.FileAttributes.ReadOnly;

				File.Delete(filePath);
			}
			StreamWriter sr = File.CreateText(filePath);
			sr.WriteLine("Checking out");
			sr.Write(CreateScript);
			sr.Close();
			
		}
		#endregion
		#region public Methods
		public void CheckIn(IDbConnection sqlConnection)
		{
			string CreateScript =GetScript(sqlConnection);
				
			CreateFile(CreateScript);

			int pos = vssPath.LastIndexOf("\\");
			string path = vssPath.Substring(0,vssPath.LastIndexOf("\\"));

			int status =vssConnection.GetStatus(vssPath);

			if(status==0)
				vssConnection.AddItem(path ,filePath,"");
				
			vssConnection.CheckInItem(vssPath,filePath,"");
				
			FileInfo fi = new FileInfo(filePath);
			if ((fi.Attributes & System.IO.FileAttributes.ReadOnly) != 0)
				fi.Attributes -= System.IO.FileAttributes.ReadOnly;

			File.Delete(filePath);

			this.ImageIndex=GetImageIndex();
			this.SelectedImageIndex=GetImageIndex();
		}
		public void ShowHistory(IDbConnection sqlConnection)
		{
			try
			{
				string ret =vssConnection.GetHistory(vssPath);
			}
			catch
			{
				MessageBox.Show("Object has not been checked in", "Visual SourceSafe");
				return;
			}

		}
		public void CheckOut(IDbConnection sqlConnection)
		{
			string CreateScript =GetScript(sqlConnection);
				
			CreateFile(CreateScript);

			int pos = vssPath.LastIndexOf("\\");
			string path = vssPath.Substring(0,vssPath.LastIndexOf("\\"));

			int status =vssConnection.GetStatus(vssPath);

			if(status==0)
				vssConnection.AddItem(path ,filePath,"");

				
			vssConnection.CheckOutItem(vssPath,filePath,"");
				
			FileInfo fi = new FileInfo(filePath);
			if ((fi.Attributes & System.IO.FileAttributes.ReadOnly) != 0)
				fi.Attributes -= System.IO.FileAttributes.ReadOnly;

			File.Delete(filePath);

			this.ImageIndex=GetImageIndex();
			this.SelectedImageIndex=GetImageIndex();
		}
		public void UndoCheckOut(IDbConnection sqlConnection)
		{
			string CreateScript =GetScript(sqlConnection);
				
			CreateFile(CreateScript);

			int pos = vssPath.LastIndexOf("\\");
			string path = vssPath.Substring(0,vssPath.LastIndexOf("\\"));

			int status =vssConnection.GetStatus(vssPath);

			if(status==0)
				vssConnection.AddItem(path ,filePath,"");

				
			vssConnection.UndoChechOut(vssPath,filePath,"");
				
			FileInfo fi = new FileInfo(filePath);
			if ((fi.Attributes & System.IO.FileAttributes.ReadOnly) != 0)
				fi.Attributes -= System.IO.FileAttributes.ReadOnly;

			File.Delete(filePath);

			this.ImageIndex=GetImageIndex();
			this.SelectedImageIndex=GetImageIndex();
		}
			
		#endregion	
	}
}
