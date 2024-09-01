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

        public List<OrderDto> GetAllOrderWithProduct()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = OrderQueries.GetAllOrderWithProductQuery;

                connection.Open();
                var result = new List<OrderDto>();

                connection.Query<OrderDto, ProductDto, OrderDto>(query,
                    (order, product) =>
                    {
                        int i = 0;
                        bool isFouded = false;
                        foreach (var u in result)
                        {
                            if (u.Id == order.Id)
                            {
                                isFouded = true;
                                break;
                            }
                            i++;
                        }

                        OrderDto crtnOrder;
                        if (isFouded)
                        {
                            crtnOrder = result[i];
                        }
                        else
                        {
                            crtnOrder = order;
                            crtnOrder.Products = new List<ProductDto>();
                            result.Add(crtnOrder);
                        }

                        if (product is null)
                        {
                            return order;
                        }

                        crtnOrder.Products.Add(product);

                        return order;
                    }, splitOn: "Id");

                return result;
            }
        }
    }
}
