using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCompany.Model
{
	public class Employees
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Email { get; set; }
		public virtual Position? Position { get; set; }
	}
}
