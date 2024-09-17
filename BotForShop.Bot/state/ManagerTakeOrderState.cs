using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using BotForShop.DAL;
using BotForShop.Bot.state;
using BotForShop.Bot;

namespace BotForShop.Bot.state
{
    public class ManagerTakeOrderState:AbstractState
    {
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            var orderContext = new OrderContext();
            int idMessage = update.CallbackQuery.Message.MessageId;

            if (update.CallbackQuery.Data == "take")
            {
                //foreach (var item in OrderProceccing.Orders)
                //{
                //    Console.WriteLine("here");
                //}

                foreach (var item in OrderProceccing.Orders)
                {
                    foreach (var mesId in item.ForSend)
                    {
                        Console.WriteLine(mesId.MessageId);
                        Console.WriteLine(mesId.ChatId);

                        if (mesId.MessageId == idMessage)
                        {
                            orderContext.LastMessag = idMessage;
                            orderContext = item;
                            break; // add enter up
                        }
                    }
                }

                foreach (var item in orderContext.ForSend)
                {
                    if (idMessage != item.MessageId)
                    {
                        await botClient.EditMessageTextAsync(
                                item.ChatId, item.MessageId, "the order was taken");
                    }
                    else
                    {
                        await botClient.EditMessageTextAsync(
                            item.ChatId, item.MessageId,
                            SendOrDleteOrderState.CurrentProducts, replyMarkup: keyboardNext);
                    }

                }

                //context.State = new ManagerCollectedOrderState();
                int orderId = orderContext.OrderId;
                var orderRepository = new OrderRepository();
                orderRepository.UpdateStatusOrder(orderId, 2);
                orderRepository.AddShopIdInOrder(orderId, context.ShopId);
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







//foreach (var item in UserProcessing.Users)
//{
//    if (item.Value.RoleId == 2)
//    {
//        if (item.Value.ChatId != context.ChatId)
//        {
//            await botClient.EditMessageTextAsync(
//                item.Value.ChatId, item.Value.LastMessageId, "the order was taken");
//        }
//        else
//        {
//            await botClient.EditMessageTextAsync(
//                item.Value.ChatId, item.Value.LastMessageId,
//                SendOrDleteOrderState.CurrentProducts, replyMarkup: keyboardNext);
//            item.Value.State = new ManagerCollectedOrderState();
//        }
//    }
//}
