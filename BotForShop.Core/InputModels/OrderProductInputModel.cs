using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.InputModels
{
    public class OrderProductInputModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
