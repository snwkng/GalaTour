using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class ExDuration
    {
        public int ID { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Excursion> Excursions { get; set; }
    }
}
