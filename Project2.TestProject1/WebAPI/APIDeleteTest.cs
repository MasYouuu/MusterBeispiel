using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.TestProject1.Helpers;
using System.Net.Http.Json;

namespace Project2.TestProject1.WebAPI
{
    [Collection("Sequential")]
    public class APIDeleteTest : IntegrationTestBase
    {
        public APIDeleteTest(WebApplicationFactory<Program> factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> TestData()
        {
            var flower = new FlowerDTO(100, "Flo", Species.Ferns, "red", "Spring");

            var tree = new TreeDTO(100, "Tree1", Species.Ginkgo, "Type1", "Type25");

            var owner = new OwnerDTO(100, "Ali", "Awan");

            var garden = new GardenDTO(100, "Location1", new(), new OwnerDTO(100, "Ali", "Awan"));

            yield return new object[] { flower, "Flower" };
            yield return new object[] { tree, "Tree" };
            yield return new object[] { owner, "Owner" };
            yield return new object[] { garden, "Garden" };
        }

        [Theory]
        [MemberData(nameof(TestData))] 
        public async Task TestDelete<D>(D entity, string path) where D : BaseDTO
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/{path}",entity);
            result.IsSuccessStatusCode.Should().BeTrue();
            result = await _httpClient.GetAsync($"/api/{path}/byId/{entity.Id}");
            result.IsSuccessStatusCode.Should().BeTrue();
            result = await _httpClient.DeleteAsync($"/api/{path}/{entity.Id}");
            result.IsSuccessStatusCode.Should().BeTrue();
            result = await _httpClient.GetAsync($"/api/{path}/byId/{entity.Id}");
            result.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}
