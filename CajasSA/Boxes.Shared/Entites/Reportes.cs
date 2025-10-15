using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Reportes
{
    public int Id { get; set; }

    [Display(Name = "Fecha de reporte")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime ReportDate { get; set; }

    [Display(Name = "Fecha de entrada")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime EntryDate { get; set; }

    [Display(Name = "Fecha de salida")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime DepartureDate { get; set; }
}