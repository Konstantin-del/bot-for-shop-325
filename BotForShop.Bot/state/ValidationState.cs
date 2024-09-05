using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot.state
{
    public class ValidationState
    {
        //public Context Users { get; set; }
        //public void UserControl(Update update, ITelegramBotClient botClient)
        //{
        //    var Users = new Dictionary<long, Context>();


        //    var message = update.Message;

        //    Context userCurrent;

        //    if (Users.ContainsKey(message.Chat.Id))
        //    {
        //        userCurrent = Users[message.Chat.Id];

        //        userCurrent.BotActionContext(update, botClient);
        //    }
        //    else
        //    {
        //        userCurrent = new Context();
        //        userCurrent.ChatId = message.Chat.Id;
        //        Console.WriteLine(userCurrent.ChatId);
        //        Users.Add(message.Chat.Id, userCurrent);
        //        userCurrent.State = new StartMenuState();
        //        userCurrent.BotActionContext(update, botClient);
        //    }
        //}
    }
}
