using AutoMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.Infrastructure.Repos;
using Project2.TestProject1.Helpers;
using Project2.Infrastructure.Mapper;
using NSubstitute;
using Project2.WebAPI.Controllers;
using Project2.WebAPI.Controllers.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Project2.TestProject1.WebAPI
{
    [Collection("Sequential")]
    public class APIGetTest
    {
        public static IEnumerable<object[]> TestData()
        {
            var flowerList = new List<FlowerDTO>
            {
                new FlowerDTO(1, "Flo", Species.Ferns, "red", "Spring"),
                new FlowerDTO(2, "Ali", Species.Mosses, "yellow", "Summer"),
                new FlowerDTO(3, "Mathias", Species.Ginkgo, "blue", "Winter")
            };
            var flowerRepo = Substitute.For<IFlowerRepo>();
            flowerRepo.GetAll().Returns(flowerList);

            var treeList = new List<TreeDTO>
            {
                new TreeDTO(1, "Tree1", Species.Ginkgo,"Type1", "Type25"),
                new TreeDTO(2, "Tree2", Species.Mosses,"Type2", "Type26"),
                new TreeDTO(3, "Tree3", Species.Ferns,"Type3", "Type27")
            };
            var treeRepo = Substitute.For<ITreeRepo>();
            treeRepo.GetAll().Returns(treeList);

            var ownerList = new List<OwnerDTO>
            {
                new OwnerDTO(1, "Ali", "Awan"),
                new OwnerDTO(2, "Florian", "Weber"),
                new OwnerDTO(3, "Mathias", "Bodenbrunner")
            };
            var ownerRepo = Substitute.For<IOwnerRepo>();
            ownerRepo.GetAll().Returns(ownerList);

            var gardenList = new List<GardenDTO>
            {
                new GardenDTO(1, "Location1", new(), new OwnerDTO(4, "Ali", "Awan")),
                new GardenDTO(2, "Location2", new(), new OwnerDTO(5, "Florian", "Weber")),
                new GardenDTO(3, "Location3", new(), new OwnerDTO(6, "Mathias", "Bodenbrunner")),
            };
            var gardenRepo = Substitute.For<IGardenRepo>();
            gardenRepo.GetAll().Returns(gardenList);

            yield return new object[] { flowerList, flowerRepo };
            yield return new object[] { treeList, treeRepo };
            yield return new object[] { ownerList, ownerRepo };
            yield return new object[] { gardenList, gardenRepo };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetAllTest<T, D>(List<D> entity, IGenericRepo<T, D> repo) where T : class
        {
            var controller = new GenericController<T, D>(repo);
            var result = controller.GetAll().Result;
            var list = ((result as OkObjectResult).Value as IEnumerable<D>);
            entity.AsEnumerable().Should().BeEquivalentTo(list);
        }
    }
}
