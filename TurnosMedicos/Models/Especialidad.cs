using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurnosMedicos.Models
{
    [Table("Especialidad")]
    public class Especialidad
    {
        [Key]
        public int EspecialidadId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(50, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; } = "";

        public ICollection<Medico>? Medicos { get; set; }
    }
}
