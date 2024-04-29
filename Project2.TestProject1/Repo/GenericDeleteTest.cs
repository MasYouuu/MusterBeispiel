using AutoMapper;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.Infrastructure.Repos;
using Project2.TestProject1.Helpers;
using Project2.Infrastructure.Mapper;
using FluentAssertions;
using Project2.Infrastructure.Context;

namespace Project2.TestProject1.Repo
{
    [Collection("Sequential")]
    public class GenericDeleteTest
    {
        public static IEnumerable<object[]> TestData()
        {
            var context = DatabaseUtilities.GetDatabase();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))).CreateMapper();

            var flowerList = new List<Flower>
            {
                new Flower("Flo", Species.Ferns, "red", "Spring") { Id = 1 },
                new Flower("Ali", Species.Mosses, "yellow", "Summer") { Id = 1 },
                new Flower("Mathias", Species.Ginkgo, "blue", "Winter") { Id = 1 }
            };
            var flowerRepo = new FlowerRepo(context, mapper);

            var treeList = new List<Tree>
            {
                new Tree("Tree1", Species.Ginkgo,"Type1", "Type25") { Id = 1 },
                new Tree("Tree2", Species.Mosses,"Type2", "Type26") { Id = 1 },
                new Tree("Tree3", Species.Ferns,"Type3", "Type27") { Id = 1 }
            };
            var treeRepo = new TreeRepo(context, mapper);

            var ownerList = new List<Owner>
            {
                new Owner("Ali", "Awan") { Id = 1 },
                new Owner("Florian", "Weber") { Id = 1 },
                new Owner("Mathias", "Bodenbrunner") { Id = 1 }
            };
            var ownerRepo = new OwnerRepo(context, mapper);

            var gardenList = new List<Garden>
            {
                new Garden("Location1", new(), new Owner("Ali", "Awan")) { Id = 1 },
                new Garden("Location2", new(), new Owner("Florian", "Weber")) { Id = 1 },
                new Garden("Location3", new(), new Owner("Mathias", "Bodenbrunner")) { Id = 1 },
            };
            var gardenRepo = new GardenRepo(context, mapper);

            yield return new object[] { mapper.Map<FlowerDTO>(flowerList.FirstOrDefault()), flowerRepo, context };
            yield return new object[] { mapper.Map<TreeDTO>(treeList.FirstOrDefault()), treeRepo, context };
            yield return new object[] { mapper.Map<OwnerDTO>(ownerList.FirstOrDefault()), ownerRepo, context };
            yield return new object[] { mapper.Map<GardenDTO>(gardenList.FirstOrDefault()), gardenRepo, context };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task DeleteTest<T, D>(D entity, IGenericRepo<T, D> repo, GardenContext context) where T : class where D : BaseDTO
        {
            repo.GetAll().Count().Should().Be(0);
            await repo.Add(entity);
            context.ChangeTracker.Clear();
            repo.GetAll().Count().Should().Be(1);
            await repo.Remove(entity.Id);
            repo.GetAll().Count().Should().Be(0);
        }
    }
}
