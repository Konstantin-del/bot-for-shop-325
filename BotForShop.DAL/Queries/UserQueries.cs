using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    public class UserQueries
    {
        public const string AddUserQuerie = 
            $" INSERT INTO \"Users\""+
            $" (\"UserName\", \"Phone\", \"RoleId\", \"ShopId\", \"ChatId\")"+ 
            $" VALUES (@name, @phone, @roleId, @shopId, @chatId)";

        public const string GetAllUsersQuery = $"SELECT * FROM \"UserTest\";";

        public const string GetAllUsersFullInfoQuery =
            $"select U.\"Id\", U.\"Name\"," +
            $" O.\"Id\", O.\"Adress\"," +
            $" P.\"Id\", P.\"Name\", OP.\"Count\"" +
            $" from \"User\" as U" +
            $" left join \"Order\" as O on O.\"ClientId\"=U.\"Id\"" +
            $" left join \"OrderProducts\" as OP on OP.\"OrderId\"=O.\"Id\"" +
            $" left join \"Product\" as P on P.\"Id\"=OP.\"ProductId\"";

        public const string GetUserForAuthenticationQuery =
            $"SELECT u.\"UserName\", u.\"ChatId\", u.\"RoleId\"" +
            $" FROM \"Users\" as u "; 

        public const string GetUserRoleQuery = $"SELECT * FROM \"UserRoles\"";

        public const string GetUserShopQuery = $"SELECT \"Shops\".\"Id\"," +
            $" \"Shops\".\"ShopAddress\" FROM \"Shops\"";
    }
}
