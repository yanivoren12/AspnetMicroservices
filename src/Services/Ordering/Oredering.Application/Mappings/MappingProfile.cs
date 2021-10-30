using AutoMapper;
using Oredering.Application.Features.Orders.Commands.CheckoutOrder;
using Oredering.Application.Features.Orders.Commands.UpdateOrder;
using Oredering.Application.Features.Orders.Queries.GetOrderList;
using Oredering.Domain.Entities;

namespace Oredering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
