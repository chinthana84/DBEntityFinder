using DataAccessLayer;
using ServiceLayer.Interfaces;
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
        string query = "";
        public StoredProcedureService()
        {
            this._idb = new SQLStoredProcedure();
        }
        public string GetStoredProcedure(string procName,string connection)
        {

            return this._idb.GetStoredProcedure(procName, connection);
        }
    }
}
