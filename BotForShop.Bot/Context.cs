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
        public AbstractState state { get; set; }

        public void HandleMessage(Update update)
        {
            state.HandleMessage(this, update);
        }
        public void BotAction(Update update, ITelegramBotClient botClient)
        {
            state.BotAction(this, update, botClient);
        }
        
    }
}
