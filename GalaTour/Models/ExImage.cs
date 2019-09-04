using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaTour.Models
{
    public class ExImage
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }

        public virtual ICollection<Excursion> Excursions { get; set; }
    }
}
