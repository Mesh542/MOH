using Microsoft.EntityFrameworkCore;
using MOH.Models;
using System.Collections.Generic;


namespace MOH.Data
{
    public class MOHDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public MOHDbContext(DbContextOptions<MOHDbContext> options) : base(options)
        {

        }
    }
}
