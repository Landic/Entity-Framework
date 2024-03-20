using Country_Table;
using Microsoft.EntityFrameworkCore;
namespace DBContext
{
    public class CountryDBContext : DbContext
    {
        public DbSet<Country> country { get; set; }
        public DbSet<Continent> continent { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=DESKTOP-KBKU3U8;Database=Countrys;Integrated Security=SSPI;TrustServerCertificate=true");
        }
        public CountryDBContext() 
        {
            if (Database.EnsureCreated())
            {
                var continent = new List<Continent>
                {
                    new Continent {Name = "Europa"},
                    new Continent {Name = "South America"},
                    new Continent {Name = "North America"},
                    new Continent {Name = "Asia"},
                    new Continent {Name = "Africa"}
                };
                var countries = new List<Country>
                    {
                        new Country(){Name = "Ukraine", NameCapital = "Kyiv", Population=40000000, Area = 603628, Continent=continent[0]},
                        new Country(){Name = "Germany", NameCapital = "Berlin", Population=83200000, Area = 357592, Continent=continent[0]},
                        new Country(){Name = "France", NameCapital = "Paris", Population=67750000, Area = 643801, Continent=continent[0]},
                        new Country(){Name = "Spain", NameCapital = "Barcelona", Population=47420000, Area = 506030, Continent=continent[0]},
                        new Country(){Name = "Poland", NameCapital = "Warsaw", Population=37750000, Area = 312696, Continent=continent[0]},
                        new Country(){Name = "China", NameCapital = "Beijing", Population=1409670000, Area = 9596961, Continent=continent[3]},
                        new Country(){Name = "Japan", NameCapital = "Tokyo", Population=125416877, Area = 377975, Continent=continent[3]},
                        new Country(){Name = "USA", NameCapital = "Washington", Population=334914895, Area = 9833520, Continent=continent[2]},
                        new Country(){Name = "Canada", NameCapital = "Ottawa", Population=40528396, Area = 3855100, Continent=continent[2]},
                        new Country(){Name = "Brazil", NameCapital = "Brasilia", Population=203062512, Area = 8515767, Continent=continent[1]}
                    };
                this.continent?.AddRange(continent);
                country?.AddRange(countries);
                SaveChanges();
                Console.WriteLine("База данных создана и заполнена");
            }
        }

    }
}
