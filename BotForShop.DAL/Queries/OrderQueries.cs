using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    public class OrderQueries
    {
        public const string GetOrderByIdQuery = 
            $" SELECT \"Orders\".\"Id\", \"Products\".\"ProductName\", \"OrderProducts\".\"Count\"" +
            $" FROM \"Orders\" JOIN \"OrderProducts\" ON \"Orders\".\"Id\" = \"OrderProducts\".\"OrderId\" " +
            $" JOIN \"Products\"  ON \"Products\".\"Id\" = \"OrderProducts\".\"ProductId\" " +
            $" WHERE \"Orders\".\"Id\" = (@id);";

        public const string GetAllOrderWithProductQuery =
            $" SELECT \"Orders\".\"Id\", \"Products\".\"Id\", \"Products\".\"ProductName\"" +
            $" FROM \"Orders\"" +
            $" JOIN \"OrderProducts\"" +
            $" ON \"Orders\".\"Id\" = \"OrderProducts\".\"OrderId\"" +
            $" JOIN \"Products\"" +
            $" ON \"Products\".\"Id\" = \"OrderProducts\".\"ProductId\";";
    }
}
