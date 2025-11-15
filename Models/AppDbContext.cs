using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace state_district_city.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
              : base(options)
        {
        }

        
        public DbSet<MasterState> MasterState { get; set; }
        public DbSet<MasterDistrict> MasterDistrict { get; set; }
        public DbSet<MasterCity> MasterCity { get; set; }
        

    }
}
