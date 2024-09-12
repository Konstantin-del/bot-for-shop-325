using BotForShop.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using BotForShop.BLL;
using System;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types.Enums;

namespace BotForShop.Bot.state
{
    public class AddUserState: AbstractState
    {
        public string HandleAnswerNumber { get; set; } = "0";
        public UserInputModel User { get; set; } = new UserInputModel();
        public int CurrentMessageId { get; set; }

        string value;

        bool isString = false;

        string currentMessage;

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {


            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery.Data == "end")
            {
                EditAnswer(context, botClient, "CANCEL");
                UserProcessing.UpdateAdninToStartAdminState();
                UserProcessing.UserCurrent.BotActionContext(update, botClient);
                HandleAnswerNumber = "0";
            }
            else
            {
                if (HandleAnswerNumber != "0" && HandleAnswerNumber != "1")
                {
                    value = update.Message.Text;
                    isString = string.IsNullOrEmpty(value);
                }



                if (!isString)
                {
                    switch (HandleAnswerNumber)
                    {
                        case "0":
                            CurrentMessageId = update.CallbackQuery.Message.MessageId;
                            EditAnswerWithButton(context, botClient, "create user", keyboardYesNo);
                            HandleAnswerNumber = "1";
                            break;

                        case "1":
                            IsCallback(context, botClient, update);
                            if (update.CallbackQuery.Data == "yes")
                            {
                                IsCallback(context, botClient, update);
                                //EditAnswer(context, botClient, "To add chat id, enter user name");
                                EditAnswerWithButton(
                                    context, botClient,
                                    "To add chat id, enter user name", keyboardCancel);
                                HandleAnswerNumber = "2";
                            }
                            else
                            {
                                EditAnswer(context, botClient, "redirect to admin menu");
                                UserProcessing.UpdateAdninToStartAdminState();
                                UserProcessing.UserCurrent.BotActionContext(update, botClient);
                                HandleAnswerNumber = "0";
                            }
                            break;

                        case "2":
                            var chatId = UserProcessing.GetChatIdByName(value); //here value is name  
                            if (chatId != 0) // add check it user in db 
                            {
                                User.ChatId = chatId;
                                User.UserName = value;

                                currentMessage = $"chatid-{chatId} \n" +
                                    $"name-{value} added \n" + ShowIdRolesAndShops();

                                EditAnswerWithButton(
                                    context, botClient,
                                    currentMessage + "\n ENTER PHONE", keyboardCancel);
                                //EditAnswer(context, botClient, currentMessage + "\n ENTER PHONE");
                                HandleAnswerNumber = "3";
                            }
                            else
                            {
                                EditAnswer(context, botClient, "redirect to admin menu");
                                UserProcessing.UpdateAdninToStartAdminState();
                                UserProcessing.UserCurrent.BotActionContext(update, botClient);
                                HandleAnswerNumber = "0";
                            }
                            break;


                        case "3":
                            User.Phone = value;
                            //EditAnswer(context, botClient, currentMessage + "\n ENTER ROLE ID");
                            EditAnswerWithButton(
                                   context, botClient,
                              currentMessage + "\n ENTER ROLE ID", keyboardCancel);
                            HandleAnswerNumber = "4";
                            break;

                        case "4":
                            User.RoleId = Convert.ToInt32(value);
                            //EditAnswer(context, botClient, currentMessage + "\n ENTER SHOP ID");
                            EditAnswerWithButton(
                                   context, botClient,
                              currentMessage + "\n ENTER SHOP ID", keyboardCancel);
                            HandleAnswerNumber = "5";
                            break;

                        case "5":
                            User.ShopId = Convert.ToInt32(value);
                            try
                            {
                                var userServis = new UserService();
                                userServis.AddUser(User);
                                EditAnswer(context, botClient, "USER ADDED");
                                UserProcessing.GetValuesForAuthentication(); // update users
                                context.State = new StartMenuAdminState();
                                HandleAnswerNumber = "0";
                            }
                            catch
                            {
                                EditAnswer(context, botClient, "error adding user in db");
                                Console.WriteLine("error adding user in db");
                            }
                            if (HandleAnswerNumber == "0")
                            {
                                UserProcessing.GetValuesForAuthentication(); // update users
                            }
                            UserProcessing.UpdateAdninToStartAdminState();
                            UserProcessing.UserCurrent.BotActionContext(update, botClient);
                            break;

                        default:
                            EditAnswer(context, botClient, "error filing object");
                            UserProcessing.UpdateAdninToStartAdminState();
                            UserProcessing.UserCurrent.BotActionContext(update, botClient);
                            HandleAnswerNumber = "0";
                            break;
                    }
                }
                else
                {
                    SendAnswer(context, botClient, "the message is not correct");
                    UserProcessing.UpdateAdninToStartAdminState();
                    UserProcessing.UserCurrent.BotActionContext(update, botClient);
                    HandleAnswerNumber = "0";
                }
            }
        }
        public async void SendAnswer(Context context, ITelegramBotClient botClient, string text)
        {
            await botClient.SendTextMessageAsync(context.ChatId, $"{text}");
        }

        public async void EditAnswer(Context context,
            ITelegramBotClient botClient, string text)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
                text);
        }

        public async void EditAnswerWithButton(Context context,
            ITelegramBotClient botClient, string text, InlineKeyboardMarkup keyboard)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, CurrentMessageId,
                text, replyMarkup: keyboard);
        }

        public string ShowIdRolesAndShops()
        {
            UserService userService = new UserService();
            var roles = userService.GetUserRole();
            var addresses = userService.GetShopAddresses();
            string info = "";

            foreach (var item in roles)
            {
                info += $"{item.Id}-{item.UserRole} \n";
            }
            foreach (var item in addresses)
            {
                 
                info += $"{item.Id}-{item.ShopAddress.TrimEnd()} \n";
            }
            return info;
        }

        public async void IsCallback(Context context, ITelegramBotClient botClient, Update update)
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

        InlineKeyboardMarkup keyboardYesNo = new InlineKeyboardMarkup(
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

        InlineKeyboardMarkup keyboardCancel = new InlineKeyboardMarkup(
            new[]
            {
                    InlineKeyboardButton.WithCallbackData("Cancel", "end")
            }
        );

        //public async void IsMessage(Context context, ITelegramBotClient botClient, Update update)
        //{
        //    if(update.Type == UpdateType.CallbackQuery)
        //    {
        //        await botClient.EditMessageTextAsync(
        //        context.ChatId, CurrentMessageId,
        //        "ERROR, text was expected");
        //        UserProcessing.UpdateAdninToStartAdminState();
        //        UserProcessing.UserCurrent.BotActionContext(update, botClient);
        //    }
        //}

    }
}