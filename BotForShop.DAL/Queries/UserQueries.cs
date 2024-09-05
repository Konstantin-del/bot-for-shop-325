﻿using System;
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

        public const string GetAllUsersFullInfoQuery =
            $"select U.\"Id\", U.\"Name\"," +
            $" O.\"Id\", O.\"Adress\"," +
            $" P.\"Id\", P.\"Name\", OP.\"Count\"" +
            $" from \"User\" as U" +
            $" left join \"Order\" as O on O.\"ClientId\"=U.\"Id\"" +
            $" left join \"OrderProducts\" as OP on OP.\"OrderId\"=O.\"Id\"" +
            $" left join \"Product\" as P on P.\"Id\"=OP.\"ProductId\"";

        public const string GetUsersForAuthentication =
            $"select U.\"ChatId\", UR.\"UserRole\"" +
            $" from \"User\" as U" +
            $" left join \"UserRole\" as UR on UR.\"Id\"=U.\"RoleId\"";

        public const string GetUserForAuthentication = $"SELECT U.\"Name\", U.\"ShatId\", UR.\"UserRole\""+
            $" FROM \"Users\" as U "+
            $" JOIN \"UserRoles\" as UR "+
            $" ON U.\"RoleId\"=UR.\"Id\"";

        public const string GetUserRole = $"SELECT * FROM \"UserRoles\"";

        public const string GetUserShop = $"SELECT \"Shops\".\"Id\"," +
            $" \"Shops\".\"ShopAddress\" FROM \"Shops\"";
    }
}
