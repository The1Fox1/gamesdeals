using GamesDeals.Models.Domain;
using GamesDeals.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesDeals.Services.Interfaces
{
    public interface IGameServices
    {
        int Insert(GameAddRequest model);

        GameWithLinks GetById(int Id);

        void Update(GameUpdateRequest model);

        void Delete(int Id);
    }
}
