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

        public static Dictionary<long, Context> Users { get; set; } 

        static string token = Environment.GetEnvironmentVariable("Gram");

        static long MyChatId = 5926961743;


        static void Main(string[] args)
        {
            try
            {
                var userServis = new UserService();
                var users = userServis.getUsersForAuthentication();
                foreach (var item in users)
                {
                    var userCurrent = new Context();
                    userCurrent.ChatId = item.ChatId;
                    userCurrent.UserRole = item.UserRole;
                    userCurrent.Name = item.Name;
                    if (item.UserRole == "administrator") userCurrent.State = new StartMenuAdminState();
                    Users.Add(item.ChatId, userCurrent);
                }
            }
            catch
            {
                Users = new Dictionary<long, Context>();
            }


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

        public static long GetChat(string name)
        {
             return  Users.Where(x => x.Value.Name == name).FirstOrDefault().Key;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                Context userCurrent;

                if (Users.ContainsKey(message.Chat.Id) )
                {
                    userCurrent = Users[message.Chat.Id];
                }
                else
                {
                    userCurrent = new Context();
                    userCurrent.Name = message.Chat.FirstName;
                    userCurrent.ChatId = message.Chat.Id;
                    Console.WriteLine(userCurrent.ChatId);
                    //Console.WriteLine(userCurrent.ChatId);
                    Users.Add(message.Chat.Id, userCurrent);
                    userCurrent.State = new StartMenuState(); 
                }
                // Users[message.Chat.Id].UserRole == "

                if (update.Message.Text == "1")
                {
                    bool isAdmin = userCurrent.UserRole == "administrator" || userCurrent.UserRole == "325";
                    if (isAdmin)
                    {
                        userCurrent.State = new AddUserState();
                    }
                }
  
                userCurrent.BotActionContext(update, botClient);
                
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
        }

        //if (userCurrent.State.GetType().ToString() == "AddUserState")

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
        //else
        //{
        //    await botClient.SendTextMessageAsync(message.Chat, $"you entered - {message.Text}");// отправляем юзеров в телегу
        //}
    }
}
