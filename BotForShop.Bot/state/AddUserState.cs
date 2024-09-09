using BotForShop.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using BotForShop.BLL;
using System;

namespace BotForShop.Bot.state
{
    public class AddUserState: AbstractState
    {
        public string HandleAnswerNumber { get; set; } = "0";
        public UserInputModel User { get; set; } = new UserInputModel();

        string value;

        bool isString = false;

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            if(HandleAnswerNumber != "0")
            {
                value = update.Message.Text;
                isString = string.IsNullOrEmpty(value);
            }
            
            if (!isString)
            {
                switch (HandleAnswerNumber)
                {
                    case "0":
                        EditAnswer(context, update, botClient);
                        SendAnswer(context, botClient, "To get chat id user, enter name");
                        HandleAnswerNumber = "1";
                        break;

                    case "1":
                        var chatId = UserProcessing.GetChatIdByName(value);
                        if (chatId != 0)
                        {
                            User.ChatId = chatId;
                            SendAnswer(
                                context, botClient, $"chatid-{chatId} added \n" + 
                                ShowIdRolesAndShops() +"\n"+"Enter name"
                                );
                            HandleAnswerNumber = "2";
                        }
                        else
                        {
                            SendAnswer(context, botClient, "no such name");
                            context.State = new StartMenuAdminState();
                            context.BotActionContext(update, botClient);
                            HandleAnswerNumber = "0";
                        }
                        break;

                    case "2":
                        User.UserName = value;
                        SendAnswer(context, botClient, "enter phone");
                        HandleAnswerNumber = "3";
                        break;

                    case "3":
                        User.Phone = value;;
                        SendAnswer(context, botClient, "enter role id");
                        HandleAnswerNumber = "4";
                        break;

                    case "4":
                        User.RoleId = Convert.ToInt32(value);
                        SendAnswer(context, botClient, "enter shope id");
                        HandleAnswerNumber = "5";
                        break;

                    case "5":
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
                        HandleAnswerNumber = "1";
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

        public async void EditAnswer(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.EditMessageTextAsync(
                context.ChatId, update.CallbackQuery.Message.MessageId,
                "add user state", replyMarkup: context.keyboard);
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
        
    }
}