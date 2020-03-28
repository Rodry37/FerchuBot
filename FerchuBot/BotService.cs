using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot
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
                var command = message.Substring(2).Split(' ').First();

                switch (command.ToLowerInvariant())
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
            var roll = socketMessage.Content.Split(' ')[1];
            try
            {
                var splitDice = roll.Split('d');
                var numberOfDice = int.Parse(splitDice.First());

                splitDice = splitDice[1].Split(',');
                DiceType diceType = (DiceType)Enum.Parse(typeof(DiceType), "d"+splitDice[0]);
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
    }
}
