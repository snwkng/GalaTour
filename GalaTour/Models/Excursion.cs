using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GalaTour.Models
{
    public class Excursion
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThePriceInclude { get; set; }
        public string ImageURL { get; set; }
        public string Duration { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string HotelName { get; set; }
        public string HotelLink { get; set; }
        public string DocLink { get; set; }

        public int ExCityID { get; set; }
        
        public virtual ExCity ExCity { get; set; }
    }
}
