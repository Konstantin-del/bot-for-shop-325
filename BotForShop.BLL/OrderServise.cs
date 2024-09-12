using AutoMapper;
using BotForShop.BLL.Mappings;
using BotForShop.Core.Dtos;
using BotForShop.Core.InputModels;
using BotForShop.Core.OutputModels;
using BotForShop.DAL;
using System;


namespace BotForShop.BLL
{
    public class OrderServise
    {

        public OrderRepository OrderRepository { get; set; }

        public ProductRepository ProductRepository { get; set; }

        private Mapper _mapper;

        public OrderServise()
        {
            OrderRepository = new OrderRepository();

            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new OrderMapperProfile());
                cfg.AddProfile(new OrderProductMapperProfile());
            });
            _mapper = new Mapper(config);
        }

        public OrderWithProductListOutputModel GetOrderWithProduct(int id)
        {
            var orderDto = OrderRepository.GetOrderWithProduct(id);
            var order = _mapper.Map<OrderWithProductListOutputModel>(orderDto);
            
            return order;
        }
    }
}
