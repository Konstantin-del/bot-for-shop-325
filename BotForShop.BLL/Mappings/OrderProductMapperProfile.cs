using AutoMapper;
using BotForShop.Core.Dtos;
using BotForShop.Core.InputModels;
using BotForShop.Core.OutputModels;

namespace BotForShop.BLL.Mappings
{
    public class OrderProductMapperProfile:Profile
    {
        public OrderProductMapperProfile()
        {
            CreateMap<OrderDto, OrderOutputModel>();
            CreateMap<ProductDto, OrderProductOutputModel>();
            CreateMap<OrderProductInputModel, OrderDto>();
            CreateMap<OrderDto, OrderProductOutputModel>();
            CreateMap<OrderDto, OrderWithProductListOutputModel>();
        }
    }
}
