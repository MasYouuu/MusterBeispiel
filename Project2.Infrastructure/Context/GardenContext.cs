using Bogus;
using Microsoft.EntityFrameworkCore;
using Project2.Infrastructure.Model;

namespace Project2.Infrastructure.Context
{
    public class GardenContext : DbContext
    {
        public DbSet<Flower> Flowers => Set<Flower>();
        public DbSet<Garden> Gardens => Set<Garden>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Plant> Plants => Set<Plant>();
        public DbSet<Tree> Trees => Set<Tree>();


        public GardenContext(DbContextOptions opt) : base(opt)
        {

        }

        public GardenContext() : this(new DbContextOptionsBuilder<GardenContext>().UseSqlite("Data Source=Garden.db").Options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tree>().HasBaseType(typeof(Plant));
            modelBuilder.Entity<Flower>().HasBaseType(typeof(Plant));
            modelBuilder.Entity<Garden>().Navigation(x => x.Owner).AutoInclude();
            modelBuilder.Entity<Garden>().Navigation(x => x.Plants).AutoInclude();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public async Task SeedBogusAsync()
        {
            var fakerOwners = new Faker<Owner>().CustomInstantiator(f =>
            {
                return new Owner()
                {
                    Firstname = f.Person.FirstName,
                    Lastname = f.Person.LastName,
                };
            }).Generate(20);


            var fakerTrees = new Faker<Tree>().CustomInstantiator(f =>
            {
                return new Tree(f.Commerce.ProductName(), f.PickRandom<Species>(), f.Commerce.ProductName(), f.Commerce.ProductName());
            }).Generate(20);


            var fakerFlower = new Faker<Flower>().CustomInstantiator(f =>
            {
                return new Flower()
                {
                    Name = f.Commerce.ProductName(),
                    Species = f.PickRandom<Species>(),
                    BloomTime = f.Date.Month().ToString(),
                    PetalColor = f.Commerce.ProductName(),
                };
            }).Generate(20);


            var ownerGardenMap = new Dictionary<Owner, Garden>();
            var fakerGardens = new Faker<Garden>().CustomInstantiator(f =>
            {
                var owner = f.PickRandom(fakerOwners);
                do
                {
                    owner = f.PickRandom(fakerOwners);
                }
                while (ownerGardenMap.ContainsKey(owner));

                var garden = new Garden()
                {
                    Location = f.Address.StreetName().ToString(),
                    Owner = owner
                };

                for (int i = 0; i < 5; i++)
                {
                    garden.AddPlantToGarden(f.PickRandom(fakerFlower));
                    garden.AddPlantToGarden(f.PickRandom(fakerTrees));
                }

                ownerGardenMap.Add(owner, garden);
                return garden;
            }).Generate(20);


            Owners.AddRange(fakerOwners);
            Gardens.AddRange(fakerGardens);
            Trees.AddRange(fakerTrees);
            Flowers.AddRange(fakerFlower);
            await SaveChangesAsync();
        }
    }
}
