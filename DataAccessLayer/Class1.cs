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
        string GetString(string s);
        DataTable GetDataTable(string query,string con);
         
    }

    public class SQLServer : IDB
    {
        private string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public string GetString(string s)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string query,string con)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlDataAdapter da = new SqlDataAdapter(query,sqlConnection);
            da.Fill(dt);
            return dt;
        }
    }

    public class SQLStoredProcedure : SQLServer, IStoredProcedure
    {
        protected IDB _idb;
        string query = "";

        public string GetStoredProcedure(string procName,string con)
        {
            query= $@"select text
from syscomments 
where  object_name(id) ='{procName}'
order by object_name(id) ";
           DataTable dt= GetDataTable(query,con);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString() ;
            else
                return "";
        }
    }

    public interface IStoredProcedure
    {
        string GetStoredProcedure(string procName,string con);
    }

}
