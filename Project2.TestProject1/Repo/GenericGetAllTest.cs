using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.Infrastructure.Repos;
using MockQueryable.NSubstitute;
using FluentAssertions;
using Project2.Infrastructure.Mapper;

namespace Project2.TestProject1.Repo
{
    [Collection("Sequential")]
    public class GenericGetAllTest
    {
        public static IEnumerable<object[]> TestData()
        {
            var context = Substitute.For<GardenContext>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))).CreateMapper();

            var flowerList = new List<Flower>
            {
                new Flower("Flo", Species.Ferns, "red", "Spring"),
                new Flower("Ali", Species.Mosses, "yellow", "Summer"),
                new Flower("Mathias", Species.Ginkgo, "blue", "Winter")
            }.AsQueryable().BuildMockDbSet();
            var flowerRepo = new FlowerRepo(context, mapper);

            var treeList = new List<Tree>
            {
                new Tree("Tree1", Species.Ginkgo,"Type1", "Type25"),
                new Tree("Tree2", Species.Mosses,"Type2", "Type26"),
                new Tree("Tree3", Species.Ferns,"Type3", "Type27")
            }.AsQueryable().BuildMockDbSet();
            var treeRepo = new TreeRepo(context, mapper);

            var ownerList = new List<Owner>
            {
                new Owner("Ali", "Awan"),
                new Owner("Florian", "Weber"),
                new Owner("Mathias", "Bodenbrunner")
            }.AsQueryable().BuildMockDbSet();
            var ownerRepo = new OwnerRepo(context, mapper);

            var gardenList = new List<Garden>
            {
                new Garden("Location1", new(), new Owner("Ali", "Awan")),
                new Garden("Location2", new(), new Owner("Florian", "Weber")),
                new Garden("Location3", new(), new Owner("Mathias", "Bodenbrunner")),
            }.AsQueryable().BuildMockDbSet();
            var gardenRepo = new GardenRepo(context, mapper);

            yield return new object[] { flowerList, flowerRepo, context };
            yield return new object[] { treeList, treeRepo, context };
            yield return new object[] { ownerList, ownerRepo, context };
            yield return new object[] { gardenList, gardenRepo, context };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetAllTest<T, D>(DbSet<T> set, IGenericRepo<T, D> repo, GardenContext context) where T : class
        {
            context.Set<T>().Returns(set);
            repo.GetAll().Count().Should().Be(3);
        }
    }
}
