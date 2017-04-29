using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TranslateBot.Interfaces
{
    public interface ITranslateService
    {
        Task<string> Translate(string textToTranslate, string toLanguage = null);
    }
}
