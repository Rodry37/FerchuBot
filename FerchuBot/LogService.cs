using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot
{
    public class LogService
    {
        public Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
