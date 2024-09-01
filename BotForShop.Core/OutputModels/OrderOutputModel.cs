using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.OutputModels
{
    public class OrderOutputModel
    {
        public int Id { get; set; }

        public List<ProductOutputModel> Products;
    }
}
