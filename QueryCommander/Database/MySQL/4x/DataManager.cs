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
using MySql.Data.MySqlClient;
using QueryCommander.Database;

namespace QueryCommander.Database.MySQL._4x
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
		private MySqlDataAdapter _dataAdapter;
		private static ArrayList _sqlInfoMessages = new ArrayList();
		private MySqlCommand _currentCommand;

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
			set{_dataAdapter=(MySqlDataAdapter)value;}
		}	

		#endregion
		#region Public Methods
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
		public DataManager()
		{
			string path="QueryCommander.Database.MySQL._4x.Meta.QueryStrings.xml";
			_queryDocument=DBCommon.ReadEmbeddedResource(path);

			_commands = new QueryCommands(_queryDocument);
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
			
			MySqlDataAdapter dataAdapter;
			MySqlCommand     dataCommand;
			DataSet			 dataSet = new DataSet();
			DataTable        dataTable = new DataTable();
			string tableName = "Query";
			try 
			{
				dataCommand = new MySqlCommand(command, (MySqlConnection)dataConnection);	
				dataAdapter = new MySqlDataAdapter(dataCommand);
				dataAdapter.Fill(dataSet, tableName);
				dataTable = dataSet.Tables[tableName];
			}
			catch(Exception ex)
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
				dataConnection = new MySqlConnection(dataConnection.ConnectionString);
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

				if(currentCommand.ToUpper().IndexOf("SELECT")>=0 ||
					currentCommand.ToUpper().IndexOf("SHOW")>=0)
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
				_currentCommand = new MySqlCommand(command, (MySqlConnection)dataConnection);
				if(IsQuery)
				{
					DataAdapter= new MySqlDataAdapter(_currentCommand);
					MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(_dataAdapter);
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
				
			return "Not yet implemented";
			
		}
		
		/// <summary>
		/// Returns ALL database objects. Used to populate the Object browser
		/// </summary>
		/// <param name="ServerName"></param>
		/// <param name="dataConnection"></param>
		/// <returns></returns>
		public ArrayList GetDatabasesObjects(string ServerName, IDbConnection dataConnection)
		{
			ArrayList Result = new ArrayList();

			// Get databases
			DataTable dsDatabases = ExecuteCommand(_commands.GetDatabases(),dataConnection);
			foreach(DataRow dataBaseRow in dsDatabases.Rows)
			{
				string databaseName = dataBaseRow.ItemArray[0].ToString();
				
				DB db = new DB();
				db.Server = ServerName;
				db.Name = databaseName;

				DataTable dsTables = ExecuteCommand(_commands.GetAllTables(databaseName),dataConnection);
				foreach(DataRow tableRowRow in dsTables.Rows)
				{
					string tableName = tableRowRow.ItemArray[0].ToString();
					
					DBObject dbObject = new DBObject();
					dbObject.Name = tableName;
					dbObject.Type = "U ";
					db.dbObjects.Add(dbObject);
				}

				Result.Add(db);
			}

			return Result;
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
			string QueryString = _commands.GetTables(likeChar,DBName);

			ArrayList		 Result = new ArrayList();
			DataTable dt = ExecuteCommand(QueryString,dataConnection);
			
			foreach(DataRow row in dt.Rows)
			{
				DBObject dbObject = new DBObject();
				dbObject.Name = row.ItemArray[0].ToString();
				dbObject.Type = "U ";
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
			string QueryString = _commands.GetColumnsFromTable(DBName,objectName);
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
			return(Result);return new ArrayList();
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
			return new ArrayList();
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

			returnString = dt.Rows[0][1].ToString();
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
				return String.Format(FindQueryProvider("GetTableConstructor"),objectName);
			}
			public string GetDatabases()
			{
				return FindQueryProvider("GetDatabases");
			}
			public string GetAllTables(string database)
			{
				return String.Format(FindQueryProvider("GetAllTables"),database);
			}

			public string GetTables(string tableLikeName, string database)
			{
				return String.Format(FindQueryProvider("GetTables"),database,tableLikeName);
			}
			public string GetColumnsFromTable(string tableName, string database)
			{
				return String.Format(FindQueryProvider("GetColumnsFromTable"),database,tableName);
			}
			public string GetTableConstructor(string tableName)
			{
				return String.Format(FindQueryProvider("GetTableConstructor"),tableName);
			}
			private string FindQueryProvider(string provider)
			{
				return _queryDocument.GetElementsByTagName(provider)[0].InnerText;
			}
			
		}
	}
}
