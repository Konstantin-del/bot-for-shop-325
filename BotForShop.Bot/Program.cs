using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using BotForShop.BLL;
using BotForShop.Core.Dtos;

namespace BotForShop.Bot
{
    public class Program
    {

        public List<UserDto> Users;

        static string token = Environment.GetEnvironmentVariable("Gram");
        public static UserService UserService { get; set; }
        static void Main(string[] args)
        {

            var userService = new UserService();
            var h = userService.GetAllOrderWith();

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

                string text =
                    "enter namber \n" +
                    "add user: 1 \n" +
                    "get one user: 2 \n" +
                    "get all users: 3 \n";

                await botClient.SendTextMessageAsync(message.Chat, text);

                if (message.Text.ToLower() == "/start")
                {
   
                    await botClient.SendTextMessageAsync(message.Chat, $"Your name is {message.Chat.FirstName}");
                }
                else if (message.Text != null)
                {
                    //var user = new UserInputModel()
                    //{
                    //    Name = message.Text
                    //};

                    //Console.WriteLine(user.Name);

                    //var userService = new UserService();

                    //userService.AddUser(user); // добавляем юзера в тестовую таблицу

                    //var users = userService.GetAllUsers(); // извлекаем юзеров из тестовой таблицы


                    //string mess = "";

                    //foreach (var item in users)
                    //{
                    //    mess += $"{item.Name} \n";
                    //}
                    //await botClient.SendTextMessageAsync(message.Chat, mess);
                }
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
