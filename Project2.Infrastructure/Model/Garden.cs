namespace Project2.Infrastructure.Model
{
    public class Garden : BaseEntity
    {
        public string Location { get; set; }
        public readonly List<Plant> Plants = new();
        public Owner Owner { get; set; }


        public Garden() { }

        public Garden(string location, List<Plant> plants, Owner owner)
        {
            Location = location;
            Plants = plants;
            Owner = owner;
        }


        public void AddPlantToGarden(Plant plant)
        {
            Plants.Add(plant);
        }

        public void RemovePlant(Plant plant)
        {
            Plants.Remove(plant);
        }
    }
}
