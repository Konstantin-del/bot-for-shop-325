using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotForShop.Core;
using BotForShop.Core.OutputModels;

namespace BotForShop.Bot
{
    public class OrderContext
    {
        public long ChatIdEmployee { get; set; }

        public int LastMessag { get; set; }

        public int OrderId { get; set; }

        public string Order { get; set; }

        public  List<ModelForMailing> ForSend { get; set; }

    }

    
}
