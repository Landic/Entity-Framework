using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCompany.Model
{
	public class DBContext : DbContext
	{
		public DbSet<Employees> Employees { get; set; }
		public DbSet<Position> Positions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-1147B1S;Database=ITCompany;Trusted_Connection=True;");
		}

		public DBContext()
		{
			if (Database.EnsureCreated())
			{
				var positions = new List<Position>()
				{
					new Position{Name="Программист"},
					new Position{Name="Дизайнер"},
					new Position{Name="Тестировщик"},
					new Position{Name="Бизнес-Аналитик"}
				};
				var employees = new List<Employees>()
				{
					new Employees{Name="Александр", Surname="Александрович", Email="Aleksandrovich@gmail.com" ,Position = positions[0]},
					new Employees{Name="Иван", Surname="Иванов", Email="Ivanov1@gmail.com", Position = positions[1]},
					new Employees{Name="Василий", Surname="Васильевич", Email="vasilievich3@gmail.com", Position = positions[2]},
					new Employees{Name="Николай", Surname="Николаевич", Email="nikolaevich2@gmail.com", Position = positions[3]}
				};
				Positions?.AddRange(positions);
				Employees?.AddRange(employees);
				SaveChanges();
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employees>().Property(i => i.Name).IsRequired();
			modelBuilder.Entity<Employees>().Property(i => i.Surname).IsRequired();
			modelBuilder.Entity<Employees>().HasOne(i => i.Position).WithMany(i => i.Employees).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Position>().Property(i => i.Name).IsRequired();
			base.OnModelCreating(modelBuilder);
		}
	}
}
