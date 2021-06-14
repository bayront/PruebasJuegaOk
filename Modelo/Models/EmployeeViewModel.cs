using System;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Models
{
    public partial class EmployeeViewModel
    {

        public int? EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [StringLength(25)]
        [Display(Name = "Título de Cortesía")]
        public string TitleOfCourtesy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Fecha de Cumpleaños")]
        public DateTime? BirthDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Fecha de Contratación")]
        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [StringLength(15)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [StringLength(15)]
        [Display(Name = "Región")]
        public string Region { get; set; }

        [StringLength(10)]
        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }

        [StringLength(15)]
        [Display(Name = "País")]
        public string Country { get; set; }

        [RegularExpression("(^[0-9- + ( )]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(24)]
        [Display(Name = "Teléfono")]
        public string HomePhone { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(4)]
        [Display(Name = "Extensión")]
        public string Extension { get; set; }

        [Display(Name = "Foto")]
        public byte[] Photo { get; set; }

        [Display(Name = "Nota")]
        public string Notes { get; set; }

        [Display(Name = "Superior")]
        public int? ReportsTo { get; set; }

        [StringLength(255)]
        [Display(Name = "Dirección de la Foto")]
        public string PhotoPath { get; set; }

        public bool? Eliminado { get; set; }
        public byte[] RowVersion { get; set; }
        public string informa { get; set; }
    }
}
