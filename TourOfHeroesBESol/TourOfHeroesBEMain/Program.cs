using System;

namespace TourOfHeroesBEMain
{



    class Program
    {



        static void Main(string[] args)
        {
            AdodbMssqlTest test = new AdodbMssqlTest();
            test.ConnectionTest();

            Console.ReadKey(true);
        }



    }



}

