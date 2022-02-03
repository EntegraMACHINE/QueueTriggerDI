using System.Data;

namespace QueueTriggerDI.Context.Services
{
    public interface IDbConnectionService
    {
        IDbConnection CreateConnection();
    }
}
