using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesDeals.Web.Api.Controllers
{
    [Route("api/testing")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public ActionResult<int> Get()
        {
            return 5;
        }

    }
}