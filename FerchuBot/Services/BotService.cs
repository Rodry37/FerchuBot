using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot.Services
{
    public class BotService
    {
        private DiceService diceService;
        public BotService()
        {
            diceService = new DiceService();
        }

        private const string COMMAND_CHAR = ">>";
        public async Task HandleMessage(SocketMessage socketMessage)
        {
            var message = socketMessage.Content;

            if (message.Length > 2 && message.StartsWith(COMMAND_CHAR))
            {
                string command = GetCommandFromMessage(message);

                switch (command.ToLowerInvariant().Trim())
                {
                    case BotCommands.PING:
                        await Ping(socketMessage);
                        break;

                    case BotCommands.ROLL:
                        await RollDice(socketMessage);
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task RollDice(SocketMessage socketMessage)
        {
            var roll = GetParametersFromMessage(socketMessage.Content);
            try
            {
                var splitDice = roll.Split('d');
                var numberOfDice = int.Parse(splitDice.First());

                splitDice = splitDice[1].Split(',');
                DiceType diceType = (DiceType)Enum.Parse(typeof(DiceType), "d" + splitDice[0]);
                int modifier = 0;
                if (splitDice.Length > 1)
                {
                    modifier = int.Parse(splitDice[1]);
                }

                var result = await diceService.RollTheDice(numberOfDice, diceType, modifier);

                await socketMessage.Channel.SendMessageAsync("Result is: " + result.ToString());
            }
            catch (Exception)
            {
                await socketMessage.Channel.SendMessageAsync("Error with roll format use >>roll [numOfDice]d[dice],[modifier]\nExample: 2d6,+2");
            }
        }

        private async Task Ping(SocketMessage socketMessage)
        {
            Console.WriteLine("Ping recieved by " + socketMessage.Author.Username + " in channel " + socketMessage.Channel.Name);
            await socketMessage.Channel.SendMessageAsync("Pong!");
        }

        private string GetCommandFromMessage(string message)
        {
            return message.Substring(2).TrimStart().Split(' ').First();
        }

        private string GetParametersFromMessage(string message)
        {
            var strList = message
                .Substring(2)
                .TrimStart()
                .Split(' ')
                .ToList();

            // Remove command from query
            strList.RemoveAt(0);

            return string.Join(" ", strList).Trim();
        }
    }
}
