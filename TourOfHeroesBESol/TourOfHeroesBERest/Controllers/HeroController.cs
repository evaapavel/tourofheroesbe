using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TourOfHeroesBECommon.BusinessObjects;
using TourOfHeroesBECommon.Services;



namespace TourOfHeroesBERest.Controllers
{



    //[Route("api/heroes")]
    [ApiController]
    [Route("/api/heroes")]
    public class HeroController : ControllerBase
    {



        private readonly ILogger<HeroController> _logger;

        private readonly HeroService _heroService;



        public HeroController(ILogger<HeroController> logger)
        {
            _logger = logger;
            // Temporary solution
            _heroService = new HeroService();
        }



        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            IEnumerable<Hero> heroes = _heroService.GetListOfHeroes();
            //return heroes.ToArray<Hero[]>();
            return heroes.ToArray<Hero>();
            //return heroes.ToArray();
        }



        //// REST API path: GET /api/heroes
        //[HttpGet]
        //public async Task<IActionResult> GetList()
        //{
        //    IEnumerable<Hero> heroes = _heroService.GetListOfHeroes();
        //    // HTTP status code: 200 (OK)
        //    return Ok(heroes);
        //}



    }



}
