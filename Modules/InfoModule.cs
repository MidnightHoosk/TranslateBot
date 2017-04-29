using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TranslateBot.Modules
{
    public class InfoModule : ModuleBase
    {
        CommandService commandService;

        public InfoModule(CommandService commandService)
        {
            this.commandService = commandService;
        }

        [Command("help"), Summary("Shows help menu.")]
        public async Task Help()
        {
            StringBuilder helpBuilder = new StringBuilder();

            EmbedBuilder builder = new EmbedBuilder
            {
                Title = "Commands",
                Color = new Color(51, 153, 255)
            };

            foreach (var command in commandService.Commands)
            {
                helpBuilder.Append($"-{command.Name} -- Usage: {command.Summary}\n");
            }

            builder.WithDescription(helpBuilder.ToString());

            var channel = await Context.User.CreateDMChannelAsync();

            await channel.SendMessageAsync("", embed: builder);

            if (!Context.IsPrivate)
            {
                await ReplyAsync($"{Context.User.Mention} Check your direct messages!");
            }
        }

        [Command("servers"), Summary("Show number of servers installed on.")]
        public async Task Servers()
        {
            var servers = Context.Client.GetGuildsAsync().Result.Count;

            await ReplyAsync($"Server(s) Count: {servers}");
        }


        [Command("invite"), Summary("Invite me to your server!")]
        public async Task Invite()
        {
            var inviteLink = "https://discordapp.com/api/oauth2/authorize?client_id=306182556515172353&scope=bot";

            var channel = await Context.User.CreateDMChannelAsync();
            await channel.SendMessageAsync($"Invite me to your server! {inviteLink}");
        }
    }
}
