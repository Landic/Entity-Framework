using System.Collections.Generic;

namespace GameLib
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate {  get; set; }
        public virtual Studio Studio { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}