
using Npgsql;
using Dapper;
using BotForShop.Core.Dtos;
using BotForShop.Core;
using BotForShop.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.DAL
{
    public class ProductRepository
    {
        public List<ProductDto> GetAllProducts()
        {
            string conectionString = Options.ConectionString;
            using (var connection = new NpgsqlConnection(conectionString))
            {
                var query = ProductQueries.GetAllActivProductsQuery;

                connection.Open();
                var products = connection.Query<ProductDto>(query).ToList();

                return products;
            }
        }
    }
}
