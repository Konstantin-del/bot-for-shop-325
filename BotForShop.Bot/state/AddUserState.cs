using BotForShop.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using BotForShop.BLL;
using System;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace BotForShop.Bot.state
{
    public class AddUserState: AbstractState
    {
        public string HandleAnswerNumber { get; set; } = "0";
        public UserInputModel User { get; set; } = new UserInputModel();
        public static int CurrentMessageId { get; set; }

        string value;

        bool isString = false;

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            if(HandleAnswerNumber != "0" && HandleAnswerNumber != "1")
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
                        await botClient.EditMessageTextAsync(
                            context.ChatId, update.CallbackQuery.Message.MessageId,
                            "create user", replyMarkup: keyboardIs);
                            HandleAnswerNumber = "1";
                        break;

                    case "1":
                        if(update.CallbackQuery.Data == "yes")
                        {
                            
                            EditAnswer(context, botClient, "To get chat id user, enter name");
                            HandleAnswerNumber = "2";
                        }
                        else
                        {
                            EditAnswer(context, botClient, "redirect to admin menu");
                            UserProcessing.UpdateAdninToStartAdminState();
                            UserProcessing.userCurrent.BotActionContext(update, botClient);
                            HandleAnswerNumber = "0";
                        }
                        break;

                    case "2":
                        var chatId = UserProcessing.GetChatIdByName(value);//here value is name  
                        if (chatId != 0)
                        {
                            User.ChatId = chatId;
                            SendAnswer(
                                context, botClient, $"chatid-{chatId} added \n" + 
                                ShowIdRolesAndShops() +"\n"+"Enter name"
                                );
                            HandleAnswerNumber = "3";
                        }
                        else
                        {
                            EditAnswer(context, botClient, "redirect to admin menu");
                            UserProcessing.UpdateAdninToStartAdminState();
                            UserProcessing.userCurrent.BotActionContext(update, botClient);
                            HandleAnswerNumber = "0";
                        }
                        break;

                    case "3":
                        User.UserName = value;
                        SendAnswer(context, botClient, "enter phone");
                        HandleAnswerNumber = "4";
                        break;

                    case "4":
                        User.Phone = value;;
                        SendAnswer(context, botClient, "enter role id");
                        HandleAnswerNumber = "5";
                        break;

                    case "5":
                        User.RoleId = Convert.ToInt32(value);
                        SendAnswer(context, botClient, "enter shope id");
                        HandleAnswerNumber = "6";
                        break;

                    case "6":
                        User.ShopId = Convert.ToInt32(value);
                        try 
                        {
                            var userServis = new UserService();
                            int userId = userServis.AddUser(User);
                            SendAnswer(context, botClient, "user added");
                            context.Id = userId;
                            UserProcessing.GetValuesForAuthentication(); // update users
                            context.State = new StartMenuAdminState();
                            HandleAnswerNumber = "0";
                        }
                        catch 
                        {
                            SendAnswer(context, botClient, "error adding user ");
                            context.State = new StartMenuAdminState();
                        }
                        break;

                    default:
                        SendAnswer(context, botClient, "error filing object");
                        HandleAnswerNumber = "2";
                        break;
                }
            }
            else
            {
                SendAnswer(context, botClient, "the message is not correct");
                HandleAnswerNumber = "1";
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