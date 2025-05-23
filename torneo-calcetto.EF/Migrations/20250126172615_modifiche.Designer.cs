﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using torneo_calcetto.EF.Context;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    [DbContext(typeof(TorneoCalcettoContext))]
    [Migration("20250126172615_modifiche")]
    partial class modifiche
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("torneo_calcetto.EF.Models.Partita", b =>
                {
                    b.Property<int>("IdPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPk"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FaseEliminatoria")
                        .HasColumnType("bit");

                    b.Property<int>("FkIdSquadraCasa")
                        .HasColumnType("int");

                    b.Property<int>("FkIdTorneo")
                        .HasColumnType("int");

                    b.Property<int>("FkIdTrasferta")
                        .HasColumnType("int");

                    b.Property<int>("Giornata")
                        .HasColumnType("int");

                    b.Property<int>("GolCasa")
                        .HasColumnType("int");

                    b.Property<int>("GolOspite")
                        .HasColumnType("int");

                    b.Property<string>("SquadraCasa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SquadraOspite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPartita")
                        .HasColumnType("int");

                    b.Property<int>("TorneoNavigationIdPk")
                        .HasColumnType("int");

                    b.Property<string>("Vincitore")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPk");

                    b.HasIndex("FkIdSquadraCasa");

                    b.HasIndex("FkIdTrasferta");

                    b.HasIndex("TorneoNavigationIdPk");

                    b.ToTable("Partite");
                });

            modelBuilder.Entity("torneo_calcetto.EF.Models.Squadra", b =>
                {
                    b.Property<int>("IdPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPk"));

                    b.Property<int>("DifferenzaReti")
                        .HasColumnType("int");

                    b.Property<int>("Fascia")
                        .HasColumnType("int");

                    b.Property<int>("GolFatti")
                        .HasColumnType("int");

                    b.Property<int>("GolSubiti")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pareggi")
                        .HasColumnType("int");

                    b.Property<int>("PartiteGiocate")
                        .HasColumnType("int");

                    b.Property<int>("PuntiFatti")
                        .HasColumnType("int");

                    b.Property<int>("Sconfitte")
                        .HasColumnType("int");

                    b.Property<int>("Vittorie")
                        .HasColumnType("int");

                    b.HasKey("IdPk");

                    b.ToTable("Squadre");
                });

            modelBuilder.Entity("torneo_calcetto.EF.Models.Torneo", b =>
                {
                    b.Property<int>("IdPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPk"));

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPk");

                    b.ToTable("Tornei");
                });

            modelBuilder.Entity("torneo_calcetto.EF.Models.Partita", b =>
                {
                    b.HasOne("torneo_calcetto.EF.Models.Squadra", "SquadraCasaNavigation")
                        .WithMany()
                        .HasForeignKey("FkIdSquadraCasa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("torneo_calcetto.EF.Models.Squadra", "SquadraTrasfertaNavigation")
                        .WithMany()
                        .HasForeignKey("FkIdTrasferta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("torneo_calcetto.EF.Models.Torneo", "TorneoNavigation")
                        .WithMany()
                        .HasForeignKey("TorneoNavigationIdPk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SquadraCasaNavigation");

                    b.Navigation("SquadraTrasfertaNavigation");

                    b.Navigation("TorneoNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
