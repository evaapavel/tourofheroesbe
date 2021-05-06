using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Linq.SqlClient;

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
                //query = query.Where(h => h.Name == hero.Name);
                string name = hero.Name;
                //if ( ! name.Contains('*') )
                //{
                //    query = query.Where(h => h.Name == name);
                //}
                //else
                //{
                //    // For search terms like 'Ma*', replace '*' with '%' and use LIKE (e.g. WHERE NAME LIKE 'Ma%').
                //    //name = name.Replace('*', '%');
                //    //query = query.Where(h => SqlMethods.Like(h.Name, name));
                //}
                int countStars = name.Count(c => c == '*');
                switch (countStars)
                {
                    case 0:
                        // No asterisks (wildcards) at all.
                        query = query.Where(h => h.Name == name);
                        break;
                    case 1:
                        // One asterisk.
                        // One asterisk may be at the beginning, in the middle or at the end of the search term.
                        if (name.Length > 1)
                        {
                            // Expect one non-asterisk character at least.
                            if (name[0] == '*')
                            {
                                // Wildcard at the beginning of the search term.
                                // WHERE NAME LIKE '%Ma'
                                string term = name.Substring(1);
                                query = query.Where(h => h.Name.EndsWith(term));
                                //query = query.Where(h => h.Name.EndsWith(term, StringComparison.OrdinalIgnoreCase));
                            }
                            else if (name[name.Length - 1] == '*')
                            {
                                // Wildcard at the end of the search term.
                                // WHERE NAME LIKE 'Ma%'
                                string term = name.Substring(0, name.Length - 1);
                                query = query.Where(h => h.Name.StartsWith(term));
                                //query = query.Where(h => h.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase));
                            }
                            else
                            {
                                // Wildcard in the middle of the search term.
                                // WHERE NAME LIKE 'Ma%ta'
                                // There must be at least 3 characters in such a string.
                                if (name.Length < 3)
                                {
                                    // This should never happen.
                                    throw new Exception($"This situation is not expected. The search term: {name}");
                                }
                                string[] terms = name.Split('*');
                                query = query.Where(h => h.Name.StartsWith(terms[0]) && h.Name.EndsWith(terms[1]));
                                //query = query.Where(h => h.Name.StartsWith(terms[0], StringComparison.OrdinalIgnoreCase) && h.Name.EndsWith(terms[1], StringComparison.OrdinalIgnoreCase));
                            }
                        }
                        break;
                    case 2:
                        // In case of two asterisks, we expect only this: *ma*. No other variants are allowed.
                        if ( ! ((name.IndexOf('*') == 0) && (name.LastIndexOf('*') == name.Length - 1)) )
                        {
                            throw new NotSupportedException($"This search term is not supported: {name}");
                        }
                        if (name.Length > 2)
                        {
                            // Expect one non-asterisk character at least.
                            // WHERE NAME LIKE '%Ma%'
                            string term = name.Substring(1, name.Length - 2);
                            query = query.Where(h => h.Name.Contains(term));
                            //query = query.Where(h => h.Name.Contains(term, StringComparison.OrdinalIgnoreCase));
                        }
                        break;
                    default:
                        throw new NotSupportedException($"This search term is not supported: {name}");
                }
            }
            // ...

            return query;

        }



    }



}
