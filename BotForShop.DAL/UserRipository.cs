using Npgsql;
using Dapper;
using BotForShop.DAL.Queries;
using BotForShop.Core.Dtos;
using BotForShop.Core;
using System.Collections;

namespace BotForShop.DAL
{
    public class UserRepository
    {
        public int AddUser(UserDto user)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.AddUserQuerie;
                var args = new { 
                    name = user.UserName,
                    phone = user.Phone, 
                    roleId = user.RoleId,
                    shopId = user.ShopId,
                    chatId = user.ChatId
                };

                connection.Open();
                var userId = connection.Query <UserDto> (query, args).First();
                int id = Convert.ToInt32(userId.Id);
                Console.WriteLine(id);
                return id;
            }
        }

        public List<UserDto> GetUserRoleId()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetUserRoleQuery;
                
                connection.Open();
                var arr = connection.Query<UserDto>(query).ToList();
                return arr;
            }
        }

        public List<UserDto> GetShopAddressId()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetUserShopQuery;

                connection.Open();
                var arr = connection.Query<UserDto>(query).ToList();
                return arr;
            }
        }

        public List<UserDto> GetUsersForAuthentication()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetUserForAuthenticationQuery;

                connection.Open();
                var arr = connection.Query<UserDto>(query).ToList();
                return arr;
            }
        }

        public List<UserDto> GetAllUsers()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.GetAllUsersQuery;

                connection.Open();
                var arr = connection.Query<UserDto>(query).ToList();
                return arr;
            }
        }

        
    }
}
