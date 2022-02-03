using Dapper;
using QueueTriggerDI.Context.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QueueTriggerDI.Context.Repositories
{
    public class DapperRepository<T> : IDapperRepository<T>
    { 
        protected readonly IDbConnectionService dbConnectionService;

        protected DapperRepository(IDbConnectionService dbConnectionService)
        {
            this.dbConnectionService = dbConnectionService;
        }

        public T QuerySingle(string storedProcedureName, DynamicParameters parameters)
        {
            using (IDbConnection connection = dbConnectionService.CreateConnection())
            {
                return connection.QuerySingle<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IList<T> Query(string storedProcedureName)
        {
            using (IDbConnection connection = dbConnectionService.CreateConnection())
            {
                return connection.Query<T>(storedProcedureName, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Execute(string storedProcedureName, DynamicParameters parameters)
        {
            using (IDbConnection connection = dbConnectionService.CreateConnection())
            {
                connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
