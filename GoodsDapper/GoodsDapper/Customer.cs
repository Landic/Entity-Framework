using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsDapper
{
    internal class Customer
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }

    }
}
