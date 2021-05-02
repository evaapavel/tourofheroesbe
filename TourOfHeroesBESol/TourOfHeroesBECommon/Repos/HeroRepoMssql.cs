using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using TourOfHeroesBECommon.BusinessObjects;
using TourOfHeroesBECommon.EF;



namespace TourOfHeroesBECommon.Repos
{



    public class HeroRepoMssql
    {



        public IList<Hero> GetList()
        {
            using (var context = new MssqlContext())
            {

                var query = from h in context.Heroes
                            select h;

                var heroes = query.ToList<Hero>();
                return heroes;

            }
        }



        public IList<Hero> FindList(Hero hero)
        {
            using (var context = new MssqlContext())
            {

                //var query = from h in context.Heroes
                //        where h.Name == hero.Name
                //        select h;

                IQueryable<Hero> query = BuildQuery(context.Heroes, hero);

                var heroes = query.ToList<Hero>();
                return heroes;

            }
        }



        public bool Exists(Hero hero)
        {
            using (var context = new MssqlContext())
            {

                //var query = from h in context.Heroes
                //        where h.Name == hero.Name
                //        select h;

                IQueryable<Hero> query = BuildQuery(context.Heroes, hero);

                var exists = query.Any<Hero>();
                return exists;

            }
        }



        public Hero Load(Hero hero)
        {
            if ( ! Exists(hero) )
            {
                throw new Exception($"There's no such hero with ID: {hero.ID}");
            }
            using (var context = new MssqlContext())
            {

                return context.Heroes.Find(hero.ID);

            }
        }



        public void Store(Hero hero)
        {
            using (var context = new MssqlContext())
            {

                context.Entry(hero).State = ((hero.ID == 0) ? (EntityState.Added) : (EntityState.Modified));

                context.SaveChanges();

            }
        }



        public void Add(Hero hero)
        {
            using (var context = new MssqlContext())
            {

                context.Heroes.Add(hero);

                context.SaveChanges();

            }
        }



        public void Remove(Hero hero)
        {
            using (var context = new MssqlContext())
            {

                context.Entry(hero).State = EntityState.Deleted;

                context.SaveChanges();

            }
        }



        private IQueryable<Hero> BuildQuery(IQueryable<Hero> query, Hero hero)
        {

            if (hero.ID != 0)
            {
                query = query.Where(h => h.ID == hero.ID);
            }
            if (hero.Name != null)
            {
                query = query.Where(h => h.Name == hero.Name);
            }
            // ...

            return query;

        }



    }



}
