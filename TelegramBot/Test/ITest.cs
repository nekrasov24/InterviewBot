using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface ITest
    {
        Task UserRegistration(ITelegramBotClient telegramBotClient, Update update);
        Task StartTest(ITelegramBotClient telegramBotClient, Update update);
        Task ContinueTest(ITelegramBotClient telegramBotClient, Update update);
    }
}
