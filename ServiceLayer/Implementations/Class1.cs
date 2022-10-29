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
 
    public class DBService : IDBTypeService
    {
        protected IDB dB;
        public AppKeyObject appKeyObject { get; set; }
        public DBService(AppKeyObject appKeyObject)
        {
            this.appKeyObject = appKeyObject;
            dB = Creator.GetDBTypeObject(appKeyObject.dbType);
        }
        public DataTable GetResults(string query)
        {
            return this.dB.GetDataTable(query, this.appKeyObject);
        }

        public DataTable GetDBObject(string procName )
        {
            return this.dB.GetObjectByName(procName, this.appKeyObject);
        }
    }
}
