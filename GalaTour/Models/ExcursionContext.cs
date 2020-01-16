using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GalaTour.Models
{
    public class ExcursionContext : DbContext
    {
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<ExCity> ExCities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public DbSet<BusTour> BusTours { get; set; }
       public ExcursionContext(DbContextOptions<ExcursionContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        } 
    }
}
