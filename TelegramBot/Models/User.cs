using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Grade Grade { get; set; }

        public long ChatId { get; set; }
    }
}
