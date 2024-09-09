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

namespace BotForShop.Bot.state
{
    public class AddOrderState :AbstractState
    {
        public static int CurrentMessageId { get; set; }

        public static string HandleAnswerNumber { get; set; } = "0";
 
        public OrderInputModel Order { get; set; } = new OrderInputModel();

        bool isString = true;

        Update value;

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {

            switch (HandleAnswerNumber)
            {
                case "0":
                    CurrentMessageId = update.CallbackQuery.Message.MessageId;
                    await botClient.EditMessageTextAsync(
                    context.ChatId, CurrentMessageId,
                    "create order", replyMarkup: keyboardIs);
                    
                    //SendAnswer(context, botClient, ShowAllProducts());
                    //SendAnswer(context, botClient, "write product id");
                    HandleAnswerNumber = "1";
                    break;


                    //value = update.Message.Text;
                    //isString = string.IsNullOrEmpty(value);
              
                case "1":
                    if (update.CallbackQuery.Data == "yes")
                    {
                        CurrentMessageId = update.CallbackQuery.Message.MessageId;
                        EditAnswer(context, botClient, "creat order");
                    
                    }
                    else 
                    {
                        UserProcessing.UpdateAdninToAddUserState();
                        UserProcessing.userCurrent.BotActionContext(update, botClient);
                    }
                        
                    

                    //await botClient.EditMessageTextAsync(
                    //    context.ChatId, CurrentMessageId,
                    //    "create order", replyMarkup: keyboardIs);

                    //   SendAnswer(context, botClient, ShowAllProducts());
                    //   SendAnswer(context, botClient, "write product id");
                    HandleAnswerNumber = "2";
                        break;

                    //case "2":
                    //    var chatId = UserProcessing.GetChatIdByName(value);
                    //    if (chatId != 0)
                    //    {
                    //        User.ChatId = chatId;
                    //        HandleAnswer(
                    //            update, botClient, $"chatid-{chatId} added \n" +
                    //            ShowIdRolesAndShops() + "\n" + "Enter name"
                    //            );
                    //        HandleAnswerNumber = "2";
                    //    }
                    //    else
                    //    {
                    //        HandleAnswer(update, botClient, "no such name");
                    //        context.State = new StartMenuAdminState();
                    //        context.BotActionContext(update, botClient);
                    //        HandleAnswerNumber = "0";
                    //    }
                    //    break;

                    //case "2":
                    //    User.UserName = value;
                    //    HandleAnswer(update, botClient, "enter phone");
                    //    HandleAnswerNumber = "3";
                    //    break;

                    //case "5":
                    //    User.ShopId = Convert.ToInt32(value);
                    //    try
                    //    {
                    //        var userServis = new UserService();
                    //        userServis.AddUser(User);
                    //        HandleAnswer(update, botClient, "user added");
                    //        UserProcessing.GetValuesForAuthentication(); // update users
                    //        context.State = new StartMenuAdminState();
                    //        HandleAnswerNumber = "0";
                    //    }
                    //    catch
                    //    {
                    //        HandleAnswer(update, botClient, "error adding user ");
                    //        context.State = new StartMenuAdminState();
                    //    }
                    //    break;

                    default:
                        SendAnswer(context, botClient, "error filing object");
                        HandleAnswerNumber = "1";
                        break;

                }
            
            //else
            //{
            //    HandleAnswer(update, botClient, "the message is not correct");
            //    HandleAnswerNumber = "1";
            //}
        }
        public async void SendAnswer(
            Context context, ITelegramBotClient botClient, string text,
            bool isRememberMessageId = true)
        {
            var Message = await botClient.SendTextMessageAsync(
                context.ChatId, $"{text}");
            if(isRememberMessageId)
            CurrentMessageId = Message.MessageId;
        }

        public async void EditAnswer(Context context,
            ITelegramBotClient botClient, string text)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
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
