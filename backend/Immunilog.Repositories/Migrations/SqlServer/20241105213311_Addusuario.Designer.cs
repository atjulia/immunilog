﻿// <auto-generated />
using System;
using Immunilog.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Immunilog.Repositories.Migrations.SqlServer
{
    [DbContext(typeof(SqlServerDbContext))]
    [Migration("20241105213311_Addusuario")]
    partial class Addusuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Immunilog.Domain.Entities.Doenca", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doenca");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DtCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DtUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UltimoAcesso")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Vacina", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DtUpdate")
                        .HasColumnType("datetime2");

                    b.Property<float>("IdadeRecomendada")
                        .HasColumnType("real");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCalendario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDoseObs")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vacina");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.VacinaDoenca", b =>
                {
                    b.Property<Guid>("VacinaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoencaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VacinaId", "DoencaId");

                    b.HasIndex("DoencaId");

                    b.ToTable("VacinaDoencas");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.VacinaPessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DtAplicacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtUpdate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReacaoOutros")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VacinaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("VacinaPessoa");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Pessoa", b =>
                {
                    b.HasOne("Immunilog.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.VacinaDoenca", b =>
                {
                    b.HasOne("Immunilog.Domain.Entities.Doenca", "Doenca")
                        .WithMany("VacinaDoencas")
                        .HasForeignKey("DoencaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Immunilog.Domain.Entities.Vacina", "Vacina")
                        .WithMany("VacinaDoencas")
                        .HasForeignKey("VacinaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doenca");

                    b.Navigation("Vacina");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.VacinaPessoa", b =>
                {
                    b.HasOne("Immunilog.Domain.Entities.Pessoa", null)
                        .WithMany("Vacinas")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Doenca", b =>
                {
                    b.Navigation("VacinaDoencas");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Pessoa", b =>
                {
                    b.Navigation("Vacinas");
                });

            modelBuilder.Entity("Immunilog.Domain.Entities.Vacina", b =>
                {
                    b.Navigation("VacinaDoencas");
                });
#pragma warning restore 612, 618
        }
    }
}