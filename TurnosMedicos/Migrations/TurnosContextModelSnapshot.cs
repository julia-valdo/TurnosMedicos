﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TurnosMedicos.Migrations
{
    [DbContext(typeof(TurnosContext))]
    partial class TurnosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("TurnosMedicos.Models.Especialidad", b =>
                {
                    b.Property<int>("EspecialidadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("EspecialidadId");

                    b.ToTable("Especialidad");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Medico", b =>
                {
                    b.Property<int>("MedicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EspecialidadId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Matricula")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("MedicoId");

                    b.HasIndex("EspecialidadId");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Turno", b =>
                {
                    b.Property<int>("TurnoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<int>("MedicoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TurnoId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Medico", b =>
                {
                    b.HasOne("TurnosMedicos.Models.Especialidad", "Especialidad")
                        .WithMany("Medicos")
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Turno", b =>
                {
                    b.HasOne("TurnosMedicos.Models.Medico", "Medico")
                        .WithMany("Turnos")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TurnosMedicos.Models.Usuario", "Usuario")
                        .WithMany("Turnos")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Medico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Especialidad", b =>
                {
                    b.Navigation("Medicos");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Medico", b =>
                {
                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("TurnosMedicos.Models.Usuario", b =>
                {
                    b.Navigation("Turnos");
                });
#pragma warning restore 612, 618
        }
    }
}
