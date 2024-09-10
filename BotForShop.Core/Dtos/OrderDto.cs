using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.Dtos
{
    public class OrderDto
    { 
        public int? Id { get; set; }

        public DateTime? Date { get; set; }

        public int? AdminId { get; set; }

        public string? ProductName { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int? Count { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}

