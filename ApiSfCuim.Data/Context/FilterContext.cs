using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Data.Context
{
    public class FilterContext : DbContext 
    {
        public FilterContext (DbContextOptions <FilterContext> options) : base(options) { }

        public virtual DbSet<Filter> Filters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filter>(Entity =>
          {
              Entity.HasKey(e => e.IdTmpArma);
              Entity.ToView("SF_VIEW_FILTRO_REFERENCIA");
              Entity.Property(e => e.FechaFiltro);
              Entity.Property(e => e.Filtro).HasMaxLength(50);
              Entity.Property(e => e.IdOperadorFiltro).HasMaxLength(5);
              Entity.Property(e => e.IdOperadorFiltro).HasMaxLength(5);
          });
        }
    }
}
