using System;
using System.Collections.Generic;
using System.Linq;

using TourOfHeroesBECommon.BusinessObjects;
using TourOfHeroesBECommon.Repos;



namespace TourOfHeroesBEMain
{



    public class EFMssqlTest
    {



        private string id13originalName = "Bombasto";



        private HeroRepoMssql heroRepo;



        public EFMssqlTest()
        {
            this.heroRepo = new HeroRepoMssql();
        }



        public void GetListTest()
        {
            IList<Hero> heroes = this.heroRepo.GetList();
            foreach (Hero hero in heroes)
            {
                Console.WriteLine($"ID: {hero.ID}    Name: {hero.Name}");
            }
        }



        public void AddTest()
        {
            Hero deadpool = new Hero { Name = "Deadpool" };
            Hero antman = new Hero { Name = "Ant-Man" };
            Hero gamora = new Hero { Name = "Gamora" };
            if ( ! this.heroRepo.Exists(deadpool) )
            {
                this.heroRepo.Add(deadpool);
            }
            if ( ! this.heroRepo.Exists(antman) )
            {
                this.heroRepo.Add(antman);
            }
            if ( ! this.heroRepo.Exists(gamora) )
            {
                this.heroRepo.Add(gamora);
            }
            //GetListTest();
        }



        public void RemoveTest()
        {
            AddTest();
            Hero anti = new Hero { Name = "Anti" };
            if ( ! this.heroRepo.Exists(anti))
            {
                this.heroRepo.Add(anti);
                Console.WriteLine("Hero Anti added.");
            }
            //GetListTest();
            if (this.heroRepo.Exists(anti))
            {
                this.heroRepo.Remove(anti);
                Console.WriteLine("Hero Anti removed.");
            }
            //GetListTest();
        }



        public void StoreTest()
        {
            AddTest();
            Hero id13obj1 = new Hero { ID = 13 };
            Hero id13obj2 = this.heroRepo.Load(id13obj1);
            id13obj2.Name = "Otsabmob";
            this.heroRepo.Store(id13obj2);
            Hero id13obj3 = this.heroRepo.Load(id13obj1);
            id13obj3.Name = id13originalName;
            this.heroRepo.Store(id13obj3);
        }



        public void LaunchTestSuite()
        {

            // Display the contents of the hero repo.
            this.GetListTest();
            Console.WriteLine();
            Console.WriteLine();

            // Try to add heroes.
            this.AddTest();
            this.GetListTest();
            Console.WriteLine();
            Console.WriteLine();

            // Try to add and remove a hero.
            this.RemoveTest();
            this.GetListTest();
            Console.WriteLine();
            Console.WriteLine();

            // Try to temporarily change a hero's name.
            this.StoreTest();
            this.GetListTest();
            Console.WriteLine();
            Console.WriteLine();

        }



    }



}
