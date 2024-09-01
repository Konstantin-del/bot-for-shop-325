
using Telegram.Bot.Types;
using Telegram.Bot;
using Npgsql.Replication.PgOutput.Messages;

namespace BotForShop.Bot.state
{
    public class StartMenuState : AbstractState
    {
        public override void HandleMessage(Context context, Update update)
        {

        }

        bool isFirst = true;
        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            if (update.Message.Text.ToLower() == "/start" && isFirst)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat,
                    "Hello, i'm Bot For Shop \n press any button to open the menu");
                isFirst = false;
            }

            else
            {
                string text =

                 "enter namber \n" +
                 "add user: 1 \n" +
                 "get one user: 2 \n";

                await botClient.SendTextMessageAsync(update.Message.Chat, text);
            }
        }


    }
}
