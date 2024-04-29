namespace Project2.Infrastructure.Model
{
    public class Flower : Plant
    {
        public string PetalColor { get; set; }
        public string BloomTime { get; set; }


        public Flower() { }


        public Flower(string name, Species species, string petalColor, string bloomTime) : base(name, species)
        {
            Name = name;
            Species = species;
            PetalColor = petalColor;
            BloomTime = bloomTime;
        }
    }
}
