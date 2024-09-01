using BotForShop.Bot.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot
{
    public class Context
    {
        public long ChatId;
        public AbstractState State { get; set; }

        public void HandleMessageContext(Update update)
        {
            State.HandleMessage(this, update);
        }
        public void BotActionContext(Update update, ITelegramBotClient botClient)
        {
            State.BotAction(this, update, botClient);
        }
        
    }
}
