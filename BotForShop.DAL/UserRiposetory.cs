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
        public void AddUser(string name, int phone, string mail, int role_id, int shope_id)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.AddUserQuerie;
                var args = new { name = name, phone = phone, mail = mail, role_id = role_id, shope_id = shope_id };

                connection.Open();
                connection.Query(query, args);
            }
        }

        public void AddUser(string name)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = UserQueries.AddOneUserQuerie;
                var args = new { name = name };

                connection.Open();
                connection.Query(query, args);
            }
        }

        //public void UpdateUser(UserDto user)
        //{
        //    string conectionString = Options.ConectionString;
        //    using (var connection = new NpgsqlConnection(conectionString))
        //    {
        //        string query = UserQueries.UpdateUserQuery;
        //        var args = new { name = user.Name, id = user.Id };

        //        connection.Open();
        //        connection.Query(query, args);
        //    }
        //}

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
