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
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace QueryCommander.Database
{
	#region Classes

	public abstract class DBCommon
	{
		public XmlDocument xmlDocument=null;
		public SqlDataAdapter DataAdapter;
		public static ArrayList SqlInfoMessages = new ArrayList();
		public enum ScriptType{CREATE,ALTER,UPDATE,DELETE,INSERT}
		public enum ScriptObjectType{TABLE,VIEW,PROCEDURE}
		public static XmlDocument ReadEmbeddedResource(string resource)
		{
			System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
			System.IO.Stream str = a.GetManifestResourceStream(resource);
			System.IO.StreamReader reader = new StreamReader(str);
			
			string content = reader.ReadToEnd();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(content);
			return doc;
		}
	}

	//		public class DBFactory
	//		{
	//			public delegate XmlDocument DatabaseDelegate(SqlConnection  dataConnection,string databaseName);
	//			public static IAsyncResult Init (SqlConnection  dataConnection,string databaseName, Database db)
	//			{
	//				DatabaseDelegate dlgt = new DatabaseDelegate( db.GetAllObjects);
	//				IAsyncResult ar = dlgt.BeginInvoke(dataConnection,
	//					databaseName,
	//					new AsyncCallback(CallbackMethod),
	//					dlgt );
	//				return ar;
	//
	//			}
	//			public static void CallbackMethod(IAsyncResult ar) 
	//			{
	//				try
	//				{
	//					// Retrieve the delegate.
	//					DatabaseDelegate dlgt = (DatabaseDelegate) ar.AsyncState;
	//
	//					// Call EndInvoke to retrieve the results.
	//					//				
	//					dlgt.EndInvoke(ar);
	//				}
	//				catch
	//				{return;}
	//
	//				//Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".", threadId, ret);
	//			}
	//
	//		}
	public class DB
	{
		public string Name;
		public string Server;
		public ArrayList dbObjects = new ArrayList();
	}
	public class DBObject
	{
		public string Name;
		public string Type;
		public DBObjectAttributeCollection Attributes;
	}
	public class DBObjectAttribute
	{
		public string Name;
		public string Type;
		public int Length;
		public int Precesion;
			
	}
	public class DBObjectAttributeCollection : ArrayList
	{
		public int Add(DBObjectAttribute attr)
		{
			return base.Add(attr);
		}
	}	
	public class DBObjectCollection : ArrayList
	{
		public int Add(DBObject obj)
		{
			return base.Add(obj);
		}
		public DBObjectCollection FindByType(string type)
		{
			DBObjectCollection oc = new DBObjectCollection();
			foreach(DBObject o in this)
			{
				if(o.Type==type)
					oc.Add(o);
			}
			return oc;
		}
	}

	public class DBObjectProperties
	{
		public string Name;
	}
		
	class DBCommands
	{
		#region QueryStrings
		const string QueryString_GetDatabaseObjects = @"declare @SQL varchar(1000) 
															declare @DBName varchar(255) 
															declare SYSDB cursor for 
															select name 
															from master.dbo.sysdatabases 
															where has_dbaccess(name) = 1 
															order by name 

															open SYSDB 
															fetch next from SYSDB into  @DBName 
															while @@fetch_status = 0 
															begin 
															set @SQL = 'use [' + @DBName +']  select '''
																+ @DBName + ''', O.name, xtype from [' + @DBName + '].dbo.sysobjects O
																where id >1000 
																and xtype in (''U'',''P'',''V'',''FN'') 
																and name not like (''dt_%'') 
																order by xtype , O.name' 
															exec( @SQL ) 
															fetch next from SYSDB into  @DBName 
															end 
															close SYSDB 
															Deallocate SYSDB ";
			
		const string QueryString_GetDatabaseObject= "select name, xtype from [{0}].dbo.sysobjects where name like ('{1}%') and xtype in ('U','P','FN','TF','V') order by name";
		const string QueryString_DatabaseObjectProperties =  "select C.* from {0}.dbo.sysobjects O join {0}.dbo.syscolumns C on C.id = O.id where O.name = '{1}' and (O.xtype ='U' or O.xtype ='V') order by C.name";
		const string QueryString_CreateScript = "select text from sysobjects o join  syscomments c on c.id = o.id where o.name = '{0}' order by o.name";
		//			const string QueryString_CreateScript = @"select " +
		//													"	case xtype " +
		//													"		when 'P' then 'DROP PROCEDURE {0}"+"\n"+"GO"+"\n"+"' + text  " +
		//													"		when 'FN' then 'DROP FUNCTION {0}"+"\n"+"GO"+"\n"+"' + text  " +
		//													"		when 'TF' then 'DROP FUNCTION {0}"+"\n"+"GO"+"\n"+"' + text  " +
		//													"		when 'V' then 'DROP VIEW {0}"+"\n"+"GO"+"\n"+"' + text  " +
		//													"		else text  " +
		//													"	end  " +
		//													"from sysobjects o  " +
		//													"join  syscomments c on c.id = o.id  " +
		//													"where o.name = '{0}' order by o.name " ;

		const string QueryString_GetJoiningOptions = @"select 
															o.name, fc.name,ro.name, c.name, fk.*
															from sysobjects o 
															join sysforeignkeys fk on fk.fkeyid = o.id
															join sysobjects ro on ro.id = fk.rkeyid
															join syscolumns c on c.id = ro.id and c.colid = fk.rkey																				  join syscolumns fc on fc.id = o.id and fc.colid = fk.fkey
															where o.name = '{0}'";
		const string QueryString_AllObjects = @"SELECT 1 as Tag, NULL as Parent,
							o.name as [DBObject!1!Name],
							o.xtype as [DBObject!1!Type],
							null as [DBObjectAttribute!2!Name] ,
							null as [DBObjectAttribute!2!Type], 
							null as [DBObjectAttribute!2!Length] ,
							null as [DBObjectAttribute!2!Precision]
						from 	sysobjects o 
						where 	o.xType != 'S'
						union all
						SELECT 2,1,
							o.name,
							o.xtype,
							c.name,
							t.name,
							c.length,
							c.prec
						from 	sysobjects o 
						join	sysColumns c on c.id = o.id
						join 	systypes t on t.xtype = c.xtype
						where 	o.xType != 'S'
						and 	len(c.Name)>0
						ORDER BY [DBObject!1!Name],[DBObjectAttribute!2!Name]
						FOR XML EXPLICIT";
		const string QueryString_ReferenceObjects = @"SELECT distinct o.name, o.xtype
						from syscomments c
						join sysobjects o on o.id = c.id
						where o.name != '{0}'
						and	c.text like'%{0} %' or c.text like'%{0}(%'";



		#endregion

		public string  CreateScript(string objectName)
		{
			return String.Format(QueryString_CreateScript,objectName);
		}
		public string  AllDatabaseObjects_()
		{
			return QueryString_AllObjects;
		}
		public string  DatabaseObjects()
		{
			return QueryString_GetDatabaseObjects;
		}
		public string  DatabaseObject(string DBName, string likeChar)
		{
			return String.Format( QueryString_GetDatabaseObject,DBName,likeChar ) ;
		}
		public string DatabasesObjectProperties(string DBName, string objectName)
		{
			return String.Format( QueryString_DatabaseObjectProperties,DBName,objectName ) ;
		}
		public string DatabasesReferenceObjects(string objectName)
		{
			return String.Format( QueryString_ReferenceObjects,objectName ) ;
		}
		public string GetJoiningOptions_(string objectName)
		{
			return String.Format( QueryString_GetJoiningOptions,objectName ) ;
		}
	}

	#endregion	
}
