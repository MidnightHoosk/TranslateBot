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
        [Command("translate"), Summary("-translate <text to translate>")]
        public async Task Translate([Remainder, Summary("Text to translate.")] string text)
        {
            string translation = await TranslateApi(text);
            await ReplyAsync(translation);
        }

        async Task<string> TranslateApi(string textToTranslate, string toLanguage = "")
        {
            string lang = (String.IsNullOrWhiteSpace(toLanguage)) ? "en" : "en-" + toLanguage;

            var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri($"https://translate.yandex.net/api/v1.5/tr.json/translate?lang={lang}&key={Environment.GetEnvironmentVariable("translatekey")}");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("text", textToTranslate)
                });

                var response = await client.PostAsync("", content);
                string result = await response.Content.ReadAsStringAsync();
                var translation = JsonConvert.DeserializeObject<TranslateResponse>(result).text[0];

                return translation.Replace("\"", "");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
    class TranslateResponse
    {
        public int code;
        public string lang;
        public string[] text;
    }
}
