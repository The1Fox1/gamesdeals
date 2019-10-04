using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Models.Requests
{
    public class GameUpdateRequest : GameAddRequest
    {
        public int Id { get; set; }
    }
}
