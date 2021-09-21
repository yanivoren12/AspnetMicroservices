using AutoMapper;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;

namespace Discount.GRPC.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
