using BotForShop.BLL;
using BotForShop.Bot.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotForShop.Bot
{
    public static class UserProcessing
    {
        public static Dictionary<long, Context> Users { get; set; } = new Dictionary<long, Context>();

        public static Context userCurrent { get; set; } = new Context();

        public static void GetValuesForAuthentication()
        {
            try
            {
                var userServis = new UserService();
                var usersFromDB = userServis.GetUsersForAuthentication();
                foreach (var item in usersFromDB)
                {
                    var userCurrent = new Context();
                    userCurrent.ChatId = item.ChatId;
                    userCurrent.RoleId = item.RoleId;
                    userCurrent.Name = item.UserName.ToLower();
                    userCurrent.Id = item.Id;

                    if (userCurrent.RoleId == 1)
                    {
                        userCurrent.State = new StartMenuState();
                    }
                    else if (userCurrent.RoleId == 2)
                    {
                        userCurrent.State = new StartMenuState();
                    }
                    else if (userCurrent.RoleId == 3)
                    {
                        userCurrent.State = new StartMenuAdminState();
                    }
                    else
                    {
                        userCurrent.State = new StartMenuState();
                    }
                    Users.Add(userCurrent.ChatId, userCurrent);
                    Console.WriteLine(userCurrent.RoleId);
                    Console.WriteLine(userCurrent.Name);
                    Console.WriteLine(userCurrent.Id);
                }
            }
            catch
            {
                Console.WriteLine("users not loaded from db");
            }
        }

        public static void UpdateCurrentUserIfMessage(Update update)
        {
            var message = update.Message;

            if (Users.ContainsKey(message.Chat.Id))
            {
                userCurrent = Users[message.Chat.Id];
            }
            else
            {
                //userCurrent = new Context();
                userCurrent.Name = message.Chat.FirstName.ToLower();
                userCurrent.ChatId = message.Chat.Id;
                Users.Add(message.Chat.Id, userCurrent);
                userCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateCurrentUserIfCallback(Update update)
        {
            var callback = update.CallbackQuery;

            if (Users.ContainsKey(callback.From.Id))
            {
                userCurrent = Users[callback.From.Id];
            }
            else
            {
                //userCurrent = new Context();
                userCurrent.Name = callback.From.FirstName.ToLower();
                userCurrent.ChatId = callback.From.Id;
                Users.Add(callback.From.Id, userCurrent);
                userCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateAdninToStartAdminState()
        {
            userCurrent.State = new StartMenuAdminState();
        }

        public static void UpdateAdninToAddUserState() 
        {
            bool isAdmin = userCurrent.RoleId == 3 || userCurrent.RoleId == 325;
            if (isAdmin)
            {
                userCurrent.State = new AddUserState();
            }   
            else
            {

                userCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateAdninToAddOrderState()
        {
            //await botClient.EditMessageTextAsync(new ChatId(context.ChatId),
               // update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text);
            userCurrent.State = new AddOrderState();
        }

        public static long GetChatIdByName(string name)
        {
            return Users.Where(x => x.Value.Name == name.ToLower()).FirstOrDefault().Key;
        }

    }
}
