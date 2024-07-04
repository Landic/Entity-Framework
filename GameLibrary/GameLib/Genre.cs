using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public  class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
