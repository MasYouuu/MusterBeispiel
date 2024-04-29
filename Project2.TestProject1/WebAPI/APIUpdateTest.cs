using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.TestProject1.Helpers;
using System.Net.Http.Json;

namespace Project2.TestProject1.WebAPI
{
    [Collection("Sequential")]
    public class APIUpdateTest : IntegrationTestBase
    {
        public APIUpdateTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> TestData()
        {
            var flower = new FlowerDTO(100, "Flo", Species.Ferns, "red", "Spring");
            var flower2 = new FlowerDTO(100, "Mathias", Species.Flowering_Plants, "blue", "Spring");

            var tree = new TreeDTO(100, "Tree1", Species.Ginkgo, "Type1", "Type25");
            var tree2 = new TreeDTO(100, "Tree1", Species.Ginkgo, "Type1", "Type25");

            var owner = new OwnerDTO(100, "Ali", "Awan");
            var owner2 = new OwnerDTO(100, "Ali", "Awan");

            var garden = new GardenDTO(100, "Location1", new(), new OwnerDTO(100, "Ali", "Awan"));
            var garden2 = new GardenDTO(100, "Location1", new(), new OwnerDTO(100, "Ali", "Awan"));

            yield return new object[] { flower, flower2, "Flower" };
            yield return new object[] { tree, tree2, "Tree" };
            yield return new object[] { owner, owner2, "Owner" };
            yield return new object[] { garden, garden2, "Garden" };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task TestPatch<D>(D entity, D entity2, string path) where D : BaseDTO
        {
            var r = await _httpClient.PostAsJsonAsync($"/api/{path}", entity);
            var getResponse = await _httpClient.GetAsync($"/api/{path}/byId/{entity.Id}");
            var contentBefore = (await getResponse.Content.ReadFromJsonAsync<D>());
            var result = await _httpClient.PatchAsJsonAsync($"/api/{path}", entity2);

            getResponse = await _httpClient.GetAsync($"/api/{path}/byId/{entity.Id}");
            var contentAfter = (await getResponse.Content.ReadFromJsonAsync<D>());
            result.IsSuccessStatusCode.Should().BeTrue();
            contentAfter.Should().BeEquivalentTo(entity2);
        }
    }
}
