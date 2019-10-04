using System;

namespace GamesDeals.Models.Domain
{
    public class SteamGame : BaseGame
    {
        public string Url { get; set; }

        public int Rating { get; set; }

        public string Image { get; set; }

        public string AppId { get; set; }

    }
}
