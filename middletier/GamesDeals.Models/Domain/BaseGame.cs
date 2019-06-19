using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Models.Domain
{
    public class BaseGame
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public float RetailPrice { get; set; }

        public float SalePrice { get; set; }

        public string Image { get; set; }
    }
}
