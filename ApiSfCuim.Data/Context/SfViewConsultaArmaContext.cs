using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Data.Context
{
    public class SfViewConsultaArmaContext : DbContext 
    {
        public SfViewConsultaArmaContext(DbContextOptions<SfViewConsultaArmaContext> options) : base(options) { }

        public virtual DbSet<SfViewConsultaArma> SF_VIEW_CONSULTA_ARMA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SfViewConsultaArma>(entity => {
                entity.HasKey(e => e.IdDescripcionArma);
                entity.ToTable("SF_VIEW_CONSULTA_ARMA");
                entity.Property(e => e.TipoArma).HasMaxLength(50);
                entity.Property(e => e.Modelo).HasMaxLength(20);
                entity.Property(e => e.Medida).HasMaxLength(20);
                entity.Property(e => e.Calibre).HasMaxLength(30);
                entity.Property(e => e.Clase).HasMaxLength(30);

            });
        }

    }
}
