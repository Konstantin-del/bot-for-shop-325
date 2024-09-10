﻿using Npgsql;
using Dapper;
using BotForShop.DAL.Queries;
using BotForShop.Core.Dtos;
using BotForShop.Core;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BotForShop.DAL
{
    public class OrderRepository
    {
        public int AddOrder(OrderDto order)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                var query = OrderQueries.AddOrder;
                var args = new {
                    date = order.Date,
                    statusId = 1,
                    adminId = order.AdminId 
                };

                connection.Open();
                var orderId = connection.Query<OrderDto>(query, args).First();
                int id = Convert.ToInt32(orderId.Id);
                return id;
            }
        }

        public void AddOrderProduct(OrderDto orderProduct)
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                var query = OrderQueries.AddOrderProduct;
                var args = new
                {
                    orderId = orderProduct.OrderId,
                    productId = orderProduct.ProductId,
                    count = orderProduct.Count
                };

                connection.Open();
                connection.Query<OrderDto>(query, args);
            }
        }
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
