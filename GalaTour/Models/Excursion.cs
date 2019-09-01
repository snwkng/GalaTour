using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GalaTour.Models
{
    public class Excursion
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int ExDurationID { get; set; }
        public int ExCityID { get; set; }
        public int ExDateID { get; set; }
        public int ExPriceID { get; set; }

        public virtual ExDuration ExDuration { get; set; }
        public virtual ExCity ExCity { get; set; }
        public virtual ExDate ExDate { get; set; }
        public virtual ExPrice ExPrice { get; set; }
    }
}
