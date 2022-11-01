using System.ComponentModel.DataAnnotations;

namespace TurnosMedicos.ModelsView
{
    public class GeneradorTurno
    {
        public int MedicoId { get; set; }
        public DateTime FechaDesde { get; set; }
        public int CantidadTurnos { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }
    }
}
