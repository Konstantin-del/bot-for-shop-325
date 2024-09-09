
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    public class ProductQueries
    {
        public const string GetAllActivProductsQuery =
            $" SELECT p.\"Id\", p.\"ProductName\"" +
            $" FROM \"Products\" as p" +
            $" WHERE p.\"IsActive\" = true";
    }
}
