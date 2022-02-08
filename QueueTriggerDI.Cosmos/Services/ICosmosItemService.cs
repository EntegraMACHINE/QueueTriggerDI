using QueueTriggerDI.Cosmos.Entities;
using QueueTriggerDI.Cosmos.DTO;
using System.Threading.Tasks;

namespace QueueTriggerDI.Cosmos.Services
{
    public interface ICosmosItemService<T> where T : class, ICosmosItem
    {
        Task<T> AddItemAync(CreateItemDto<T> createItenModel);
    }
}
