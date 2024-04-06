using Microsoft.EntityFrameworkCore;

namespace RapidApi.Model
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<ApiKeys> apiKeys { get; set; }
    }
}
