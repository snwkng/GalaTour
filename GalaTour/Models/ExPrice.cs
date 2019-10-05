using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class ExPrice
    {
        public int ID { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Excursion> Excursions { get; set; }
    }
}
