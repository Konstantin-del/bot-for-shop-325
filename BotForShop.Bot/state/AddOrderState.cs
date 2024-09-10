using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;
using BotForShop.BLL;
using BotForShop.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using System.Diagnostics.Metrics;

namespace BotForShop.Bot.state
{
    public class AddOrderState :AbstractState
    {
        int currentMessageId;

        string handleAnswerNumber = "0";
 
        bool isString = true;

        Update value;

        int orderId;

        OrderInputModel order = new OrderInputModel();

        OrderProductInputModel orderProduct = new OrderProductInputModel();

        UserService userServis = new UserService();

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {

            switch (handleAnswerNumber)
            {
                case "0":
                    currentMessageId = update.CallbackQuery.Message.MessageId;
                    await botClient.EditMessageTextAsync(
                    context.ChatId, currentMessageId,
                    "create order", replyMarkup: keyboardIs);
                    //EditAnswer(context, botClient, "create order", keyboardIs);

                    

                    handleAnswerNumber = "1";
                    break;

                case "1":
                    if (update.CallbackQuery.Data == "yes")
                    {
                        currentMessageId = update.CallbackQuery.Message.MessageId;
                        EditAnswer(context, botClient, "create order");
                        order.Date = DateTime.Today;
                        order.AdminId = context.Id;
                        try
                        {
                            orderId = userServis.AddOrder(order);
                            EditAnswer(context, botClient, $"created order, id - {orderId}");
                            SendAnswer(context, botClient, ShowAllProducts() + "\n " +
                                "enter id product");
                            orderProduct.OrderId = orderId;
                        }
                        catch
                        {
                            Console.WriteLine("ERROR db");
                            EditAnswer(context, botClient, "error db");
                            UserProcessing.UpdateAdninToAddOrderState();
                            UserProcessing.userCurrent.BotActionContext(update, botClient);
                            handleAnswerNumber = "0";
                        }
                    }
                    else 
                    {
                        EditAnswer(context, botClient, "redirect to admin menu");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.userCurrent.BotActionContext(update, botClient);
                        handleAnswerNumber = "0";
                    }
                    handleAnswerNumber = "2";
                    break;

                case "2":
                    
                    orderProduct.ProductId = Convert.ToInt32(update.Message.Text);
                    SendAnswer(context, botClient, "inter count");
                    handleAnswerNumber = "3";
                    break;

                case "3":
                    orderProduct.Count = Convert.ToInt32(update.Message.Text);
                    userServis.AddOrderProduct(orderProduct);
                    break;

             
                default:
                        SendAnswer(context, botClient, "error filing object");
                        handleAnswerNumber = "1";
                        break;

                }
            
            //else
            //{
            //    HandleAnswer(update, botClient, "the message is not correct");
            //    HandleAnswerNumber = "1";
            //}
        }
        public async void SendAnswer(
            Context context, ITelegramBotClient botClient, string text)
        {
            var Message = await botClient.SendTextMessageAsync(
                context.ChatId, $"{text}");
        }

        public async void EditAnswer(Context context,
            ITelegramBotClient botClient, string text)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, currentMessageId,
                text);
        }
        public string ShowAllProducts()
        {
            UserService userService = new UserService();
            var products = userService.GetAllProducts();
            
            string info = "";

            foreach (var item in products)
            {
                info += $"{item.Id}-{item.ProductName} \n";
            }
            return info;
        }

        InlineKeyboardMarkup keyboardIs = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("yes", "yes")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("no", "no")
                },
            }
        );


    }



}
