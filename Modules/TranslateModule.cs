using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TranslateBot.Modules
{
    public class TranslateModule : ModuleBase
    {
        [Command("translate"), Summary("Translates using Google Language Auto-detect.")]
        public async Task Translate([Remainder, Summary("Text to translate.")] string text)
        {

            await ReplyAsync(text);
        }

        [Command("random"), Summary("Displays a random number between 1 and 100.")]
        async Task Random()
        {
            Random rng = new Random();
            int random = rng.Next(0, 101);
            await ReplyAsync($"Random number between 1 and 100: {random}");
        }
    }
}
