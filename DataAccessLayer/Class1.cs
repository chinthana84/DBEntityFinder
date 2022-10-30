using SharedLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Class1
    {
    }

    public interface IDB
    {
        DataTable GetObjectByName(string procName, AppKeyObject con);
        DataTable GetDataTable(string query, AppKeyObject con);
    }

    public class SQLServerDB : IDB
    { 
        string query="";
        public DataTable GetObjectByName(string procName, AppKeyObject con)
        {
            query = $@"SELECT s.text, NAME AS ObjectName
	                        ,schema_name(o.schema_id) AS SchemaName
	                        ,type
	                        ,o.type_desc
	                        
                        FROM sys.objects o
                        inner join  syscomments s on o.object_id=s.id
                        WHERE o.is_ms_shipped = 0
                         AND o.NAME ='{procName}'
                        ORDER BY o.NAME";
            query = $@"
                    SELECT db_name() AS the__database
				                    , OBJECT_SCHEMA_NAME(O.object_id) AS the__schema
				                    , O.name AS object__name 
				                    , O.type_desc AS object__type
				                    , O.is_ms_shipped
				                    , M.definition AS   b
				                    ,O.object_id
		                    FROM sys.objects O WITH(NOLOCK)
			                    LEFT JOIN sys.sql_modules M ON O.object_id = M.object_id
		                    WHERE O.name='{procName}' ";

            DataTable dt = GetDataTable(query, con);

            if (dt.Rows.Count > 1)
            {
                return dt;
            }
            else if (dt.Rows.Count == 1)
            {
        
                DataTable dt2 = GetDataTable($@"		declare @s as nvarchar(max);
		                                set @s=	(	SELECT 1 AS [Tag], 0 AS [Parent], NCHAR(13) + NCHAR(10) +
                                       OBJECT_DEFINITION({dt.Rows[0]["object_id"]}) AS [Code!1!!CDATA]
                                FOR XML EXPLICIT)
                                select replace(replace(@s, '<Code><![CDATA[',''),']]></Code>','')", con);

          

                //.Replace("<Code><![CDATA[", "").Replace("]]></Code>", "")

                return dt2;
            }
            else
                return new DataTable();
        }

        public DataTable GetDataTable(string query, AppKeyObject con)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(con.Value);
            SqlDataAdapter da = new SqlDataAdapter(query,sqlConnection);
            da.Fill(dt);
            return dt;
        }
    }

    public class Creator
    {
        public static IDB GetDBTypeObject(string key)
        {
            if(key.ToUpper().Trim()== dbType.SqlServer.ToString().ToUpper())
            {
                return new SQLServerDB();
            }
            else
            {
                throw new Exception("Incorrect Database key in config");
            }
        }
    }
}
