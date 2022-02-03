using Dapper;
using System.Collections.Generic;

namespace QueueTriggerDI.Context.Repositories
{
    public interface IDapperRepository<T>
    {
        T QuerySingle(string storedProcedureName, DynamicParameters parameters);

        IList<T> Query(string storedProcedureName);

        void Execute(string storedProcedureName, DynamicParameters parameters);
    }
}
