
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BotForShop.Bot.state
{
    public class StartMenuState : AbstractState
    {
        public override void HandleMessage(Context context, Update update)
        {

        }

        public override async void BotAction(Context context, Update update, ITelegramBotClient botClient)
        {
            string text =
             "enter namber \n" +
             "add user: 1 \n" +
             "get one user: 2 \n";

            await botClient.SendTextMessageAsync(update.Message.Chat, text);
        }


    }
}
