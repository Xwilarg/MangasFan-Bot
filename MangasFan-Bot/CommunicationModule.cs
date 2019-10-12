using Discord;
using Discord.Commands;
using DiscordUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MangasFan_Bot
{
    public class CommunicationModule : ModuleBase
    {
        [Command("Info")]
        public async Task Info(params string[] _)
        {
            await ReplyAsync("", false, Utils.GetBotInfo(Program.P.StartTime, "MangasFan-Bot", Program.P.client.CurrentUser));
        }

        [Command("User")]
        public async Task User(params string[] args)
        {
            IUser user = await Utils.GetUser(string.Join(" ", args), Context.Guild);
            if (user == null)
            {
                await ReplyAsync("There is no user with this name");
                return;
            }
            dynamic json;
            using (HttpClient hc = new HttpClient())
                json = JsonConvert.DeserializeObject(await hc.GetStringAsync("https://www.mangasfan.fr/api_discord.php?name=" + Uri.EscapeDataString(user.Username) + "&id=" + user.Discriminator));
            if (json == null)
            {
                await ReplyAsync("This user didn't link his Mangas'Fan and Discord account.");
                return;
            }
            await ReplyAsync("", false, new EmbedBuilder
            {
                Title = json.username,
                Description = json.description,
                Color = Color.Blue,
                ImageUrl = "https://www.mangasfan.fr/membres/images/avatars/" + json.avatar,
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder
                    {
                        IsInline = true,
                        Name = "Favorite Anime",
                        Value = json.anime
                    },
                    new EmbedFieldBuilder
                    {
                        IsInline = true,
                        Name = "Favorite Manga",
                        Value = json.manga
                    }
                }
            }.Build());
        }
    }
}
