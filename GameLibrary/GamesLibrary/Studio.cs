using System.Collections.Generic;

namespace GamesLibrary
{
    public class Studio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
