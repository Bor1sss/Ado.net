using Microsoft.EntityFrameworkCore;
using Countries;
using Microsoft.Extensions.Configuration;


namespace CountryContext1
{
    public class CountryContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Countries.Region> Regions { get; set; }

        static DbContextOptions<CountryContext> _options;

        static CountryContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CountryContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public CountryContext() : base(_options)
        {

            
            if (Database.EnsureCreated())
            {

                Region Regions1 = new Region { Name = "Europe" };
                Region Regions2 = new Region { Name = "America" };
                Region Regions3 = new Region { Name = "Asia" };

                Regions?.Add(Regions1);
                Regions?.Add(Regions2);
                Regions?.Add(Regions3);


                Country group1 = new Country { Name = "USA", population = 331002651, Capital = "Washington DC", Region = Regions2,size=1231 };
                Country group2 = new Country { Name = "Germany", population = 83149300, Capital = "Berlin", Region = Regions1 , size = 1232 };
                Country group3 = new Country { Name = "China", population = 1444216107, Capital = "Beijing", Region = Regions3, size = 1233 };
                Country group4 = new Country { Name = "Brazil", population = 212559417, Capital = "Brasília", Region = Regions2 , size = 1234 };
                Country group5 = new Country { Name = "India", population = 1380004385, Capital = "New Delhi", Region = Regions3 , size = 1235 };
                Country group6 = new Country { Name = "Japan", population = 126476461, Capital = "Tokyo", Region = Regions3 , size = 1236 };
                Country group7 = new Country { Name = "Canada", population = 37742154, Capital = "Ottawa", Region = Regions2 , size = 1237 };
                Country group8 = new Country { Name = "Australia", population = 25499884, Capital = "Canberra", Region = Regions1 , size = 1238 };
                Country group9 = new Country { Name = "South Africa", population = 59308690, Capital = "Pretoria", Region = Regions2 , size = 1239 };
                Country group10 = new Country { Name = "Russia", population = 145912025, Capital = "Moscow", Region = Regions1,size=123456 };
                Countries?.Add(group1);
                Countries?.Add(group2);
                Countries?.Add(group4);
                Countries?.Add(group5);
                Countries?.Add(group6);
                Countries?.Add(group7);
                Countries?.Add(group8);
                Countries?.Add(group9);
                Countries?.Add(group10);


                SaveChanges();
            }


        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // метод UseLazyLoadingProxies() делает доступной ленивую загрузку.
            optionsBuilder.UseLazyLoadingProxies();
        }


    }
}
