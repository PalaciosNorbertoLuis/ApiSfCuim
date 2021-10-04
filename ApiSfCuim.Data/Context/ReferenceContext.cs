using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Data.Context
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext (DbContextOptions <ReferenceContext> options) : base(options) { }

        public virtual DbSet <Reference> References { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            });
        }
    }
}
