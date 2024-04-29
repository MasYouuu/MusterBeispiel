using AutoMapper;
using FluentAssertions;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Mapper;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.TestProject1.Helpers;

namespace Project2.TestProject1.Repo
{
    [Collection("Sequential")]
    public class GenericAddTest
    {
        public static IEnumerable<object[]> TestData()
        {
            var context = DatabaseUtilities.GetDatabase();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))).CreateMapper();

            var flowerList = new List<Flower>
            {
                new Flower("Flo", Species.Ferns, "red", "Spring"),
                new Flower("Ali", Species.Mosses, "yellow", "Summer"),
                new Flower("Mathias", Species.Ginkgo, "blue", "Winter")
            };
            var flowerRepo = new FlowerRepo(context, mapper);

            var treeList = new List<Tree>
            {
                new Tree("Tree1", Species.Ginkgo,"Type1", "Type25"),
                new Tree("Tree2", Species.Mosses,"Type2", "Type26"),
                new Tree("Tree3", Species.Ferns,"Type3", "Type27")
            };
            var treeRepo = new TreeRepo(context, mapper);

            var ownerList = new List<Owner>
            {
                new Owner("Ali", "Awan"),
                new Owner("Florian", "Weber"),
                new Owner("Mathias", "Bodenbrunner")
            };
            var ownerRepo = new OwnerRepo(context, mapper);

            var gardenList = new List<Garden>
            {
                new Garden("Location1", new(), new Owner("Ali", "Awan")),
                new Garden("Location2", new(), new Owner("Florian", "Weber")),
                new Garden("Location3", new(), new Owner("Mathias", "Bodenbrunner")),
            };
            var gardenRepo = new GardenRepo(context, mapper);

            yield return new object[] { mapper.Map<FlowerDTO>(flowerList.FirstOrDefault()), flowerRepo };
            yield return new object[] { mapper.Map<TreeDTO>(treeList.FirstOrDefault()), treeRepo };
            yield return new object[] { mapper.Map<OwnerDTO>(ownerList.FirstOrDefault()), ownerRepo };
            yield return new object[] { mapper.Map<GardenDTO>(gardenList.FirstOrDefault()), gardenRepo };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void AddTest<T, D>(D entity, IGenericRepo<T, D> repo) where T : class
        {
            repo.GetAll().Count().Should().Be(0);
            repo.Add(entity);
            repo.GetAll().Count().Should().Be(1);
        }
    }
}
