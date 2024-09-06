using BotForShop.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using BotForShop.BLL;
using System;

namespace BotForShop.Bot.state
{
    public class AddUserState: AbstractState
    {
        public static string HandleAnswerNumber = "0";
        public UserInputModel User { get; set; } = new UserInputModel();
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
           
        }

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            var value = update.Message.Text;
            bool isString = string.IsNullOrEmpty(value);
            
            if (!isString)
            {
                switch (HandleAnswerNumber)
                {
                    case "0":
                        HandleAnswer(update, botClient, "What get chat id users, enter name");
                        HandleAnswerNumber = "1";
                        break;

                    case "1":
                        var chatId = Program.GetChat(value);
                        if (chatId != 0)
                        {
                            User.ChatId = chatId;

                            HandleAnswer(update, botClient, $"chatid-{chatId} added \n" + 
                            ShowIdRolesAndShops() + "enter name");

                            HandleAnswerNumber = "2";
                        }
                        else
                        {
                            HandleAnswer(update, botClient, "error");
                            context.State = new StartMenuAdminState();
                            context.BotActionContext(update, botClient);
                        }
                        break;

                    case "2":
                        User.Name = value;
                        HandleAnswer(update, botClient, "enter phone");
                        HandleAnswerNumber = "3";
                        break;

                    case "3":
                        User.Phone = value;
                        HandleAnswer(update, botClient, "enter role id");
                        HandleAnswerNumber = "4";
                        break;

                    case "4":
                        User.RoleId = Convert.ToInt32(value);
                        HandleAnswer(update, botClient, "enter shope id");
                        HandleAnswerNumber = "5";
                        break;

                    case "5":
                        User.ShopeId = Convert.ToInt32(value);
                        var userServis = new UserService();
                        userServis.AddUser(User);
                        HandleAnswer(update, botClient, "create user");
                        context.State = new StartMenuAdminState();
                        break;

                    default:
                        HandleAnswer(update, botClient, "error");
                        HandleAnswerNumber = "1";
                        break;
                }
            }
            else
            {
                HandleAnswer(update, botClient, "the message is not correct");
                HandleAnswerNumber = "1";
            }
        }
        public async void HandleAnswer(Update update, ITelegramBotClient botClient, string text)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, $"{text}");
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

// await botClient.SendTextMessageAsync(5926961743, "user registration");