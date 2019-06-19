using System;

namespace GamesDeals.Models.Domain
{
    public class SteamGame : BaseGame
    {
        public DateTime? ReleaseDate { get; set; }

        public int Rating { get; set; }

        public string AppId { get; set; }
    }
}
