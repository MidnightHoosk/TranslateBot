using Discord.WebSocket;
using System.Threading.Tasks;

namespace TranslateBot.Interfaces
{
    public interface ICommands
    {
        Task Install();
        Task HandleCommand(SocketMessage messageParam);
    }
}
