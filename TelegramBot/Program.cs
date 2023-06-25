using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        
        static readonly ITelegramBotClient bot = new TelegramBotClient("5145900862:AAE_lgBsrY18RVktYrFj9emKfzlek9o350U");
        public readonly ITest _test;
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancelationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            var message = update.Message;
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (message.Text.ToLower() == "/start")
                {
                    var r = new Test();
                    await r.UserRegistration(botClient, update);

                    //await botClient.SendTextMessageAsync(chatId: "1052886210", text: "вммдмлм");
                }
                else if (message.Text.ToLower() == "/start test")
                {
                    var r = new Test();
                    

                    

                    await r.StartTest(botClient, update);
                    
                }
                else
                {
                    var r = new Test();
                    await r.ContinueTest(botClient, update);
                }

            }    
        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exception), "что-то пошло не так");            
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine(" ---> запущен бот " + bot.GetMeAsync().Result.FirstName);
            var ctk = new CancellationTokenSource();
            Console.WriteLine(ctk);
            var cancelationTokenSourse = ctk.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            
            bot.StartReceiving(updateHandler: HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancelationTokenSourse);
            Console.ReadLine();
        }
    }
}
