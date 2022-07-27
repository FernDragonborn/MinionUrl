using Microsoft.EntityFrameworkCore;
using MinionUrl.Models;

namespace MinionUrl.Data
{
    public class MinionUrlDbContext : DbContext
    {
        public MinionUrlDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UrlData> UrlData { get; set; }
    }
}
