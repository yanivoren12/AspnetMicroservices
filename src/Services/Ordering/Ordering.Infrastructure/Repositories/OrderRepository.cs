using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Presistence;
using Oredering.Application.Contracts.Persistence;
using Oredering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return await context.Orders
                                .Where(x => x.UserName == userName)
                                .ToListAsync();
        }
    }
}
