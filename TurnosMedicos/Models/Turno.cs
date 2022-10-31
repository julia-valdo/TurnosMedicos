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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Hora { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
    }
}
