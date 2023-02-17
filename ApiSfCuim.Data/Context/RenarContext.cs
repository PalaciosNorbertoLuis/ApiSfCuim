using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSfCuim.Data.Context
{
    public class RenarContext : DbContext
    {
        public RenarContext (DbContextOptions <RenarContext> options) : base(options) { }

        public virtual DbSet <Consult> Consults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consult>(Entity => 
            {
                Entity.HasKey(e => e.IdDescripcionArma);
                Entity.ToView("SF_VIEW_CONSULTA_ARMA");
                Entity.Property(e => e.TipoArma).HasMaxLength(50);
                Entity.Property(e => e.Marca).HasMaxLength(30);
                Entity.Property(e => e.Modelo).HasMaxLength(20);
                Entity.Property(e => e.Medida).HasMaxLength(20);
                Entity.Property(e => e.Calibre).HasMaxLength(30);
                Entity.Property(e => e.Clase).HasMaxLength(30);
            });
        } 
    }
}
