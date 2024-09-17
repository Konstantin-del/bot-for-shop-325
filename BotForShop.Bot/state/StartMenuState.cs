
using Telegram.Bot.Types;
using Telegram.Bot;
using Npgsql.Replication.PgOutput.Messages;
using static System.Net.Mime.MediaTypeNames;

namespace BotForShop.Bot.state
{
    public class StartMenuState : AbstractState
    { 

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            string startText = "Hello, i'm Bot For Shop \n" +
                "Only emploeyees of company \n" +
                "can use me, if you want to \n" +
                "register, send a special message \n" +
                "and we will forward \n" +
                "your request to the administrator";

            if (update.Message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(update.Message.Chat, startText);   
            }
            else if (update.Message.Text.ToLower() == "325")
            {
                context.RoleId = 325;
                context.State = new StartMenuAdminState();
                context.BotActionContext(update, botClient);
            }
            else 
            {
                Console.WriteLine("unhandled exception");
                //await botClient.SendTextMessageAsync(update.Message.Chat, startText);
            }
        }


    }
}
