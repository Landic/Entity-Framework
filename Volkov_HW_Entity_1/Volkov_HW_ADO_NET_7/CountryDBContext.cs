using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volkov_HW_ADO_NET_7
{
    public class CountryDBContext : DbContext
    {
        public DbSet<Country> Countries { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=DESKTOP-KBKU3U8;Database=Countrys;Integrated Security=SSPI;TrustServerCertificate=true");
        }
    }
}
