using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Models.Domain
{
    public class BaseGame
    {
        public int Id { get; set; }

        public string Plain { get; set; }

        public string Title { get; set; }

        public float RetailPrice { get; set; }

        public float SalePrice { get; set; }

       // public int PriceCut { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
