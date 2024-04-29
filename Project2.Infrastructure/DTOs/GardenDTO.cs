namespace Project2.Infrastructure.DTOs
{
    public class GardenDTO(int id, string location, List<PlantDTO> plants, OwnerDTO owner) : BaseDTO(id)
    {
        public string Location { get; set; } = location;
        public List<PlantDTO> Plants { get; set; } = plants;
        public OwnerDTO Owner { get; set; } = owner;
    }
}
