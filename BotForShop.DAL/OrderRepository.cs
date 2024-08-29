using Npgsql;
using Dapper;
using BotForShop.DAL.Queries;
using BotForShop.Core.Dtos;
using BotForShop.Core;
using System.Xml.Linq;


namespace BotForShop.DAL
{
    public class OrderRepository
    {
        public List<OrderDto> GetOrderById(int id)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                var query = OrderQueries.GetOrderByIdQuery;
                var args = new { id = id };

                connection.Open();
                var items = connection.Query<OrderDto>(query, args).ToList();

                return items;
            }
        }
    }
}
