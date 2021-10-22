using ApiSfCuim.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Data.Context
{
    public class RenarAuxiliarContext: DbContext
    {
        public RenarAuxiliarContext (DbContextOptions<RenarAuxiliarContext> options) : base(options) { }

        public DbSet<Operator> Operators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>(Entity =>
            {
                Entity.HasKey(e => e.id_usuario);
                Entity.ToTable("EXT_OPERADOR");
                Entity.Property(e => e.login_id).HasMaxLength(20);
            });
        }

    }
}
