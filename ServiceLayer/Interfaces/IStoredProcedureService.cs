using SharedLayer;
using System.Data;

namespace ServiceLayer.Interfaces
{
    public interface IStoredProcedureService
    {
        DataTable GetStoredProcedure(string procName, AppKeyObject connection);

        DataTable ExecuteQuery(string query, AppKeyObject appKeyObject);
    }
}
