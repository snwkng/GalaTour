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
       public ExcursionContext(DbContextOptions<ExcursionContext> options)
            : base(options)
        {
            Database.Migrate();
        } 
    }
}
