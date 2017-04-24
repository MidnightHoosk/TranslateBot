using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TranslateBot.Interfaces
{
    class Commands : ICommands
    {
        private readonly IDependencyMap _map;
        private CommandService commands;

        public Commands(IDependencyMap map)
        {
            _map = map;
        }

        public async Task Install()
        {

            commands = new CommandService();
            
            Console.WriteLine("Installing Commands...");
            _map.Add(commands);

            var client = _map.Get<DiscordSocketClient>();

            client.MessageReceived += HandleCommand;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            var client = _map.Get<DiscordSocketClient>();
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, _map);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
