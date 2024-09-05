using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotForShop.Core.OutputModels
{
     public class UsersAuthentication
    {
        public long ChatId { get; set; }
        public string UserRole { get; set; }
        public string Name { get; set; }
    }
}
