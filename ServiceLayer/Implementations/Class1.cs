using DataAccessLayer;
using ServiceLayer.Interfaces;
using SharedLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations
{
    class Class1
    {
    }

    public class StoredProcedureService : IStoredProcedureService
    {
        protected IStoredProcedure _idb;
        protected IDB _db;
        public StoredProcedureService(string db)
        {
            if (db.ToUpper() == dbType.SqlServer.ToString().ToUpper())
            {
                this._idb = new SQLStoredProcedure();
                this._db = new SQLServer();
            }
            else
            {
                throw new ApplicationException("app key db type not found");
            }
          
        }

        public DataTable ExecuteQuery(string query, AppKeyObject appKeyObject)
        {
            return this._db.GetDataTable(query, appKeyObject);
        }

        public DataTable GetStoredProcedure(string procName, AppKeyObject connection)
        {

            return this._idb.GetStoredProcedure(procName, connection);
        }
    }
}
