using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace TranslateBot.Modules
{
    public class RandomModule : ModuleBase
    {
        [Command("random"), Summary("-random: displays a random number between 1 and 100")]
        async Task Random()
        {
            Random rng = new Random();
            int random = rng.Next(0, 101);
            await ReplyAsync($"Random number between 1 and 100: {random}");
        }
    }
}
