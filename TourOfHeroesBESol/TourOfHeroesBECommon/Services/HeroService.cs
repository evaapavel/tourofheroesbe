﻿using System;
using System.Collections.Generic;

using TourOfHeroesBECommon.BusinessObjects;
using TourOfHeroesBECommon.Repos;



namespace TourOfHeroesBECommon.Services
{



    public class HeroService
    {



        private HeroRepoMssql heroRepo;



        public HeroService()
        {
            // Temporary solution.
            this.heroRepo = new HeroRepoMssql();
        }



        public IList<Hero> GetListOfHeroes()
        {
            return this.heroRepo.GetList();
        }



        public void SaveHero(Hero hero)
        {
            if (hero.ID == 0)
            {
                this.heroRepo.Add(hero);
            }
            else
            {
                this.heroRepo.Store(hero);
            }
        }



        public void DeleteHero(int id)
        {
            Hero heroToDelete = new Hero { ID = id };
            this.heroRepo.Remove(heroToDelete);
        }



    }



}