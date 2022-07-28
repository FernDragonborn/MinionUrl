using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinionUrl.Models;

namespace MinionUrl.Data
{
    public class MinionUrlContext : DbContext
    {
        public MinionUrlContext (DbContextOptions<MinionUrlContext> options)
            : base(options)
        {
        }

        public DbSet<MinionUrl.Models.UrlData> UrlData { get; set; } = default!;
    }
}
