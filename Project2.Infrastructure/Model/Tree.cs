namespace Project2.Infrastructure.Model
{
    public class Tree : Plant
    {
        public string BarkType { get; set; }
        public string LeafType { get; set; }


        public Tree() { }


        public Tree(string name, Species species, string barkType, string leafType) : base(name, species)
        {
            Name = name;
            Species = species;
            BarkType = barkType;
            LeafType = leafType;
        }
    }
}
