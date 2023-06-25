using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public interface IQuestionMarkup
    {
        InlineKeyboardMarkup CreateQuestionMarkup(string secondQuestionDiscription,
                    string threesQuestionDiscription, string foursQuestionDiscription, string nextAnswer);
    }
}
