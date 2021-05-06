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



        // REST API path: GET /api/heroes
        //public IEnumerable<Hero> Get()
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Hero> heroes = _heroService.GetListOfHeroes();
            // HTTP status code: 200 (OK)
            return Ok(heroes);
            //return heroes;
        }



        //// REST API path: GET /api/heroes
        //[HttpGet]
        //public async Task<IActionResult> GetList()
        //{
        //    IEnumerable<Hero> heroes = _heroService.GetListOfHeroes();
        //    // HTTP status code: 200 (OK)
        //    return Ok(heroes);
        //}



        // REST API path: GET /api/heroes/11
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
            // return hero;
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



        // REST API path: PUT /api/heroes
        [HttpPut]
        public IActionResult Put(Hero hero)
        {
            _logger.LogInformation(hero.ToString());

            // Is there a hero with the given ID?
            bool exists = _heroService.ExistsHero(hero.ID);
            if ( ! exists )
            {
                // HTTP status code: 404 (Not Found)
                return NotFound(hero);
            }

            // Update the hero.
            _heroService.SaveHero(hero);

            // REST API recommends either a status code of 200 (OK) or 204 (No Content) to be returned.
            // HTTP status code: 200 (OK)
            //return Ok(hero);
            // HTTP status code: 204 (No Content)
            return NoContent();
        }



        ////[HttpPut]
        ////[Route("{id}")]
        //// REST API path: PUT /api/heroes/11
        ////[HttpPut("{id}")]
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Store(int id, [FromForm] Hero hero)
        //{
        //    _logger.LogInformation(hero.ToString());

        //    //if ( (hero.ID != 0) && (id != hero.ID) )
        //    //{
        //    //    throw new InvalidOperationException($"URI id ({id}) does not correpond to hero ID ({hero.ID}).");
        //    //}
        //    //if (hero.ID == 0)
        //    //{
        //    //    hero.ID = id;
        //    //}

        //    //if ((hero.ID != 0) && (id != hero.ID))
        //    //{
        //    //    throw new InvalidOperationException($"URI id ({id}) does not correpond to hero ID ({hero.ID}).");
        //    //}
        //    //if (hero.ID == 0)
        //    //{
        //    //    hero.ID = id;
        //    //}

        //    if (id != hero.ID)
        //    {
        //        throw new InvalidOperationException($"URI id ({id}) does not correpond to hero ID ({hero.ID}).");
        //    }
        //    // Is there a hero with the given ID?
        //    bool exists = _heroService.ExistsHero(id);
        //    if (!exists)
        //    {
        //        // HTTP status code: 404 (Not Found)
        //        return NotFound(hero);
        //    }

        //    // REST API recommends either a status code of 200 (OK) or 204 (No Content) to be returned.
        //    // HTTP status code: 200 (OK)
        //    //return Ok(hero);
        //    // HTTP status code: 204 (No Content)
        //    return NoContent();
        //}



    }



}
