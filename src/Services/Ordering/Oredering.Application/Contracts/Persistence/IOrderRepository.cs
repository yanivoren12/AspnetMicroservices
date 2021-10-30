using Oredering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oredering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrderByUserName(string userName);
    }
}
