using System;
using System.ComponentModel.DataAnnotations;

namespace CarteiraDigital.Models
{
    public class Outflow
    {
        [Display(Name = "Código")]
        public virtual long Id { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public virtual DateTime OutflowDate { get; set; }
        
        [Display(Name = "Descrição")]
        public virtual string OutflowDescription { get; set; }
        
        [Display(Name = "Valor do saque")]
        [DataType(DataType.Currency)]
        public virtual double OutflowAmount { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }

        public virtual Filter Filter { get; set; }

        public Outflow() { }

        public Outflow(long id, DateTime outflowDate, string outflowDescription, double outflowAmount)
        {
            Id = id;
            OutflowDate = outflowDate;
            OutflowDescription = outflowDescription;
            OutflowAmount = outflowAmount;
        }
    }
}
