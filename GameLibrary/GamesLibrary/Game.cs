using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamesLibrary
{
    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Studio Studio { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public DateTime ReleaseDate {  get; set; }

        [EnumDataType(typeof(GameMode))]
        public GameMode Mode { get; set; }
        public int SaledCopies { get; set; }
    }
}