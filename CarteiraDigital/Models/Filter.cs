using System;
using System.ComponentModel.DataAnnotations;

namespace CarteiraDigital.Models
{
    public class Filter
    {
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime MinDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime MaxDate { get; set; }

        public int Periodo { get; set; }

        public Filter() { }
    }
}
