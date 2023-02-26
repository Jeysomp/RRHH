#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RRHH.Models;

namespace RRHH.Data
{
    public class RRHHContext : DbContext
    {
        public RRHHContext (DbContextOptions<RRHHContext> options)
            : base(options)
        {
        }

        public DbSet<RRHH.Models.Empleado> Empleado { get; set; }
        public DbSet<RRHH.Models.EmpleadoArea> EmpleadoArea { get; set; }
        public DbSet<RRHH.Models.EmpleadoCargo> EmpleadoCargo { get; set; }
        public DbSet<RRHH.Models.EmpleadoEstado> EmpleadoEstado { get; set; }
        public DbSet<RRHH.Models.EmpleadoTipo> EmpleadoTipo { get; set; }
        public DbSet<RRHH.Models.PlanillaConceptoTipo> PlanillaConceptoTipo { get; set; }
        public DbSet<RRHH.Models.PlanillaConcepto> PlanillaConcepto { get; set; }
    }
}
