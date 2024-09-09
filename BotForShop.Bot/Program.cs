using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using BotForShop.BLL;
using BotForShop.Core.Dtos;
using BotForShop.Bot.state;



namespace BotForShop.Bot
{
    public class Program
    {

        static string token = Environment.GetEnvironmentVariable("Gram");

        static void Main(string[] args)
        {
            UserProcessing.GetValuesForAuthentication();

            ITelegramBotClient bot = new TelegramBotClient(token);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.WriteLine("Ok");

            Console.ReadLine();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.Message) // for message type message
            {
                UserProcessing.UpdateCurrentUserIfMessage(update);
            }

            else if (update.Type == UpdateType.CallbackQuery) // for message type callback
            {
                UserProcessing.UpdateCurrentUserIfCallback(update);

                if (update.CallbackQuery.Data == "1")
                {
                    UserProcessing.UpdateAdninToAddOrderState();
                }

                if (update.CallbackQuery.Data == "2")
                {
                    UserProcessing.UpdateAdninToAddUserState();
                }

                if (update.CallbackQuery.Data == "3")
                {
                    UserProcessing.userCurrent.State = new StartMenuAdminState();
                }
            }

            UserProcessing.userCurrent.BotActionContext(update, botClient);
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
        }
  
    }
}
