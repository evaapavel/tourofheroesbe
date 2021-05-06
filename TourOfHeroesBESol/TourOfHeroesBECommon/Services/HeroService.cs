using System;
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



        public IList<Hero> FindListOfHeroes(string namePattern)
        {
            Hero searchCriteriaAsHero = new Hero { Name = $"*{namePattern}*" };
            //Hero searchCriteriaAsHero = new Hero { Name = namePattern };
            return this.heroRepo.FindList(searchCriteriaAsHero);
        }



        public IList<Hero> SearchHeroes(Hero hero)
        {
            if ( (hero.ID == 0) && (hero.Name == null) )
            {
                // Empty hero-search criteria.
                return GetListOfHeroes();
            }
            if ((hero.ID == 0) && (hero.Name != null))
            {
                // Only the Name property has been set.
                return FindListOfHeroes(hero.Name);
            }
            return this.heroRepo.FindList(hero);
        }



        public bool ExistsHero(int id)
        {
            Hero heroToCheck = new Hero { ID = id };
            bool exists = this.heroRepo.Exists(heroToCheck);
            return exists;
        }



        public Hero LoadHero(int id)
        {
            Hero heroToLoad = new Hero { ID = id };
            Hero heroLoaded = this.heroRepo.Load(heroToLoad);
            return heroLoaded;
        }



        //public void SaveHero(Hero hero)
        public Hero SaveHero(Hero hero)
        {
            Hero heroToReturn = hero;
            if (hero.ID == 0)
            {
                this.heroRepo.Add(hero);
                // Find all heroes with the given name.
                List<Hero> listOfHeroesToProcess = (List<Hero>) this.heroRepo.FindList(hero);
                // Sort the list of heroes by their ID's in an ascending order.
                listOfHeroesToProcess.Sort((h1, h2) => h1.ID - h2.ID);
                // Get the last hero (with the greatest ID).
                //heroToReturn = listOfHeroesToProcess[0];
                heroToReturn = listOfHeroesToProcess[listOfHeroesToProcess.Count - 1];
            }
            else
            {
                this.heroRepo.Store(hero);
            }
            return heroToReturn;
        }



        //public void DeleteHero(int id)
        public Hero DeleteHero(int id)
        {
            Hero heroToDelete = new Hero { ID = id };
            Hero heroToDeleteFound = this.heroRepo.Load(heroToDelete);
            this.heroRepo.Remove(heroToDelete);
            return heroToDeleteFound;
        }



    }



}
