using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class TourCity
    {
        public int ID { get; set; }
        public string City { get; set; }

        public ICollection<BusTour> BusTours { get; set; }
    }
}
