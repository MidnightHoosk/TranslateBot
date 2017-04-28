using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TranslateBot.Modules
{
    public class TranslateModule : ModuleBase
    {
        [Command("translate"), Summary("!translate <text to translate>")]
        public async Task Translate([Remainder, Summary("Text to translate.")] string text)
        {
            var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri($"https://translate.yandex.net/api/v1.5/tr.json/translate?lang=en&key={Environment.GetEnvironmentVariable("translatekey")}");
                // HttpContent content = new StringContent(text);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("text", text)
                });

                var response = await client.PostAsync("", content);
                string result = await response.Content.ReadAsStringAsync();
                var translation = JsonConvert.DeserializeObject<TranslateResponse>(result).text[0];

                translation = translation.Replace("\"", "");
                await ReplyAsync(translation);

            } catch (HttpRequestException e)
            {
                await ReplyAsync("Error");
                Console.WriteLine(e.Message);
            }
        }

        [Command("random"), Summary("!random: displays a random number between 1 and 100")]
        async Task Random()
        {
            Random rng = new Random();
            int random = rng.Next(0, 101);
            await ReplyAsync($"Random number between 1 and 100: {random}");
        }
    }
    class TranslateResponse
    {
        public int code;
        public string lang;
        public string[] text;
    }
}
