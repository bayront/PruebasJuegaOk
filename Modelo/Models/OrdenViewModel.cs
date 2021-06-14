using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{
    public class OrdenViewModel
    {
        public int OrderID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Fecha de la orden")]
        public DateTime? OrderDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Fecha Requerida")]
        public DateTime? RequiredDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Fecha de envio")]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "Carga")]
        public decimal? Freight { get; set; }

        [Display(Name = "Nombre de la Empresa")]
        public string ShipName { get; set; }

        [Display(Name = "Dirección")]
        public string ShipAddress { get; set; }

        [Display(Name = "Ciudad")]
        public string ShipCity { get; set; }

        [Display(Name = "Region")]
        public string ShipRegion { get; set; }

        [Display(Name = "Código Postal")]
        public string ShipPostalCode { get; set; }

        [Display(Name = "País")]
        public string ShipCountry { get; set; }

        [Display(Name = "Empresa de envio")]
        public string CompanyName { get; set; }

        [Display(Name = "Nombre del cliente")]
        public string ContactName { get; set; }

        [Display(Name = "Apellido")]
        public string TitleOfCourtesy { get; set; }

        [Display(Name = "Apellido")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Empleado encargado de la orden")]
        public string Empleado { get; set; }

        [Display(Name = "Total a Pagar")]
        public decimal? Total { get; set; }

        [Display(Name = "Descuento Total")]
        public decimal? DescuentoTotal { get; set; }


    }
}
