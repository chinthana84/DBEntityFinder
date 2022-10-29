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
            DataTable dt = GetDataTable(query, con);
            if (dt.Rows.Count > 0)
                return dt;
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
