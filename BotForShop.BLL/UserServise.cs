using AutoMapper;
using BotForShop.Core.Dtos;
using BotForShop.Core.InputModels;
using BotForShop.Core.OutputModels;
using BotForShop.DAL;
using BotForShop.BLL.Mappings;
using System;

namespace BotForShop.BLL
{

    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        public OrderRepository OrderRepository { get; set; }

        public ProductRepository ProductRepository { get; set; }

        private Mapper _mapper;

        public UserService()
        {
            UserRepository = new UserRepository();
            OrderRepository = new OrderRepository();
            ProductRepository = new ProductRepository();

            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new OrderMapperProfile());
                cfg.AddProfile(new ProductMapperProfile());
            });
            _mapper = new Mapper(config);
        }

        public int AddUser(UserInputModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            int userId = UserRepository.AddUser(userDto);
            return userId;
        }

        public List<UserOutputModel> GetAllUsers()
        {

            var users = UserRepository.GetAllUsers();
            var usersOutput = _mapper.Map<List<UserOutputModel>>(users);
            return usersOutput;
        }

        public List<UserRolesOutputModel> GetUserRole()
        {
            var arr = UserRepository.GetUserRoleId();
            var userRoles = _mapper.Map<List<UserRolesOutputModel>>(arr);
            return userRoles;
        }

        public List<UserGetShopAddressOutputModel> GetShopAddresses()
        {
            var shopsAddressesDto = UserRepository.GetShopAddressId();
            var shopAddresses = _mapper.Map<List<UserGetShopAddressOutputModel>>(shopsAddressesDto);
            return shopAddresses;
        }

        public List<UsersAuthentication> GetUsersForAuthentication()
        {
            var UsersChatIdDto = UserRepository.GetUsersForAuthentication();
            var UsersChatId = _mapper.Map<List<UsersAuthentication>>(UsersChatIdDto);
            return UsersChatId;
        }

        //public List<OrderOutputModel> GetAllOrderWith()
        //{
        //    List<OrdersDto> ordersDto = OrderRepository.GetAllOrderWithProduct();
        //    List<OrderOutputModel> users = _mapper.Map<List<OrderOutputModel>>(ordersDto);
        //    return users;
        //}

        public List<ProductOutputModel> GetAllProducts()
        {
            List<ProductDto> productsDto = ProductRepository.GetAllProducts();
            List<ProductOutputModel> products = _mapper.Map<List<ProductOutputModel>>(productsDto);
            return products;
        }

    }

}
