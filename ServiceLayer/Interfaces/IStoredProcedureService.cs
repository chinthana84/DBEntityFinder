namespace ServiceLayer.Interfaces
{
    public interface IStoredProcedureService
    {
        string GetStoredProcedure(string procName,string connection);
    }
}
