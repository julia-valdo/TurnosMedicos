using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace TurnosMedicos.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(50, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(50, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Rol { get; set; } = "";

        public ICollection<Turno>? Turnos { get; set; }
    }
}
