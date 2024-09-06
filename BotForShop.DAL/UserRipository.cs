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
                    name = user.Name,
                    phone = user.Phone, 
                    roleId = user.UserRole,
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
                string query = UserQueries.GetUserRole;
                
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
                string query = UserQueries.GetUserShop;

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
                string query = UserQueries.GetUserForAuthentication;

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
