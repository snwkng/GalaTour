using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GalaTour.Models
{
    public class ExDate
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
