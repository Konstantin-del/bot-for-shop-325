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
            CreateMap<OrderDto, OrderProductInputModel>();
            CreateMap<OrderProductInputModel, OrderDto>();
        }
    }
}
