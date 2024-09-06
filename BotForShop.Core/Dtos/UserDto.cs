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

        public long? ChatId { get; set; }

        public string? UserRole { get; set; }

        public string? Phone { get; set; }

        public string? Name { get; set; }

        public int? ShopId { get; set; }

        public string? ShopAddress { get; set; }

        //public List<OrderDto> Orders { get; set; }
    }
}
