using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.TestProject1.Helpers;
using System.Net.Http.Json;

namespace Project2.TestProject1.WebAPI
{
    [Collection("Sequential")]
    public class APIPostTest : IntegrationTestBase
    {
        public APIPostTest(WebApplicationFactory<Program> factory) : base(factory)
        {

        }

        public static IEnumerable<object[]> TestData()
        {
            var flower = new FlowerDTO(100, "Flo", Species.Ferns, "red", "Spring");

            var tree = new TreeDTO(100, "Tree1", Species.Ginkgo, "Type1", "Type25");

            var owner = new OwnerDTO(100, "Ali", "Awan");

            var garden = new GardenDTO(100, "Location1", new(), new OwnerDTO(100, "Ali", "Awan"));

            yield return new object[] { flower, "Flower"};
            yield return new object[] { tree, "Tree" };
            yield return new object[] { owner, "Owner" };
            yield return new object[] { garden, "Garden" };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task TestPost<D>(D entity, string path)
        {
            var getResponse = await _httpClient.GetAsync($"/api/{path}/all");
            var contentBefore = await getResponse.Content.ReadFromJsonAsync<IEnumerable<D>>();
            var result = await _httpClient.PostAsJsonAsync($"/api/{path}",entity);

            getResponse = await _httpClient.GetAsync($"/api/{path}/all");
            var contentAfter = await getResponse.Content.ReadFromJsonAsync<IEnumerable<D>>();
            result.IsSuccessStatusCode.Should().BeTrue();
            contentBefore.Count().Should().BeLessThan(contentAfter.Count());
        }
    }
}
