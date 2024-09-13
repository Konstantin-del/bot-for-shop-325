using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL.Queries
{
    public class OrderQueries
    {
        public const string AddOrder =
            $" INSERT INTO \"Orders\" (\"CreatedDate\", \"StatusId\",\"AdminId\")" +
            $" VALUES (@date, @statusId, @adminId) RETURNING \"Id\"";

        public const string AddOrderProduct =
            $" INSERT INTO \"OrderProducts\" (\"OrderId\",\"ProductId\", \"Count\")" +
            $" VALUES (@orderId, @productId, @count)";

        public const string GetOrderByIdQuery = 
            $" SELECT \"Orders\".\"Id\", \"Products\".\"ProductName\", \"OrderProducts\".\"Count\"" +
            $" FROM \"Orders\" JOIN \"OrderProducts\" ON \"Orders\".\"Id\" = \"OrderProducts\".\"OrderId\" " +
            $" JOIN \"Products\"  ON \"Products\".\"Id\" = \"OrderProducts\".\"ProductId\" " +
            $" WHERE \"Orders\".\"Id\" = (@id);";

        public const string GetOrderWithProductQuery =
            $" SELECT o.\"Id\", p.\"Id\", p.\"ProductName\", op.\"Count\"" +
            $" FROM \"Orders\" as o" +
            $" JOIN \"OrderProducts\" as op" +
            $" ON o.\"Id\" = op.\"OrderId\"" +
            $" JOIN \"Products\" as p" +
            $" ON p.\"Id\" = op.\"ProductId\"" +
            $" WHERE o.\"Id\" = (@id);";

        public const string UpdateStatusOrder = 
            $"UPDATE \"Orders\" SET \"StatusId\" = (@statusId) WHERE \"Id\"=(@orderId) ";

        public const string AddShopIdInOrder =
            $"UPDATE \"Orders\" SET \"ShopId\" = (@shopId) WHERE \"Id\"=(@orderId) ";


    }
}
