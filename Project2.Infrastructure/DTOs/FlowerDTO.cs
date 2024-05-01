using Project2.Infrastructure.Model;

namespace Project2.Infrastructure.DTOs
{
    public class FlowerDTO(int id, string name, Species species, string petalColor, string bloomTime) : PlantDTO(id)
    {
        public string Name { get; set; } = name;
        public Species Species { get; set; } = species;
        public string PetalColor { get; set; } = petalColor;
        public string BloomTime { get; set; } = bloomTime;
    }
}
