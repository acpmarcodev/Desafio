﻿// <auto-generated />
using System;
using Desafio.Backend.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Desafio.Backend.Migrations
{
    [DbContext(typeof(DesafioDbContext))]
    partial class DesafioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Desafio.Backend.Domain.EmployeeEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("document")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("manager_id")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<string[]>("phones")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("role_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("manager_id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("Desafio.Backend.Domain.EmployeeEntity", b =>
                {
                    b.HasOne("Desafio.Backend.Domain.EmployeeEntity", "manager")
                        .WithMany("subordinates")
                        .HasForeignKey("manager_id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("manager");
                });

            modelBuilder.Entity("Desafio.Backend.Domain.EmployeeEntity", b =>
                {
                    b.Navigation("subordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
