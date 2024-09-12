using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.OutputModels
{
    public class OrderProductOutputModel
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }

        public int Count { get; set; }
    }
}
