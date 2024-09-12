using BotForShop.BLL;
using BotForShop.Bot.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot
{
    public static class UserProcessing
    {
        public static Dictionary<long, Context> Users { get; set; }  

        public static Context UserCurrent { get; set; } 

        public static void GetValuesForAuthentication()
        {
            Users = new Dictionary<long, Context>();
            try
            {
                var pepols = new Dictionary<long, Context>();
                var userServis = new UserService();
                var usersFromDB = userServis.GetUsersForAuthentication();
                foreach (var item in usersFromDB)
                {
                    var userCurrent = new Context();
                    userCurrent.ChatId = item.ChatId;
                    userCurrent.RoleId = item.RoleId;
                    userCurrent.Name = item.UserName.ToLower().TrimEnd();
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
                    pepols.Add(userCurrent.ChatId, userCurrent);
                }
                Users = pepols;
            }
            catch
            {
                Console.WriteLine("users could not be loaded from the db");
            }
        }

        public static void UpdateCurrentUserIfMessage(Update update)
        {
            var message = update.Message;
            UserCurrent = new Context();

            if (Users.ContainsKey(message.Chat.Id))
            {
                UserCurrent = Users[message.Chat.Id];
            }
            else
            {
                UserCurrent.Name = message.Chat.FirstName.ToLower();
                UserCurrent.ChatId = message.Chat.Id;
                Users.Add(message.Chat.Id, UserCurrent);
                UserCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateCurrentUserIfCallback(Update update)
        {
            var callback = update.CallbackQuery;
            UserCurrent = new Context();

            if (Users.ContainsKey(callback.From.Id))
            {
                UserCurrent = Users[callback.From.Id];
            }
            else
            {
                UserCurrent.Name = callback.From.FirstName.ToLower();
                UserCurrent.ChatId = callback.From.Id;
                Users.Add(callback.From.Id, UserCurrent);
                UserCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateAdninToStartAdminState()
        {
            UserCurrent.State = new StartMenuAdminState();
        }

        public static void UpdateAdninToAddedOrderState()
        {
            UserCurrent.State = new AddedOrderAdminState();
        }

        public static void UpdateAdninToAddUserState() 
        {
            bool isAdmin = UserCurrent.RoleId == 3 || UserCurrent.RoleId == 325;
            if (isAdmin)
            {
                UserCurrent.State = new AddUserState();
            }   
            else
            {
                UserCurrent.State = new StartMenuState();
            }
        }

        public static void UpdateAdninToAddOrderState()
        {
            UserCurrent.State = new AddOrderState();
        }

        public static long GetChatIdByName(string name)
        {
            return Users.Where(x => x.Value.Name == name.ToLower()).FirstOrDefault().Key;
        }

    }
}
