using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurnosMedicos.Models
{
    [Table("Medico")]
    public class Medico
    {
        [Key]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Matricula { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(50, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(50, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Apellido { get; set; } = "";

        public int EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }

        public ICollection<Turno>? Turnos { get; set; }
    }
}
