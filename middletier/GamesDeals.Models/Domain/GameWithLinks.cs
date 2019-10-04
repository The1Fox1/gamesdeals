using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Models.Domain
{
    public class GameWithLinks : BaseGame
    {
        public GameLinks Links { get; set; }
    }
}
