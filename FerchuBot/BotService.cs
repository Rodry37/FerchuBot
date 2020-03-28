using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot
{
    public class BotService
    {
        private const string COMMAND_CHAR = ">>";
        public async Task HandleMessage(SocketMessage socketMessage)
        {
            var message = socketMessage.Content;

            if (message.Length > 2 && message.StartsWith(COMMAND_CHAR))
            {
                message = message.Substring(2);

                switch (message.ToLowerInvariant())
                {
                    case BotCommands.PING:
                        await Ping(socketMessage);
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task Ping(SocketMessage socketMessage)
        {
            Console.WriteLine("Ping recieved by " + socketMessage.Author.Username + " in channel " + socketMessage.Channel.Name);
            await socketMessage.Channel.SendMessageAsync("Pong!");
        }
    }
}
