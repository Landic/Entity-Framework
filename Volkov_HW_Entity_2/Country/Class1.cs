namespace Country_Table
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameCapital { get; set; }
        public long Population { get; set; }
        public float Area { get; set; }
        public virtual Continent Continent { get; set; }
    }

    public class Continent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Country> Country { get; set; }
    }
}
