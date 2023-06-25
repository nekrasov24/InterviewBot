using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class QuestionMarkup : IQuestionMarkup
    {
        public InlineKeyboardMarkup CreateQuestionMarkup( string firstAnswer,
                    string secondAnswer, string threesAnswer, string foursAnswer)
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(new[] {
                new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "1", callbackData: firstAnswer),
                        InlineKeyboardButton.WithCallbackData(text: "2", callbackData: secondAnswer),
                        InlineKeyboardButton.WithCallbackData(text: "3", callbackData: threesAnswer),
                        InlineKeyboardButton.WithCallbackData(text: "4", callbackData: foursAnswer),
                    },
                });

            return inlineKeyboard;
        }        
    }
}
