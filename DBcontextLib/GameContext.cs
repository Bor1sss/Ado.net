using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelStruct;
using System.Collections.Generic;

namespace DBcontextLib
{
    public class GameContext : DbContext
    {
      /*  static DbContextOptions<GameContext> _options;
        static GameContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GameContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }*/
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Style> Styles { get; set; }
        public GameContext()
          /* : base(_options)*/
        {
            /*    if (Database.EnsureCreated())
                {
                }*/
            /*var studio1 = new Studio { Name = "Rockstar Games" };
            var studio2 = new Studio { Name = "Naughty Dog" };
            var studio3 = new Studio { Name = "Ubisoft" };

            var style1 = new Style { Name = "Action" };
            var style2 = new Style { Name = "Adventure" };
            var style3 = new Style { Name = "RPG" };
            var style4 = new Style { Name = "Simulation" };

            var game1 = new Game { Name = "Grand Theft Auto V", Realise = new DateTime(2013, 9, 17), Studio = studio1, Styles = new List<Style> { style1 } };
            var game2 = new Game { Name = "The Last of Us Part II", Realise = new DateTime(2020, 6, 19), Studio = studio2, Styles = new List<Style> { style1, style2 } };
            var game3 = new Game { Name = "Assassin's Creed Valhalla", Realise = new DateTime(2020, 11, 10), Studio = studio3, Styles = new List<Style> { style1, style3 } };
            var game4 = new Game { Name = "The Sims 4", Realise = new DateTime(2014, 9, 2), Studio = studio1, Styles = new List<Style> { style4 } };

            Studios.Add(studio1);
            Studios.Add(studio2);
            Studios.Add(studio3);

           Styles.Add(style1);
           Styles.Add(style2);
           Styles.Add(style3);
           Styles.Add(style4);

           Games.Add(game1);
           Games.Add(game2);
           Games.Add(game3);
           Games.Add(game4);

           SaveChanges();*//*
        }*/
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // метод UseLazyLoadingProxies() делает доступной ленивую загрузку.
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=DESKTOP-BORIS;Database=Migration;Integrated Security=SSPI;TrustServerCertificate=true");
        }
    }
}
