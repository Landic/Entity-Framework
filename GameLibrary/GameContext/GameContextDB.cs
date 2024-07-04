using GamesLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameContext
{
    public class GameContextDB : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Genre> Genres { get; set; }

        static DbContextOptions<GameContextDB> _options;

        static GameContextDB()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection")!;

            var optionsBuilder = new DbContextOptionsBuilder<GameContextDB>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public GameContextDB()
           : base(_options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

        }
    }
}
