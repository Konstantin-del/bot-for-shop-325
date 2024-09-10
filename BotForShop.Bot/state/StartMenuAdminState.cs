using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace BotForShop.Bot.state
{
    public class StartMenuAdminState: AbstractState
    {
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            context.keyboard = keyboard;
            await botClient.SendTextMessageAsync(
                context.ChatId, "select action", replyMarkup: keyboard);

        }

        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Add order", "1")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Add user", "2")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("again", "3")
                },
            }
        );
    }
}
