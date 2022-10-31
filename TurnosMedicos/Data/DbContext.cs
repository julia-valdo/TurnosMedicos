using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Models;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<TurnosMedicos.Models.Especialidad> Especialidad { get; set; } = default!;

        public DbSet<TurnosMedicos.Models.Medico> Medico { get; set; }

        public DbSet<TurnosMedicos.Models.Turno> Turno { get; set; }
    }
