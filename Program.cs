using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TranslateBot.Interfaces;

namespace TranslateBot
{
    class Program
    {
        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        private CommandService commands;
        private DiscordSocketClient client;
        private DependencyMap map;

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            client.Log += Log;

            string token = Environment.GetEnvironmentVariable("Token");

            map = new DependencyMap();

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task InstallCommands()
        {
            client.MessageReceived += HandleCommand;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, map);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
