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
            //string text =
            //     "enter namber \n" +
            //     "add user: 1 \n" +
            //     "get one user: 2 \n";
            //await botClient.SendTextMessageAsync(update.Message.Chat, text);
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
                });

            context.keyboard = keyboard;

            await botClient.SendTextMessageAsync(update.Message.Chat, "select action",
                replyMarkup: keyboard
                );

        }
    }
}
