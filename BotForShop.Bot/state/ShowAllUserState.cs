﻿
using BotForShop.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot.state
{
    public class ShowAllUser : AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {

        }
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            var userService = new UserService();

            var users = userService.GetAllUsers(); 

            string mess = "";

            foreach (var item in users)
            {
                mess += $"{item.Name} \n";
            }
            await botClient.SendTextMessageAsync(update.Message.Chat, mess);
        }
    }
}
