using Project2.Infrastructure.Model;

namespace Project2.Infrastructure.DTOs
{
    public class TreeDTO(int id, string name, Species species, string barkType, string leafType) : BaseDTO(id)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string BarkType { get; set; } = barkType;
        public string LeafType { get; set; } = leafType;
        public Species Species { get; set; } = species;
    }
}
