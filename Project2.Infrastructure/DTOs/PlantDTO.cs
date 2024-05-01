namespace Project2.Infrastructure.DTOs
{
    public class PlantDTO(int id) : BaseDTO(id)
    {
        public int Id { get; set; } = id;
    }
}
