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



        //public Hero Get(int id)
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Hero hero = new Hero { ID = id };
            try
            {
                hero = _heroService.LoadHero(id);
            }
            catch (Exception)
            {
                // No such hero (non-existing ID).
                // HTTP status code: 404 (Not Found)
                return NotFound(new { id = hero.ID });
            }
            // HTTP status code: 200 (OK)
            return Ok(hero);
        }



        //// REST API path: GET /api/heroes/11
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> Load(int id)
        //{
        //    Hero hero = new Hero { ID = id };
        //    try
        //    {
        //        hero = _heroService.LoadHero(id);
        //    }
        //    //catch (Exception ex)
        //    catch(Exception)
        //    {
        //        // No such hero (non-existing ID).
        //        // HTTP status code: 404 (Not Found)
        //        //return NotFound();
        //        //return NotFound(hero);
        //        return NotFound(new { id = hero.ID });
        //    }
        //    // HTTP status code: 200 (OK)
        //    return Ok(hero);
        //}

    }



}
