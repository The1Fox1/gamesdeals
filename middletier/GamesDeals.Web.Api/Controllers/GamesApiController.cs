using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesDeals.Models.Domain;
using GamesDeals.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace GamesDeals.Web.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesApiController : BaseApiController
    {
        private ILogger _logger;
        private IGameServices _gamesServices;

        public GamesApiController(ILogger<GamesApiController> logger, IGameServices gamesServices) : base(logger)
        {
            _logger = logger;
            _gamesServices = gamesServices;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<GameWithLinks>> GetById(int id)
        {
            try
            {
                GameWithLinks item = _gamesServices.GetById(id);
                if (item == null)
                {
                    return NotFound404(new ErrorResponse("Record Not Found"));
                }
                else
                {
                    ItemResponse<GameWithLinks> response = new ItemResponse<GameWithLinks>();
                    response.Item = item;
                    return Ok200(response);
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