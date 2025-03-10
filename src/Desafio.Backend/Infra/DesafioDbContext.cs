using Desafio.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Infra
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeEntity> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeEntity>(entity =>
            {
                entity.ToTable("employees");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.first_name).IsRequired();
                entity.Property(e => e.last_name).IsRequired();
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.birthday);
                entity.Property(e => e.manager_id);

                entity.HasOne(e => e.manager)
                      .WithMany(e => e.subordinates)
                      .HasForeignKey(e => e.manager_id)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}