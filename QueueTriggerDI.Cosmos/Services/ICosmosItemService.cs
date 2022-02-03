using QueueTriggerDI.Cosmos.Entities;
using QueueTriggerDI.Cosmos.Models;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Services
{
    public interface ICosmosItemService<T> where T : class, ICosmosItem
    {
        Task<T> AddItemAync(CreateItemModel<T> createItenModel);
    }
}
