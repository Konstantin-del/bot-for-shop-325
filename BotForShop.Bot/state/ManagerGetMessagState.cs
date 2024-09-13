using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotForShop.Bot.state
{
    public class ManagerGetMessagState:AbstractState
    {
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            if (update.CallbackQuery.Data == "take")
                Console.WriteLine("here");
            {
                foreach (var item in UserProcessing.Users)
                {
                    if (item.Value.RoleId == 2)
                    {
                        if (item.Value.ChatId != context.ChatId)
                        {
                            await botClient.EditMessageTextAsync(
                                item.Value.ChatId, item.Value.LastMessageId, "the order was taken");
                        }
                        else
                        {
                            await botClient.EditMessageTextAsync(
                                item.Value.ChatId, item.Value.LastMessageId,
                                SendOrDleteOrderState.CurrentProducts, replyMarkup: keyboardNext);
                            item.Value.State = new ManagerTakeOrderState();
                        }
                    }
                    
                }
            }
        }

        InlineKeyboardMarkup keyboardNext = new InlineKeyboardMarkup(
            new[]
            {
                InlineKeyboardButton.WithCallbackData("ready to send", "cheng")
            }
        );
    }
}
