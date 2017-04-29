using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TranslateBot.Interfaces;

namespace TranslateBot.Modules
{
    public class TranslateModule : ModuleBase
    {
        ITranslateService service;
        public TranslateModule(ITranslateService service)
        {
            this.service = service;
        }

        [Command("translate"), Summary("-translate <text to translate>")]
        public async Task Translate([Remainder, Summary("Text to translate.")] string text)
        {
            var translation = await service.Translate(text);
            await ReplyAsync(translation);

            /*var client = new HttpClient();
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
            }*/

        }
    }
}
