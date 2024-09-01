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

        public List<UserDto> Users;

        static string token = Environment.GetEnvironmentVariable("Gram");
        
        static void Main(string[] args)
        {

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
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                Context context = new Context();

                if (message.Text != "1" && message.Text != "2")
                {

                    context.State = new StartMenuState();
                    context.BotActionContext(update, botClient);
                }

                else if (message.Text == "1")
                {
                    context.State = new ShowAllUser();
                    context.BotActionContext(update, botClient);
                }





                //if (message.Text.ToLower() == "/start")
                //{
   
                //    await botClient.SendTextMessageAsync(message.Chat, $"Your name is {message.Chat.FirstName}");
                //}
                //if (message.Text != null)
                //{
                //    var user = new UserInputModel()
                //    {
                //        Name = message.Text
                //    };

                //    Console.WriteLine(user.Name);

                //    var userService = new UserService();

                //    userService.AddUser(user); // добавляем юзера в тестовую таблицу
     
                //}
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"you entered - {message.Text}");// отправляем юзеров в телегу
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
