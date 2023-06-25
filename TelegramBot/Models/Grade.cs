using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Grade
    {
        public Guid Id { get; set; }
        public int GradeStatus { get; set; }
        public int QuestionNumber { get; set; }
        public User User { get; set; }

    }
}
