using Discord.Commands;
using DiscordUtils;
using System.Threading.Tasks;

namespace MangasFan_Bot
{
    public class CommunicationModule : ModuleBase
    {
        [Command("Info")]
        public async Task Info()
        {
            await ReplyAsync("", false, Utils.GetBotInfo(Program.P.StartTime, "MangasFan-Bot", Program.P.client.CurrentUser));
        }
    }
}
