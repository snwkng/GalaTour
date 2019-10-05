using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class ExHotel
    {
        public int ID { get; set; }
        public string HotelName { get; set; }
        public string HotelLink { get; set; }

        public virtual ICollection<Excursion> Excursions { get; set; }
    }
}
