﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.OutputModels
{
    public class OrderWithProductListOutputModel
    {
        public int Id { get; set; }

        public List<OrderProductOutputModel> Products { get; set; }
    }
}
