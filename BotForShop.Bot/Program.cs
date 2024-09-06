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
            Users = new Dictionary<long, Context>();
            try
            {
                var userServis = new UserService();
                var users = userServis.getUsersForAuthentication();
                foreach (var item in users)
                {
                    var userCurrent = new Context();
                    userCurrent.ChatId = item.ChatId;
                    userCurrent.RoleId = item.RoleId;
                    userCurrent.Name = item.UserName.ToLower();
                    if (userCurrent.RoleId == 3)
                    {
                        userCurrent.State = new StartMenuAdminState();
                        Console.WriteLine("gud");
                    }
                    else
                    {
                        userCurrent.State = new StartMenuState();
                        Console.WriteLine("ups");
                    }
                    Users.Add(userCurrent.ChatId, userCurrent);
                    Console.WriteLine(userCurrent.RoleId);
                    Console.WriteLine(userCurrent.Name);
                }
            }
            catch
            {
                Console.WriteLine("users not loaded from db");
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
                    userCurrent.Name = message.Chat.FirstName.ToLower();
                    userCurrent.ChatId = message.Chat.Id;
                    Users.Add(message.Chat.Id, userCurrent);
                    userCurrent.State = new StartMenuState(); 
                }
                // Users[message.Chat.Id].UserRole == "

                if (update.Message.Text == "1")
                {
                    bool isAdmin = userCurrent.RoleId == 3 || userCurrent.RoleId == 325;
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

        public static long GetChatIdByName(string name)
        {
            return Users.Where(x => x.Value.Name == name.ToLower()).FirstOrDefault().Key;
        }

        //if (userCurrent.State.GetType().ToString() == "AddUserState")

        //if (message.Text.ToLower() == "/start")
       
    }
}
