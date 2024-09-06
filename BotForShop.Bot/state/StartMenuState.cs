
using Telegram.Bot.Types;
using Telegram.Bot;
using Npgsql.Replication.PgOutput.Messages;
using static System.Net.Mime.MediaTypeNames;

namespace BotForShop.Bot.state
{
    public class StartMenuState : AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            
        }

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {


            if (update.Message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(update.Message.Chat,
                "Hello, i'm Bot For Shop \n send message to open the menu");
              
            }
            else if (update.Message.Text.ToLower() == "325")
            {
                context.UserRole = "325";
                context.State = new StartMenuAdminState();
                context.BotActionContext(update, botClient);
            }
        }


    }
}
