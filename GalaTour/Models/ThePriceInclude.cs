using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class ThePriceInclude
    {
        public int ID { get; set; }
        public string PriceInclude { get; set; }

        public virtual ICollection<Excursion> Excursions { get; set; }
    }
}
