using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TelegramBot.Models;

namespace TelegramBot
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string QuestionAnswer { get; set; }
        public int Number { get; set; }
        public Status AnswerStatus { get; set; }
        public QuestionDiscription Question { get; set; }
    }
}
