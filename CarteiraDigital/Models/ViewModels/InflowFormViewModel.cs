using System.Collections.Generic;

namespace CarteiraDigital.Models.ViewModels
{
    public class InflowFormViewModel
    {
        public Inflow Inflow { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
