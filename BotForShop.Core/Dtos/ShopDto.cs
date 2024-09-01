using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.Dtos
{
    internal class ShopDto
    {
        public string Address { get; set; }
        public List<UserDto> Users { get; set; }

    }
}
