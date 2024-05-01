using Project2.Infrastructure.Context;

namespace Project2.TestProject1.Helpers
{
    public static class DatabaseUtilities
    {
        public static GardenContext GetDatabase()
        {
            var context = new GardenContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        public async static Task InitializeDatabase(this GardenContext context)
        {
            await context.SeedBogusAsync();
        }
    }
}
