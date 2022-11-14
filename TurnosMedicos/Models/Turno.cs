using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurnosMedicos.Models
{
    [Table("Turno")]
    public class Turno
    {
        [Key]
        public int TurnoId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
    }
}
