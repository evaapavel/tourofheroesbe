using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;



namespace TourOfHeroesBEMain
{



    public class AdodbMssqlTest
    {



        public void ConnectionTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            //string connectionString = ConfigurationManager.ConnectionStrings["TourOfHeroesDBConnectionString"].ConnectionString;
            string connectionString = config["ConnectionStrings:TourOfHeroesDBConnectionString"];
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT ID, NAME FROM TH_HERO", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0]);
                string username = reader[1].ToString();
                string password = reader[2].ToString();
                Console.WriteLine($"ID: {id}    Username: {username}    Password: {password}");
            }
            reader.Close();
            command.Dispose();
            reader = null;
            command = null;

            connection.Close();
            connection.Dispose();
            connection = null;
        }



    }



}
