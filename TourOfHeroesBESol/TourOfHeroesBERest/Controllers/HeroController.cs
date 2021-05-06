using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TourOfHeroesBECommon.BusinessObjects;
using TourOfHeroesBECommon.Services;
using TourOfHeroesBERest.Attributes;

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
        // REST API path: GET /api/heroes/?name=Ma
        //public IEnumerable<Hero> Get()
        [HttpGet]
        public IActionResult Get([FromQuery] Hero hero)
        {
            IEnumerable<Hero> heroes = _heroService.SearchHeroes(hero);
            // HTTP status code: 200 (OK)
            return Ok(heroes);
            //return heroes;
        }



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



        // REST API path: PUT /api/heroes
        // Data is in the request body in JSON format.
        // Therefore, we have an HTTP header of "Content-Type", with a value of "application/json".
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



        // REST API path: POST /api/heroes
        // Data is in the request body in JSON format.
        // Therefore, we have an HTTP header of "Content-Type", with a value of "application/json".
        [HttpPost]
        public IActionResult Post(Hero hero)
        {
            _logger.LogInformation(hero.ToString());

            // Add a new hero.
            Hero newHero = _heroService.SaveHero(hero);

            // HTTP status code: 201 (Created)
            return Created(this.Request.Path, newHero);
        }



        // REST API path: DELETE /api/heroes/31
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(id.ToString());

            // Is there a hero with the given ID?
            bool exists = _heroService.ExistsHero(id);
            if (!exists)
            {
                // HTTP status code: 404 (Not Found)
                return NotFound(new { id = id });
            }

            // Delete the hero.
            Hero heroDeleted = _heroService.DeleteHero(id);

            // HTTP status code: 200 (OK)
            return Ok(heroDeleted);
            //return Ok(new { id = heroDeleted.ID });
        }



    }



}
