using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class Region
    {
        public int ID { get; set; }
        public string RegionName { get; set; }
        public string RegionImage { get; set; }
        public ICollection<BusTour> BusTours { get; set; }
    }
}
