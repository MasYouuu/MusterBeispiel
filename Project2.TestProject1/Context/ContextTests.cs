using FluentAssertions;
using Project2.TestProject1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.TestProject1.Context
{
    [Collection("Sequential")]
    public class ContextTests
    {
        [Fact]
        public async Task TestContextSeeding()
        {
            var context = DatabaseUtilities.GetDatabase();
            await context.InitializeDatabase();

            context.Flowers.Count().Should().BeGreaterThan(0);
            context.Trees.Count().Should().BeGreaterThan(0);
            context.Gardens.Count().Should().BeGreaterThan(0);
            context.Owners.Count().Should().BeGreaterThan(0);
        }
    }
}
