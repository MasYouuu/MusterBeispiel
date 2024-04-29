namespace Project2.Infrastructure.Model
{
    public abstract class Plant : BaseEntity
    {
        public string Name { get; set; }
        public List<Garden> Gardens { get; set; } = new();
        public Species Species { get; set; }


        public Plant() { }


        public Plant(string name, Species species)
        {
            Name = name;
            Species = species;
        }
    }

    public enum Species
    {
        Ginkgo, Mosses, Flowering_Plants, Ferns
    }
}
