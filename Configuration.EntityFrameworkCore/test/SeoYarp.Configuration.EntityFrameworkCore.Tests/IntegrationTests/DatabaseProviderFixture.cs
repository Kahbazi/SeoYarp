using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SeoYarp.Configuration.EntityFrameworkCore.Tests.IntegrationTests
{
    /// <summary>
    /// xUnit ClassFixture for creating and deleting integration test databases.
    /// </summary>
    /// <typeparam name="T">DbContext of Type T</typeparam>
    public class DatabaseProviderFixture<T> : IDisposable where T : DbContext
    {
        public object StoreOptions;
        public List<DbContextOptions<T>> Options;

        public void Dispose()
        {
            if (Options != null) // null check since fixtures are created even when tests are skipped
            {
                foreach (var option in Options.ToList())
                {
                    using (var context = (T)Activator.CreateInstance(typeof(T), option, StoreOptions))
                    {
                        context.Database.EnsureDeleted();
                    }
                }
            }
        }
    }
}
