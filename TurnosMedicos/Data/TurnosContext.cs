using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Models;

    public class TurnosContext : Microsoft.EntityFrameworkCore.DbContext
{
        public TurnosContext(DbContextOptions<TurnosContext> options)
            : base(options)
        {
        }

        public DbSet<TurnosMedicos.Models.Especialidad> Especialidad { get; set; } = default!;

        public DbSet<TurnosMedicos.Models.Medico> Medico { get; set; }

        public DbSet<TurnosMedicos.Models.Turno> Turno { get; set; }

        public DbSet<TurnosMedicos.Models.Usuario> Usuario { get; set; }
}
