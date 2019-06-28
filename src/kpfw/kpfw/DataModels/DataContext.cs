using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Timeline> Timeline { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
