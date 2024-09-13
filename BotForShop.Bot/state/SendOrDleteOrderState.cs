using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using BotForShop.BLL;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace BotForShop.Bot.state
{
    public class SendOrDleteOrderState: AbstractState
    {
        
        public static string CurrentProducts = "";

        string result = "";
        bool isFirst = true;

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
           
            if (isFirst)
            {
                try
                {
                    var orderServis = new OrderServise();
                    var res = orderServis.GetOrderWithProduct(AddOrderState.OrderId);

                    foreach (var item in res.Products)
                    {
                        CurrentProducts += $"{item.ProductName.TrimEnd()}, count: {item.Count} \n";
                    }
                    result = $"order id: {res.Id}\n{CurrentProducts}";

                    await botClient.EditMessageTextAsync(
                    context.ChatId, update.CallbackQuery.Message.MessageId,
                    result, replyMarkup: keyboardSend);
                }
                catch
                {
                    await botClient.EditMessageTextAsync(
                        context.ChatId, update.CallbackQuery.Message.MessageId,
                        "error adding order from db");
                    UserProcessing.UpdateAdninToStartAdminState();
                    UserProcessing.UserCurrent.BotActionContext(update, botClient);
                }
                isFirst = false;
            }
            else
            {
                if(update.CallbackQuery.Data == "send")
                {
                    try 
                    {
                        foreach (var item in UserProcessing.Users)
                        {
                            if (item.Value.RoleId == 2)
                            {
                                var idMessage = await botClient.SendTextMessageAsync(
                                    item.Value.ChatId, CurrentProducts, replyMarkup: keyboardTake);

                                item.Value.LastMessageId = idMessage.MessageId;
                                Console.WriteLine(idMessage.MessageId);
                                item.Value.State = new ManagerGetMessagState();
                            }
                        }
                        await botClient.EditMessageTextAsync(
                            context.ChatId, update.CallbackQuery.Message.MessageId,
                            "the order has been \n send to the menedger");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);
                    } 
                    catch
                    {
                        await botClient.EditMessageTextAsync(
                            context.ChatId, update.CallbackQuery.Message.MessageId, "error send order");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);
                    }
                }

                //попробовать снова или выйти
                //await botClient.SendTextMessageAsync(context.ChatId, "");
            }

            
        }


        InlineKeyboardMarkup keyboardSend = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("send request", "send")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("delete order", "del")
                },
            }
        );

        InlineKeyboardMarkup keyboardTake = new InlineKeyboardMarkup(
            new[]
            {
                InlineKeyboardButton.WithCallbackData("I'll take it", "take")
            }
        );
    }
}
