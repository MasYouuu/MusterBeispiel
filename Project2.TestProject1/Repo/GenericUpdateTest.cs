using AutoMapper;
using FluentAssertions;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Mapper;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.TestProject1.Helpers;

namespace Project2.TestProject1.Repo
{
    [Collection("Sequential")]
    public class GenericUpdateTest
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
                new Tree("Tree1", Species.Ginkgo,"Type1", "Type25") { Id = 2 },
                new Tree("Tree2", Species.Mosses,"Type2", "Type26") { Id = 2 },
                new Tree("Tree3", Species.Ferns,"Type3", "Type27") { Id = 2 }
            };
            var treeRepo = new TreeRepo(context, mapper);

            var ownerList = new List<Owner>
            {
                new Owner("Ali", "Awan") { Id = 3 },
                new Owner("Florian", "Weber") { Id = 3 },
                new Owner("Mathias", "Bodenbrunner") { Id = 3 }
            };
            var ownerRepo = new OwnerRepo(context, mapper);

            var gardenList = new List<Garden>
            {
                new Garden("Location1", new(), new Owner("Ali", "Awan") { Id = 1}) { Id = 4 },
                new Garden("Location2", new(), new Owner("Ali", "Awan") { Id = 1 }) { Id = 4 },
                new Garden("Location3", new(), new Owner("Mathias", "Bodenbrunner")) { Id = 4 },
            };
            var gardenRepo = new GardenRepo(context, mapper);

            yield return new object[] { mapper.Map<FlowerDTO>(flowerList[0]), mapper.Map<FlowerDTO>(flowerList[1]), flowerRepo, context };
            yield return new object[] { mapper.Map<TreeDTO>(treeList[0]), mapper.Map<TreeDTO>(treeList[1]), treeRepo, context };
            yield return new object[] { mapper.Map<OwnerDTO>(ownerList[0]), mapper.Map<OwnerDTO>(ownerList[1]), ownerRepo, context };
            yield return new object[] { mapper.Map<GardenDTO>(gardenList[0]), mapper.Map<GardenDTO>(gardenList[1]), gardenRepo, context };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task UpdateTest<T, D>(D entity, D updateEntity, IGenericRepo<T, D> repo, GardenContext context) where T : class
        {
            await repo.Add(entity);
            repo.GetAll().FirstOrDefault().Should().BeEquivalentTo(entity);
            context.ChangeTracker.Clear();
            await repo.Update(updateEntity);
            context.ChangeTracker.Clear();
            repo.GetAll().FirstOrDefault().Should().BeEquivalentTo(updateEntity);
        }
    }
}
