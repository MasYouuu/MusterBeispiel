using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project2.Infrastructure.Context;

namespace Project2.TestProject1.Helpers
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _httpClient;
        public IntegrationTestBase(WebApplicationFactory<Program>factory) =>
            _httpClient = factory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => 
                    d.ServiceType == typeof(DbContextOptions<GardenContext>));
                    services.Remove(descriptor);
                    services.AddDbContext<GardenContext>(options => 
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));
       
                })).CreateClient();
    }
}
