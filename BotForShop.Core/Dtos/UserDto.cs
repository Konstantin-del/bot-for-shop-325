using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.Dtos
{
    public class UserDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? OrderCount { get; set; }

        public string? ShopAddress { get; set; }

        //public List<OrderDto> Orders { get; set; }
    }
}
