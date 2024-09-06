using BotForShop.Bot.state;
using BotForShop.Core.Dtos;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotForShop.Bot
{
    public class Context
    {
        public long ChatId { get; set; }

        public string? Name { get; set; }

        public string? NicName { get; set; }

        public string? UserRole { get; set; }

        public AbstractState State { get; set; }

        public void HandleMessageContext(Update update, ITelegramBotClient botClient)
        {
            State.HandleMessage(this, update, botClient);
        }
        public void BotActionContext(Update update, ITelegramBotClient botClient)
        {
            State.BotAction(this, update, botClient);
        }
        
    }
}
