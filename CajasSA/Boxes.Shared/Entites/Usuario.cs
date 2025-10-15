using Boxes.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites
{
    public class Usuario : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "el nombre dene tener entre 4 y 30 caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "contraseña insuficiente")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Display(Name = "Telefono")]
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [ForeignKey("Rol")]
        public int RolId { get; set; }

        // navigation property
        public Rol? Rol { get; set; } = null!;
    }
}