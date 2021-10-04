using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Data.Context
{
    public class ObservationContext : DbContext
    {
        public ObservationContext(DbContextOptions<ObservationContext> options) : base(options) { }

        public virtual DbSet<Observation> Observations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Observation>(Entity =>
            {
                Entity.HasKey(e => e.IdTmpArma);
                Entity.ToView("SF_VIEW_OBSER_REFERENCIA");
                Entity.Property(e => e.Fecha);
                Entity.Property(e => e.Observacion).HasMaxLength(500);
            });
        }

    }
}
