using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GalaTour.Models
{
    public class BusTour
    {
        [Key]
        public int ID { get; set; }
        //public string Region { get; set; }
        // public string RegionImage { get; set; }
        //public string City { get; set; }
        public string HotelType { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public string AddInfo { get; set; }
        public int Price { get; set; }
        public string Date { get; set; }
        public string DocLink { get; set; }
        public string HotelImage { get; set; }

        public int RegionID { get; set; }
        public int TourCityID { get; set; }

        public virtual TourCity TourCity { get; set; }
        public virtual Region Region { get; set; }

    }
}
