using System;

namespace TourOfHeroesBEMain
{



    class Program
    {



        static void Main(string[] args)
        {
            //AdodbMssqlTest test = new AdodbMssqlTest();
            //test.ConnectionTest();

            //EFMssqlTest.LaunchTestSuite();
            EFMssqlTest efTest = new EFMssqlTest();
            efTest.LaunchTestSuite();

            //Console.ReadKey(true);
        }



    }



}

