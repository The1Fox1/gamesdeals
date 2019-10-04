using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Models.Requests
{
    public class GameAddRequest
    {
        public string Plain { get; set; }

        public string AppId { get; set; }

        public string Shop { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public float RetailPrice { get; set; }

        public float SalePrice { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}
