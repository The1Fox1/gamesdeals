using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GamesDeals.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace GamesDeals.Web.Api.Controllers
{
    [Route("api/ITAD")]
    [ApiController]
    public class ITADController : BaseApiController
    {
        private ILogger _logger;

        public ITADController(ILogger<ITADController> logger /* , IGameServices gamesServices*/) : base(logger)
        {
            _logger = logger;
            //_gamesServices = gamesServices;
        }

        private string ITADbaseUrl = "https://api.isthereanydeal.com";
        private string ITADKey = "d604f8dd0a126041cd957c04ac413711d652f580";


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<GameWithLinks>> GetPlainById(int id)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ITADbaseUrl);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("token", "xxxxxxxxxxxxxxxxxx");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(0));

                string result = streamReader.ReadToEnd();
                //Response.Write(result);

                if (result == null)
                {
                    return NotFound404(new ErrorResponse("Record Not Found"));
                }
                else
                {
                    ItemResponse<GameWithLinks> iResponse = new ItemResponse<GameWithLinks>();
                   //iResponse.Item = result;
                    return Ok200(iResponse);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }
    }
}