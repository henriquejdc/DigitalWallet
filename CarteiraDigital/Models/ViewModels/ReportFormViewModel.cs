using System.Collections.Generic;

namespace CarteiraDigital.Models.ViewModels
{
    public class ReportFormViewModel
    {
        public List<Inflow> Inflow { get; set; }

        public List<Outflow> Outflow { get; set; }

        public Filter Filter { get; set; }
    }
}
