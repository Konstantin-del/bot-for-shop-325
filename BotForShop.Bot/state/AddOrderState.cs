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
using Telegram.Bot.Types.Enums;

namespace BotForShop.Bot.state
{
    public class AddOrderState :AbstractState
    {
        private int CurrentMessageId;

        string handleAnswerNumber = "0";
 
        bool isString = true;

        Update value;

        int orderId;

        string listProducts;

        //InlineKeyboardMarkup keyboardIs;

        OrderInputModel order = new OrderInputModel();

        OrderProductInputModel orderProduct = new OrderProductInputModel();

        UserService userServis = new UserService();

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {

            switch (handleAnswerNumber)
            {
                case "0":
                    CurrentMessageId = update.CallbackQuery.Message.MessageId;
                    EditAnswerWithButton(context, botClient, "create order", keyboardIs);
                    handleAnswerNumber = "1";
                break;

                case "1":
                    if (update.CallbackQuery.Data == "yes")
                    {
                        order.Date = DateTime.Today;
                        order.AdminId = context.Id;
                        try
                        {
                            orderId = userServis.AddOrder(order);
                            orderProduct.OrderId = orderId;
                            listProducts = $"order id: {orderId} \n" + ShowAllProducts();
                            
                            //EditAnswer(context, botClient, listProducts +"\n ENTYER PRODUCT ID"); 
                        }
                        catch
                        {
                            EditAnswer(context, botClient, "error added in db");
                            UserProcessing.UpdateAdninToAddOrderState();
                            UserProcessing.UserCurrent.BotActionContext(update, botClient);
                            handleAnswerNumber = "0";
                        }
                        
                    }
                    else 
                    {
                        EditAnswer(context, botClient, "redirect to admin menu");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);
                        handleAnswerNumber = "0";
                    }
                    handleAnswerNumber = "2";
                    UserProcessing.UserCurrent.BotActionContext(update, botClient);
                    break;

                case "2":
                    IsCallback(context, botClient, update);
                    if (update.CallbackQuery.Data == "no")
                    {
                        //EditAnswer(context, botClient, "");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);
                        handleAnswerNumber = "0";
                    }
                    else 
                    {
                        CurrentMessageId = update.CallbackQuery.Message.MessageId;
                        EditAnswer(context, botClient, listProducts + "\n" + "ENTYER PRODUCT ID");
                        handleAnswerNumber = "3";
                    }
                    break;

                case "3":
                    orderProduct.ProductId = Convert.ToInt32(update.Message.Text);
                    EditAnswer(context, botClient, listProducts+"\n"+"ENTER COUNT");
                    handleAnswerNumber = "4";
                break;

                case "4":
                    orderProduct.Count = Convert.ToInt32(update.Message.Text);
                    try
                    {
                        userServis.AddOrderProduct(orderProduct);
                        SendAnswerWithButton(context, botClient, "add more", keyboardIs);
                    }
                    catch
                    {
                        EditAnswer(context, botClient, "error adding product in db");
                        UserProcessing.UpdateAdninToStartAdminState();
                        UserProcessing.UserCurrent.BotActionContext(update, botClient);
                        handleAnswerNumber = "0";
                    }
                    handleAnswerNumber = "2";
                    break;

             
                default:
                    EditAnswer(context, botClient, "error creat product");
                    UserProcessing.UpdateAdninToStartAdminState();
                    UserProcessing.UserCurrent.BotActionContext(update, botClient);
                    handleAnswerNumber = "0";
                break;
            }
        }

        public async void SendAnswer(
            Context context, ITelegramBotClient botClient, string text)
        {
            var Message = await botClient.SendTextMessageAsync(
                context.ChatId, $"{text}");
        }

        public async void SendAnswerWithButton(
            Context context, ITelegramBotClient botClient, 
                string text, InlineKeyboardMarkup keyboard)
        {
            var Message = await botClient.SendTextMessageAsync(
                context.ChatId, $"{text}", replyMarkup: keyboard);
        }

        public async void EditAnswer(
            Context context, ITelegramBotClient botClient, string text)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
                text);
        }

        public async void EditAnswerWithButton(
            Context context, ITelegramBotClient botClient, string text,
            InlineKeyboardMarkup keyboard)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
                text, replyMarkup: keyboard);
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


        public async void IsCallback(
            Context context, ITelegramBotClient botClient, Update update)
        {
            if (update.Type == UpdateType.Message)
            {
                await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
                "ERROR, button press expected");
                UserProcessing.UpdateAdninToStartAdminState();
                UserProcessing.UserCurrent.BotActionContext(update, botClient);
            }
        }
    }



}
