using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    internal class UserQueries
    {
        public const string AddUserQuerie =
        $"INSERT INTO \"User\"(\"Name\", \"Phone\", \"Mail\", \"RoleId\", \"ShopId\") VALUES ((@name), (@phone), (@mail), (@role_id), (@shope_id));";

        public const string AddOneUserQuerie =
        $"INSERT INTO \"UserTest\"(\"Name\") VALUES (@name);";

        //public const string UpdateUserQuery = $"update \"User\" set\"Name\"=@name where \"Id\"=@id";

        public const string GetAllUsersQuery = $"SELECT * FROM \"UserTest\";";

        //$"SELECT \"User\".\"Name\", \"Shop\".\"ShopAddress\" FROM public.\"User\"JOIN \"Shop\" ON \"Shop\".\"Id\" = \"User\".\"ShopId\";";


        //public const string GetUserByIdQuery = $"SELECT \"Id\", \"Name\" FROM public.\"User\" where \"Id\"=@id;";
    }
}
