using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EvalucacionTecnica.DAL.Entities
{
    public partial class AgendaContext : DbContext
    {
        public AgendaContext()
        {
        }

        public AgendaContext(DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InfoModel> DboInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoModel>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PRIMARY");

                entity.ToTable("dbo.info");

                entity.Property(e => e.IdPersona)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_persona");

                entity.Property(e => e.nombre)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("nombre");

                entity.Property(e => e.CiudadEstado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ciudad_estado");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(17)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
