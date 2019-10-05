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
        public DbSet<ThePriceInclude> ThePriceIncludes { get; set; }
        public DbSet<ExImage> ExImages { get; set; }
        public DbSet<ExCity> ExCities { get; set; }
        public DbSet<ExDate> ExDates { get; set; }
        public DbSet<ExDuration> ExDurations { get; set; }
        public DbSet<ExPrice> ExPrices { get; set; }
        public DbSet<ExHotel> ExHotels { get; set; }
       public ExcursionContext(DbContextOptions<ExcursionContext> options)
            : base(options)
        {
            Database.Migrate();
        } 
    }
}
