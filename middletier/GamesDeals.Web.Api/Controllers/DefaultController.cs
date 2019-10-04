using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesDeals.Models.Domain;
using GamesDeals.Services.Scraper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace GamesDeals.Web.Api.Controllers
{
    [Route("api/default")]
    [ApiController]
    public class DefaultController : BaseApiController
    {

        private ILogger _logger;
        //private IDefaultServices _defaultServices;

        public DefaultController(
            ILogger<DefaultController> logger 
            //,IDefaultServices defaultServices
            ) : base(logger)
        {
            _logger = logger;
           // _defaultServices = defaultServices;
        }

        [HttpGet ("SteamTop")]
        public ActionResult<ItemsResponse<SteamGame>> GetTopSell()
        {
            try
            {
                string outputFilePath = "C:/SF.Code/gamedeals/middletier/GamesDeals.Services/Scraper/Output/SteamScraper.Jsonl";
                List<SteamGame> items = new List<SteamGame>();
                SteamScraper scraper = new SteamScraper();

                System.IO.File.WriteAllText(@outputFilePath, string.Empty);

                scraper.Start();

                ItemsResponse<SteamGame> response = new ItemsResponse<SteamGame>();

                using (StreamReader reader = new StreamReader(outputFilePath))
                {
                    string json = reader.ReadToEnd();
                    response.Items = JsonConvert.DeserializeObject<List<SteamGame>>(json);
                }

                return Ok200(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return StatusCode(500, new ErrorResponse(ex.Message));

            }
            
        }

    }
}