using Npgsql;
using Dapper;
using BotForShop.DAL.Queries;
using BotForShop.Core.Dtos;
using BotForShop.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL
{
    public class UserRepository
    {
        public void AddUser(string name)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.AddUserQuerie;
                var args = new { name = name };

                connection.Open();
                connection.Query(query, args);
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

                foreach (var item in arr)
                {
                    Console.WriteLine(item.Name);
                }
                return arr;
            }
        }
    }
}
