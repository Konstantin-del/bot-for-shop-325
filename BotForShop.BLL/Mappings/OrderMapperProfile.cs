using AutoMapper;
using BotForShop.Core.Dtos;
using BotForShop.Core.InputModels;
using BotForShop.Core.OutputModels;

namespace BotForShop.BLL.Mappings
{
    public class OrderMapperProfile:Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderDto, OrderOutputModel>();
            CreateMap<OrderInputModel, OrderDto>();
        }

    }
}
