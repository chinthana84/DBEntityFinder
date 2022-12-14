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
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using QueryCommander.Database;

namespace QueryCommander.Database.Oracle._9i
{
	/// <summary>
	/// Summary description for DataManager.
	/// </summary>
	public class DataManager : IDatabaseManager
	{
		#region Private Members & Enums
		XmlDocument _queryDocument=null;
		private string[] _gos = new string[3] { "GO\n","GO\t"," GO "};
		private ArrayList _Gos;
		private QueryCommands _commands;// = new DBCommands();
		private XmlDocument _xmlDocument=null;
		private OracleDataAdapter _dataAdapter;
		private static ArrayList _sqlInfoMessages = new ArrayList();
		private OracleCommand _currentCommand;
		#endregion	
		#region IDatabaseManager Members
		public XmlDocument xmlDocument
		{
			get{return _xmlDocument;}
			set{_xmlDocument=value;}
		}

		public IDataAdapter DataAdapter
		{
			get{return _dataAdapter;}
			set{_dataAdapter=(OracleDataAdapter)value;}
		}	

		#endregion
		#region Public Methods
		public DataManager()
		{
			string path="QueryCommander.Database.Oracle._9i.Meta.QueryStrings.xml";
			_queryDocument=DBCommon.ReadEmbeddedResource(path);

			_commands = new QueryCommands(_queryDocument);
		}
		public bool StopExecuting()
		{
			try
			{
				_currentCommand.Cancel();;
				return true;
			}
			catch
			{
				return false;
			}

		}
		/// <summary>
		/// Executes all queries (SELECT statements) 
		/// This methods is overloaded
		/// </summary>
		/// <param name="command"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public DataTable ExecuteCommand (string command, IDbConnection  dataConnection)
		{
			OracleDataAdapter dataAdapter;
			OracleCommand     dataCommand;
			DataSet			 dataSet = new DataSet();
			DataTable        dataTable = new DataTable();
			string tableName = "Query";
			try 
			{
				dataCommand = new OracleCommand(command, (OracleConnection)dataConnection);	
				dataCommand.CommandTimeout=30;
				dataAdapter = new OracleDataAdapter(dataCommand);
				dataAdapter.Fill(dataSet, tableName);
				dataTable = dataSet.Tables[tableName];
			}
			catch
			{
				return dataTable;

			}
			return dataTable;
		}
		/// <summary>
		/// Executes all queries (SELECT statements) 
		/// This methods is overloaded
		/// </summary>
		/// <param name="command"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public DataTable ExecuteCommand (string command, IDbConnection  dataConnection,string databaseName)
		{
			try{dataConnection.ChangeDatabase(databaseName);}
			catch
			{
				dataConnection = new OracleConnection(dataConnection.ConnectionString);
				dataConnection.Open();
				dataConnection.ChangeDatabase(databaseName);
			}
			try
			{
				return ExecuteCommand(command,dataConnection);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		
		/// <summary>
		/// The same as ExecuteCommand but returns a dataset. Primarly used for multiple queries.
		/// </summary>
		/// <param name="command"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public DataSet ExecuteCommand_DataSet (string command, IDbConnection dataConnection, string databaseName)
		{
			DataSet ds = null;
			char[] del = ";".ToCharArray() ;
			string[] commands = command.Split(del);
			for(int i=0;i<commands.Length;i++)
			{
				bool isQuery=false;
				bool isNoneQuery=false;
			
				string currentCommand = commands[i].Trim();

				if(currentCommand.Length==0)
					continue;

				if(currentCommand.ToUpper().IndexOf("SELECT")>=0)
					isQuery=true;
			
				if(currentCommand.ToUpper().IndexOf("INSERT")>=0)
					isNoneQuery=true;
				else if(currentCommand.ToUpper().IndexOf("UPDATE")>=0)
					isNoneQuery=true;
				else if(currentCommand.ToUpper().IndexOf("DELETE")>=0)
					isNoneQuery=true;

				if(isQuery && isNoneQuery)
				{
					DialogResult res = System.Windows.Forms.MessageBox.Show("QueryCommander is unable to determine whether this statement is a query or none query\nClick [Yes] for query, [No] for none query", "Resolve statement", 
						System.Windows.Forms.MessageBoxButtons.YesNoCancel,
						System.Windows.Forms.MessageBoxIcon.Question);

					if(res==DialogResult.Yes)
					{
						DataTable dt=null;
							DataSet tmpDs = ExecuteCommand_DataSet1(currentCommand,dataConnection,databaseName,true);
						if(tmpDs.Tables.Count>0)
							dt = tmpDs.Tables[0].Clone();

						if(ds==null)
							ds=new DataSet();

						ds.Tables.Add(dt);
					}
					else if(res==DialogResult.No)
						ExecuteCommand_DataSet1(currentCommand,dataConnection,databaseName,false);

				}
				else if(isQuery)
				{
					DataTable dt=null;
					DataSet tmpDs = ExecuteCommand_DataSet1(currentCommand,dataConnection,databaseName,true);
					if(tmpDs.Tables.Count>0)
						dt = tmpDs.Tables[0].Copy();

					if(ds==null)
						ds=new DataSet();

					dt.TableName = "Query" + ds.Tables.Count.ToString();
					ds.Tables.Add(dt);
				}
				else
				{
					ExecuteCommand_DataSet1(currentCommand,dataConnection,databaseName,false);
				}
			}
			return ds;
		
		}
		private DataSet ExecuteCommand_DataSet1 (string command, IDbConnection dataConnection, string databaseName, bool IsQuery )
		{
			DataSet				dataSet = new DataSet();
			DataTable			dataTable = new DataTable();
			int length = command.Length;
			
			string tableName="Query";
			
			try 
			{	
				_currentCommand = new OracleCommand(command, (OracleConnection)dataConnection);
				_currentCommand.CommandTimeout = 30;
				if(IsQuery)
				{
					DataAdapter= new OracleDataAdapter(_currentCommand);
					OracleCommandBuilder commandBuilder = new OracleCommandBuilder(_dataAdapter);
					_dataAdapter.Fill(dataSet,tableName);
					return dataSet;
				}
				else
				{
					_currentCommand.ExecuteNonQuery();
					return null;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			return dataSet;
		
		}

		/// <summary>
		/// Execute select statement, proceses the data to return UPDATE statements.
		/// </summary>
		/// <param name="command"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public string GetUpdateStatements(string command, IDbConnection dataConnection,string databaseName)
		{
			string Result = "/* Generated by QueryCommander */\n\n";
			string inString = "";
			string valueString = "";
			string TableName = "";
			string WhereString ="";

			command = command.Replace("\t"," ");
			command = command.Replace("\n"," ");


			int startPos =  command.ToUpper().IndexOf("FROM") + 5;
			int endPos = 0;
			
			int lastTab = command.IndexOf("\t",startPos) == -1 ? startPos : command.IndexOf("\t",startPos);
			int lastSpace = command.IndexOf(" ",startPos) == -1 ? startPos : command.IndexOf(" ",startPos);
			
			
			if(lastTab == startPos)
				endPos = lastSpace;
			
			if(lastSpace == startPos)
				endPos = lastTab;
			
			if(lastSpace==startPos && lastTab==startPos)
				endPos = command.Length;
			
			if(lastSpace>startPos && lastTab>startPos)
				endPos = lastTab > lastSpace  ? lastSpace : lastTab;
			
			TableName = command.Substring(startPos,endPos-startPos).Trim();

			DataTable dt = ExecuteCommand(command, dataConnection,databaseName);
			
			inString = "UPDATE " + TableName + "\nSET ";
			foreach(DataRow row in dt.Rows)
			{
				for(int i=0;i<row.ItemArray.Length;i++)
				{
					if(dt.Columns[i].ColumnName.ToUpper() == "ID")
						WhereString = "WHERE ID = '" + row.ItemArray[i].ToString() + "'";

					if(dt.Columns[i].DataType == System.Type.GetType("System.String") || 
						dt.Columns[i].DataType == System.Type.GetType("System.DateTime"))
					{
						valueString += dt.Columns[i].ColumnName + " = " + "'" + row.ItemArray[i].ToString().Replace("'","''") + "', ";
					}
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Guid"))
					{
						string v = "'" + row.ItemArray[i].ToString().Replace("'","''") + "', ";
						if(v=="'', ")
							v = "null, ";
						
						valueString += dt.Columns[i].ColumnName + " = " + v;
					}
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Byte[]"))
						valueString = valueString;
						//valueString += dt.Columns[i].ColumnName + " = " +"null, ";
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Boolean"))
					{
						if( row.ItemArray[i].ToString() == "True")
							valueString += dt.Columns[i].ColumnName + " = " + "1, ";
						else
							valueString += dt.Columns[i].ColumnName + " = " + "0, ";
					}
					else
						valueString += dt.Columns[i].ColumnName + " = " + row.ItemArray[i].ToString() + ", ";
				}

				Result += inString + valueString.Substring(0,valueString.Length-2) + "\n" + WhereString + "\n\n";
				valueString = "";

			}
			return Result;
		}
		/// <summary>
		/// Execute select statement, proceses the data to return INSERT statements.
		/// </summary>
		/// <param name="command"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public string GetInsertStatements(string command, IDbConnection dataConnection,string databaseName)
		{
			string Result = "/* Generated by QueryCommander */\n\n";
			string inString = "";
			string valueString = "";
			string TableName = "";

			command = command.Replace("\t"," ");
			command = command.Replace("\n"," ");


			int startPos =  command.ToUpper().IndexOf("FROM") + 5;
			int endPos = 0;
			
			int lastTab = command.IndexOf("\t",startPos) == -1 ? startPos : command.IndexOf("\t",startPos);
			int lastSpace = command.IndexOf(" ",startPos) == -1 ? startPos : command.IndexOf(" ",startPos);
			
			
			if(lastTab == startPos)
				endPos = lastSpace;
			
			if(lastSpace == startPos)
				endPos = lastTab;
			
			if(lastSpace==startPos && lastTab==startPos)
				endPos = command.Length;
			
			if(lastSpace>startPos && lastTab>startPos)
				endPos = lastTab > lastSpace  ? lastSpace : lastTab;
			
			TableName = command.Substring(startPos,endPos-startPos).Trim();

			DataTable dt = ExecuteCommand(command, dataConnection,databaseName);
			
			inString = "INSERT INTO " + TableName + "(";
			foreach(DataColumn col in dt.Columns)
			{
				inString += "[" + col.ColumnName + "], ";
			}
			inString = inString.Substring(0,inString.Length-2) + ") \nVALUES (";

			foreach(DataRow row in dt.Rows)
			{
				for(int i=0;i<row.ItemArray.Length;i++)
				{
					if(dt.Columns[i].DataType == System.Type.GetType("System.String") || 
						//dt.Columns[i].DataType == System.Type.GetType("System.Guid")  || 
						dt.Columns[i].DataType == System.Type.GetType("System.DateTime"))
					{
						valueString += "'" + row.ItemArray[i].ToString().Replace("'","''") + "', ";
					}
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Guid"))
					{
						string v = "'" + row.ItemArray[i].ToString().Replace("'","''") + "', ";
						if(v=="'', ")
							v = "null, ";
						
						valueString += v;
					}
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Byte[]"))
						valueString += "null, ";
					else if(dt.Columns[i].DataType == System.Type.GetType("System.Boolean"))
					{
						if( row.ItemArray[i].ToString() == "True")
							valueString += "1, ";
						else
							valueString += "0, ";
					}
					else
						valueString += row.ItemArray[i].ToString() + ", ";
				}

				Result += inString + valueString.Substring(0,valueString.Length-2) + ")\n\n";
				valueString = "";

			}
			return Result;
		}


		/// <summary>
		/// Returns a table definition
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public string GetCreateTableString(string tableName, IDbConnection dataConnection,string databaseName)
		{
			string sql = "CREATE TABLE[" + tableName + "](\n";
			string rowString;
			string lengthString;
			string precitionString;
			string allowNull;
			string constrintString;
			string collateString;
			string defaultString;
			string constraint = "";
			int currentRow = 1;

			DataTable ds;
			DataTable dsReferense;
			DataTable dsLength;

			string colCommand = "exec sp_MShelpcolumns N'[" + tableName + "]', @orderby = 'id'";
			string constrintCommand = "exec sp_MStablekeys N'[" + tableName + "]', null, 14";
			string referenseCommand = "SELECT  c.Name as cName, o.Name as oName " + 
				"from dbo.sysindexes ix " +  
				"join   dbo.sysforeignkeys ixk on ixk.rkeyid = ix.id " + 
				"join   dbo.syscolumns c on c.id = ix.id and ixk.keyno = c.colorder " + 
				"join  dbo.sysobjects o on o.id = c.id " + 
				"where ix.name = '?'";
			string lengthCommand = "select length, xprec, xscale " +
				"from dbo.systypes " +
				"where name = '?' ";

			try
			{
				// Table columns
				ds=ExecuteCommand(colCommand,dataConnection,databaseName);

				foreach(DataRow row in ds.Rows)
				{
					string lengthCmd = lengthCommand.Replace("?", row["col_typename"].ToString());
					dsLength=ExecuteCommand(lengthCmd,dataConnection,databaseName);

					allowNull = (row["col_null"].ToString()=="False")? "NOT NULL " : "NULL ";
					constrintString = (row["col_dridefname"].ToString().Length==0)? "" : allowNull + " CONSTRAINT ["+row["col_dridefname"]+"] ";
					collateString = (row["collation"].ToString().Length==0)? "" : "COLLATE "+row["collation"]+"  " + allowNull + " ";
					defaultString = (row["text"].ToString().Length==0)? "" : "DEFAULT "+row["text"];
				
					lengthString = (row["col_len"].ToString() == dsLength.Rows[0][0].ToString())?"": "(" + row["col_len"].ToString() + ") ";
					precitionString = (row["col_prec"] != null) ?"": "(" + row["col_prec"] + "," + row["col_scale"].ToString() + ") ";
				
					rowString = "\t[" + row["col_name"] + "] [" + row["col_typename"] + "] " + lengthString + precitionString  + constrintString + collateString +defaultString ;
				
					if(currentRow != ds.Rows.Count)
						rowString += ",\n";
					else
						rowString += "\n";

					sql +=rowString;
					currentRow++;
				
				}

				// Table constraints
				currentRow = 1;
				ds=ExecuteCommand(constrintCommand,dataConnection,databaseName);
				foreach(DataRow row in ds.Rows)
				{
					int i;
					constraint += "\tCONSTRAINT [" +  row["cName"].ToString() + "]";
					switch(row["ctype"].ToString())
					{
						case "1":
							constraint += " PRIMARY KEY ";
							constraint += (row["cFlags"].ToString()=="1")? "CLUSTERED " : "NONCLUSTERED ";
							break;
						case "2":
							constraint += " UNIQUE ";
							constraint += (row["cFlags"].ToString()=="1")? "CLUSTERED " : "NONCLUSTERED ";
							break;
						case "3":
							constraint += " FOREIGN KEY ";
							break;
					}
					constraint += "\n\t(";

					for(int colCount=0;colCount<(int)row["cColCount"];colCount++)
					{
						i=colCount +1;
						constraint += "\n\t\t[" + row["cKeyCol" + i.ToString()].ToString() + "]";
						if(i == (int)row["cColCount"])
							constraint += "";
						else
							constraint += ",";
					}
					if(row["cGroupName"].ToString().Length > 0)
						constraint += "\n\t) ON [" + row["cGroupName"].ToString() + "]";
				
					if(row["cRefKey"].ToString().Length > 0)
					{
						string referenseCmd = referenseCommand.Replace("?", row["cRefKey"].ToString());
						dsReferense = ExecuteCommand(colCommand,dataConnection,databaseName);
						constraint +=  "\n\t)REFERENCES [" + dsReferense.Rows[1][0]  + "](\n\t\t[" + dsReferense.Rows[0][0] + "]\n\t)";
					}

					if(currentRow != ds.Rows.Count)
						sql += constraint + ",";
					
					constraint  ="\n";

					currentRow++;
				}
				sql += constraint + "\n)ON [PRIMARY]";

				return sql;
			}
			catch(Exception e)
			{
				string h = e.Message;
				return "";
			}

		
		}
		
		/// <summary>
		/// Returns ALL database objects. Used to populate the Object browser
		/// </summary>
		/// <param name="ServerName"></param>
		/// <param name="dataConnection"></param>
		/// <returns></returns>
		public ArrayList GetDatabasesObjects(string ServerName, IDbConnection dataConnection)
		{
			string QueryString = _commands.DatabaseObjects();

			OracleDataAdapter	dataAdapter;
			OracleCommand		dataCommand;
			DataSet			dataSet = new DataSet();
			ArrayList		Result = new ArrayList();
			try 
			{
				dataCommand = new OracleCommand(QueryString, (OracleConnection)dataConnection);	
				dataAdapter = new OracleDataAdapter(dataCommand);
				dataAdapter.Fill(dataSet);
			
				foreach(DataTable dt in dataSet.Tables)
				{
					if(dt.Rows.Count > 0)
					{
						DB db = new DB();
						db.Server = ServerName;
						db.Name = dt.Rows[0].ItemArray[0].ToString();

						foreach(DataRow row in dt.Rows)
						{
							DBObject dbObject = new DBObject();
							dbObject.Name = row.ItemArray[1].ToString();
							dbObject.Type = row.ItemArray[2].ToString();
							db.dbObjects.Add(dbObject);
						}
						Result.Add(db);
					}
				}

				return Result;
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "An error has occured",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				return null;
			}
		}
	
		/// <summary>
		/// Returns all database objects matching [likeChar]. This method is used for IntelliSence
		/// </summary>
		/// <param name="DBName"></param>
		/// <param name="likeChar"></param>
		/// <param name="dataConnection"></param>
		/// <returns></returns>
		public ArrayList GetDatabasesObjects(string DBName, string likeChar, IDbConnection dataConnection)
		{
			likeChar = likeChar.Replace("*","%");
			string QueryString = _commands.DatabaseObject(DBName, likeChar);

			ArrayList		 Result = new ArrayList();
			DataTable dt = ExecuteCommand(QueryString,dataConnection);
			
			foreach(DataRow row in dt.Rows)
			{
				DBObject dbObject = new DBObject();
				dbObject.Name = row.ItemArray[0].ToString();
				dbObject.Type = row.ItemArray[1].ToString();
				Result.Add(dbObject);
			}
			return(Result);
		}

		/// <summary>
		/// Returns all columns for a given table or view
		/// </summary>
		/// <param name="DBName"></param>
		/// <param name="objectName"></param>
		/// <param name="dataConnection"></param>
		/// <returns></returns>
		public ArrayList GetDatabasesObjectProperties(string DBName, string objectName, IDbConnection dataConnection)
		{
			string QueryString = _commands.DatabasesObjectProperties(DBName,objectName);
			DataTable dt;
			ArrayList		 Result = new ArrayList();
			try
			{
				dt = ExecuteCommand(QueryString,dataConnection);
			}
			catch
			{
				return null;
			}

			foreach(DataRow row in dt.Rows)
			{
				DBObjectProperties dbObjectProperties = new DBObjectProperties();
				dbObjectProperties.Name = row.ItemArray[0].ToString();
				Result.Add(dbObjectProperties);
			}
			return(Result);
		}
	
		/// <summary>
		/// Returns all objects using specified object.
		/// </summary>
		/// <param name="DBName"></param>
		/// <param name="likeChar"></param>
		/// <param name="dataConnection"></param>
		/// <returns></returns>
		public ArrayList GetDatabasesReferencedObjects(string DBName, string likeChar, IDbConnection dataConnection)
		{
			likeChar = likeChar.Replace("*","%");
			string QueryString = _commands.DatabasesReferenceObjects(likeChar);
			ArrayList		 Result = new ArrayList();
			DataTable dt = ExecuteCommand(QueryString,dataConnection);
			
			foreach(DataRow row in dt.Rows)
			{
				DBObject dbObject = new DBObject();
				dbObject.Name = row.ItemArray[0].ToString();
				dbObject.Type = row.ItemArray[1].ToString();
				Result.Add(dbObject);
			}
			return(Result);
		}
		/// <summary>
		/// Returns a table definition
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="dataConnection"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public string GetObjectConstructorString(string DBName, string objectName, IDbConnection dataConnection, DBCommon.ScriptType scriptType, DBCommon.ScriptObjectType scriptObjectType)
		{
			string returnString = "";
			string QueryString =_commands.CreateScript(objectName.ToUpper());
			DataTable dt = ExecuteCommand(QueryString,dataConnection);

			foreach(DataRow row in dt.Rows)
			{
				returnString+=row["TEXT"].ToString();
			}
			return returnString;
//			if(scriptType == DBCommon.ScriptType.CREATE || scriptType == DBCommon.ScriptType.ALTER)
//			{
//				objectName = objectName.Replace("DBO.","");
//				
//				if(scriptObjectType==DBCommon.ScriptObjectType.PROCEDURE)
//					QueryString = _commands.CreateScript(objectName);
//				else
//					return GetCreateTableString(objectName,dataConnection,DBName);
//
//				if(DBName.Length>0)
//					QueryString = "USE " + DBName + " " + QueryString; 
//
//				DataTable dt = ExecuteCommand(QueryString,dataConnection);
//				for(int i=0;i<dt.Rows.Count;i++)
//					returnString += dt.Rows[i][0].ToString();  
//			}
//			else if(scriptType == DBCommon.ScriptType.INSERT)
//			{
//				ArrayList a = GetDatabasesObjectProperties(DBName,objectName,dataConnection);
//
//				returnString = "INSERT INTO " + objectName + "(";
//				
//				for(int i=0;i<a.Count;i++)
//				{
//					DBObjectProperties dbo = (DBObjectProperties)a[i];
//					returnString += "[" + dbo.Name + "]";
//					if(i<a.Count-1)
//						returnString += ", ";
//					else
//						returnString += ")";
//
//				}
//				returnString += "\nVALUES (...)";
//			}
			return returnString;
		}
		
		/// <summary>
		/// Gets all documentation headers from database
		/// </summary>
		/// <param name="DBName"></param>
		/// <param name="dataConnection"></param>
		/// <param name="whereConditions"></param>
		/// <returns></returns>
		public string GetXmlDoc(string DBName, IDbConnection dataConnection,string whereConditions)
		{
			string returnString = "";
			dataConnection.ChangeDatabase(DBName);
			string QueryString = "SELECT substring(text,charindex(  '<member',text),charindex(  '</member>',text)-charindex(  '<member',text)+9 ) " + 
				"from 	syscomments c " +
				"join 	sysobjects o on o.id = c.id " + 
				"where text like '%<member%' ";

			if(whereConditions.Length>0)
				QueryString+=" and xtype in("+whereConditions+")";

			QueryString+=" order by xtype, o.name";

			if(DBName.Length>0)
				QueryString = "USE " + DBName + " " + QueryString; 

			DataTable dt = ExecuteCommand(QueryString,dataConnection);

			for(int i=0;i<dt.Rows.Count;i++)			
				returnString += "\n" +dt.Rows[i][0].ToString();  
			
			
			return returnString;
		}
		
		#endregion
		
		class QueryCommands
		{
			XmlDocument _queryDocument=null;
			public QueryCommands(XmlDocument doc)
			{
				_queryDocument=doc;
			}

	
			public string  CreateScript(string objectName)
			{
				return String.Format(FindQueryProvider("GetCreateScript"),objectName);
			}
			public string  DatabaseObjects()
			{
				return FindQueryProvider("GetDatabaseObjects");
			}
			public string  DatabaseObject(string DBName, string likeChar)
			{
				return String.Format( FindQueryProvider("GetDatabaseObject"),likeChar ) ;
			}
			public string DatabasesObjectProperties(string DBName, string objectName)
			{
				return String.Format( FindQueryProvider("GetDatabaseObjectProperties"),objectName ) ;
			}
			public string DatabasesReferenceObjects(string objectName)
			{
				return String.Format(FindQueryProvider("GetReferenceObjects"),objectName ) ;
			}


			private string FindQueryProvider(string provider)
			{
				return _queryDocument.GetElementsByTagName(provider)[0].InnerText;
			}
		}
	}
}
