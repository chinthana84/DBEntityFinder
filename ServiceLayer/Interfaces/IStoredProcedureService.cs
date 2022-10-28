using SharedLayer;

namespace ServiceLayer.Interfaces
{
    public interface IStoredProcedureService
    {
        string GetStoredProcedure(string procName, AppKeyObject connection);
    }
}
