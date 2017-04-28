using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Text;
using System.Threading.Tasks;

namespace TranslateBot.Modules
{
    public class InfoModule : ModuleBase
    {
        CommandService commandService;
        DiscordSocketClient client;

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
                helpBuilder.Append($"!{command.Name} -- Usage: {command.Summary}\n");
            }

            builder.WithDescription(helpBuilder.ToString());
            await ReplyAsync("", embed: builder);
        }
    }
}
