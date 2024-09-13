using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BotForShop.Bot.state
{
    public class ManagerTakeOrderState: AbstractState
    {
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            if (update.CallbackQuery.Data == "take")
            {
                foreach (var item in UserProcessing.Users)
                {
                    if (item.Value.ChatId != context.ChatId)
                    {
                        await botClient.EditMessageTextAsync(
                            item.Value.ChatId, (int)item.Value.LastMessageId, "");
                        //item.Value.LastMessageId = idMessage.MessageId;
                        item.Value.State = new ManagerGetMessagState();
                    }
                }
            }
        }

    }
}
