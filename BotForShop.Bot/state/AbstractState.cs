﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot.state
{
    public abstract class AbstractState
    { 
        public abstract void BotAction(Context context, Update update, ITelegramBotClient botClient);
    }
}
