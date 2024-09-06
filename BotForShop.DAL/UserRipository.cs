using Npgsql;
using Dapper;
using BotForShop.DAL.Queries;
using BotForShop.Core.Dtos;
using BotForShop.Core;

namespace BotForShop.DAL
{
    public class UserRepository
    {
        public void AddUser(UserDto user)
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
                connection.Query(query, args);
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
