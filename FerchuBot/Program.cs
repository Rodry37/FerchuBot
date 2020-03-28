using Discord;
using Discord.WebSocket;
using FerchuBot.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FerchuBot
{
    class Program
    {
        private LogService logger;
        private BotService bot;
        private DiscordSocketClient client;
        private SecretsService secrets;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            InitializeServices();

            client.Log += logger.Log;

            var token = secrets.GetToken();

            await Login(token);

            client.MessageReceived += bot.HandleMessage;

            await Task.Delay(-1);
        }

        private async Task Login(string token)
        {
            try
            {
                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Login: " + ex.Message);
                throw;
            }
        }

        private void InitializeServices()
        {
            client = new DiscordSocketClient();
            bot = new BotService();
            logger = new LogService();
            secrets = new SecretsService();
        }

    }
}
