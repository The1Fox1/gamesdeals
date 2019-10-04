using GamesDeals.Models.Requests;
using Sabio.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GamesDeals.Services.Services
{
    class ITADServices
    {
        private IDataProvider _dataProvider;

        public ITADServices(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


    }
}
