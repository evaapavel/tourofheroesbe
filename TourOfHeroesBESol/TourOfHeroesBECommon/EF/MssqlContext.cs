using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using TourOfHeroesBECommon.BusinessObjects;



namespace TourOfHeroesBECommon.EF
{



    public class MssqlContext : DbContext
    {



        private readonly string _connectionString;



        public DbSet<Hero> Heroes { get; set; }



        public MssqlContext()
        {
            // Temporary solution.
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            //_connectionString = ConfigurationManager;
            _connectionString = config["ConnectionStrings:TourOfHeroesDBConnectionString"];
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }



    }



}
