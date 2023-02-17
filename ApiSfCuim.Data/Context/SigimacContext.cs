using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSfCuim.Data.Context
{
    public class SigimacContext : DbContext
    {
        public SigimacContext (DbContextOptions <SigimacContext> options) :base (options) { }

        public virtual DbSet<Filter> Filters { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<Reference> References { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filter>(Entity =>
            {
                Entity.HasKey(e => e.IdTmpArma);
                Entity.ToView("SF_VIEW_FILTRO_REFERENCIA");
                Entity.Property(e => e.FechaFiltro);
                Entity.Property(e => e.Filtro).HasMaxLength(50);
                Entity.Property(e => e.IdOperadorFiltro).HasMaxLength(5);
                Entity.Property(e => e.TipoOperadorFiltro).HasMaxLength(5);
                Entity.Ignore(e => e.Operador);
            });

            modelBuilder.Entity<Observation>(Entity =>
            {
                Entity.HasKey(e => e.IdTmpArma);
                Entity.ToView("SF_VIEW_OBSER_REFERENCIA");
                Entity.Property(e => e.FechaObservation);
                Entity.Property(e => e.Observacion).HasMaxLength(500);
            });

            modelBuilder.Entity<Reference>(Entity =>
            {
                Entity.HasKey(e => e.IdTmpArma);
                Entity.ToTable("SF_VIEW_CONSULTA_REFERENCIA");
                Entity.Property(e => e.IdDescArmaFK);
                Entity.Property(e => e.NumeroSerie);
                Entity.Property(e => e.IdDescripcionArma);
                Entity.Property(e => e.Estado);
                Entity.Property(e => e.TipoArma);
                Entity.Property(e => e.Marca);
                Entity.Property(e => e.Modelo);
                Entity.Property(e => e.Calibre);
                Entity.Property(e => e.Medida);
                Entity.Property(e => e.Clase);
            });

        }

    }
}
