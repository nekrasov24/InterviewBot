using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Repository;

namespace TelegramBot
{
    public class Test : ITest
    {
        
        private readonly IQuestionMarkup _questionMarkup;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<QuestionDiscription> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Grade> _gradeRepository;

        public Test()
        {
           
        }

        public async Task UserRegistration(ITelegramBotClient telegramBotClient, Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                var newUser = new User() { Id = Guid.NewGuid(), Name = Newtonsoft.Json.JsonConvert.SerializeObject(message),
                    ChatId = message.Chat.Id};
                await _userRepository.AddAsync(newUser);
                var userId = newUser.Id.ToString();
                    await telegramBotClient.SendTextMessageAsync(chatId: "1052886210", text: $"Start test using this vode {userId}");
            }            
        }

        public async Task StartTest(ITelegramBotClient telegramBotClient, Update update)
        {
            try
                { 
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {

                    var firstQuestion = _questionRepository.FindFirst();
                    var questionDiscript = firstQuestion.Discription;

                    var firstAnswer = firstQuestion.Answers.Find(i => i.Number.Equals(0));
                    var secondAnswer = firstQuestion.Answers.Find(i => i.Number.Equals(1));
                    var threesAnswer = firstQuestion.Answers.Find(i => i.Number.Equals(2));
                    var foursAnswer = firstQuestion.Answers.Find(i => i.Number.Equals(3));

                    var qm = new QuestionMarkup();

                    var markup = qm.CreateQuestionMarkup(firstAnswer.QuestionAnswer,
                        secondAnswer.QuestionAnswer, threesAnswer.QuestionAnswer, foursAnswer.QuestionAnswer);
                    if (markup == null)
                        throw new Exception("нет объекта");

                    var callBack = new CallbackQuery();
                    var idCallBack = callBack.Id;
                    
                    await telegramBotClient.AnswerCallbackQueryAsync(idCallBack, text: update.CallbackQuery.Data, showAlert: false);
                    await telegramBotClient.SendTextMessageAsync(chatId: "1052886210", questionDiscript, replyMarkup: markup);
                    return;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ContinueTest(ITelegramBotClient telegramBotClient, Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {              
                var message = update.Message.Text;
                var chatId = update.Message.Chat.Id;
                var foundAnswer = await _answerRepository.FindAsync(message);               

                var foundUser = _userRepository.FindByChatId(chatId);
                var founfLastQuestion = _questionRepository.FindLast(foundAnswer.Question.Number);
                if (founfLastQuestion != null)
                {
                    var grade = foundUser.Grade.GradeStatus;
                    await telegramBotClient.SendTextMessageAsync(chatId: "1052886210", $"Your result is {grade}");
                    return;
                }

                if (foundAnswer.AnswerStatus == Models.Status.True)
                {
                    var foundGrade = _gradeRepository.FindById(foundUser.Id);
                    var statusGrade = foundGrade.GradeStatus + 1;                  
                    var newGrade = _gradeRepository.Edit(foundGrade);
                }

                var nextNumberQuestion = foundAnswer.Question.Number + 1;
                var nextQuestion = await _questionRepository.FindByNumberAsync(nextNumberQuestion);
                var questionDiscript = nextQuestion.Discription;

                var firstAnswer = nextQuestion.Answers.Find(i => i.Number.Equals(0));
                var secondAnswer = nextQuestion.Answers.Find(i => i.Number.Equals(1));
                var threesAnswer = nextQuestion.Answers.Find(i => i.Number.Equals(2));
                var foursAnswer = nextQuestion.Answers.Find(i => i.Number.Equals(3));
                        

                var qm = new QuestionMarkup();
                var markup = qm.CreateQuestionMarkup(firstAnswer.QuestionAnswer,
                    secondAnswer.QuestionAnswer, threesAnswer.QuestionAnswer, foursAnswer.QuestionAnswer);
               
                await telegramBotClient.AnswerCallbackQueryAsync(null, text: update.CallbackQuery.Data, showAlert: false);
                await telegramBotClient.SendTextMessageAsync(chatId: "1052886210", questionDiscript, replyMarkup: markup);
                    return;

            }
        }
    }
}
