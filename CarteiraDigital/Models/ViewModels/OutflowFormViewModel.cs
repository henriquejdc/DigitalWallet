using System.Collections.Generic;

namespace CarteiraDigital.Models.ViewModels
{
    public class OutflowFormViewModel
    {
        public Outflow Outflow { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
