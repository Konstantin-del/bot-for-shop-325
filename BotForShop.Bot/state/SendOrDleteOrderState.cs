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
using BotForShop.Core.OutputModels;
using BotForShop.Core;

namespace BotForShop.Bot.state
{
    public class SendOrDleteOrderState: AbstractState
    {
        
        public static string CurrentProducts { get; set; } = "";

        public static int CurrentOrderId { get; set; }

        public OrderContext OrderContext { get; set; } 

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
                    CurrentOrderId = res.Id;
                    foreach (var item in res.Products)
                    {
                        CurrentProducts += $"{item.ProductName.TrimEnd()}, count: {item.Count} \n";
                    }
                    result = $"order id: {res.Id}\n{CurrentProducts}";

                    OrderContext = new OrderContext();
                    OrderContext.Order = result;                  
                    OrderContext.OrderId = res.Id;

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
                    ModelForMailing modelForMailing; 
                    try 
                    {
                        foreach (var item in UserProcessing.Users)
                        {
                            if (item.Value.RoleId == 2)
                            {
                                var idMessage = await botClient.SendTextMessageAsync(
                                    item.Value.ChatId, CurrentProducts, replyMarkup: keyboardTake);
                                 
                                item.Value.LastMessageId = idMessage.MessageId;
                                item.Value.State = new ManagerTakeOrderState();

                                modelForMailing = new ModelForMailing();
                                modelForMailing.MessageId = idMessage.MessageId;
                                modelForMailing.ChatId = item.Value.ChatId;
                                Console.WriteLine(modelForMailing.ChatId);
                                OrderContext.ForSend.Add(modelForMailing);
                            }
                        }
                        await botClient.EditMessageTextAsync(
                            context.ChatId, update.CallbackQuery.Message.MessageId,
                            "the order has been \n send to the menedger");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);

                        OrderProceccing.Orders.Add(OrderContext);
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
