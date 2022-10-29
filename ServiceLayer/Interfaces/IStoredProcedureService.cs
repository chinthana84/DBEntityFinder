using SharedLayer;
using System.Data;

namespace ServiceLayer.Interfaces
{
    public interface IDBTypeService
    {
        DataTable GetDBObject(string procName);

        DataTable GetResults(string query);
    }
}
