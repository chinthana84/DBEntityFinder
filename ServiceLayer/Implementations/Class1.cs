using DataAccessLayer;
using ServiceLayer.Interfaces;
using SharedLayer;
using System;
using System.Collections.Generic;
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
        public StoredProcedureService(string db)
        {
            if (db.ToUpper() == dbType.SqlServer.ToString().ToUpper())
            {
                this._idb = new SQLStoredProcedure();
            }
            else
            {
                throw new ApplicationException("app key db type not found");
            }
          
        }
        public string GetStoredProcedure(string procName, AppKeyObject connection)
        {

            return this._idb.GetStoredProcedure(procName, connection);
        }
    }
}
