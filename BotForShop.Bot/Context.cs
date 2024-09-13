using BotForShop.Bot.state;
using BotForShop.Core.Dtos;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotForShop.Bot
{
    public class Context
    {
        public long ChatId { get; set; }

        public string? Name { get; set; }

        public int LastMessageId { get; set; }

        public int? RoleId { get; set; }

        public int Id { get; set; }

        public InlineKeyboardMarkup? keyboard { get; set; }

        public AbstractState State { get; set; }

        public void BotActionContext(Update update, ITelegramBotClient botClient)
        {
            State.BotAction(this, update, botClient);
        }
        
    }
}
