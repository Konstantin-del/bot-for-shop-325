using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    public class UserQueries
    {
        public const string AddUserQuerie = $"INSERT INTO \"UserTest\"(\"Name\") VALUES (@name);";

        public const string GetAllUsersQuery = $"SELECT * FROM \"UserTest\";";

    }
}
