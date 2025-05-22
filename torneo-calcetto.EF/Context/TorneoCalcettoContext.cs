using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using torneo_calcetto.EF.Models;

namespace torneo_calcetto.EF.Context
{
    public class TorneoCalcettoContext : DbContext
    {
        public TorneoCalcettoContext(DbContextOptions<TorneoCalcettoContext> options) : base(options)
        {
        }

        public DbSet<Partita> Partite { get; set; }
        public DbSet<Squadra> Squadre { get; set; }

        public DbSet<Torneo> Tornei { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Girone> Gironi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partita>()
                .HasOne(p => p.SquadraCasaNavigation)
                .WithMany()
                .HasForeignKey(p => p.FkIdSquadraCasa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partita>()
                .HasOne(p => p.SquadraTrasfertaNavigation)
                .WithMany()
                .HasForeignKey(p => p.FkIdTrasferta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Squadra>()
                .HasOne(p => p.GironeNavigation)
                .WithMany(g => g.Squadre)
                .HasForeignKey(p => p.FkIdGirone)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partita>()
                .HasOne(p => p.GironeNavigation)
                .WithMany(g => g.Partite)
                .HasForeignKey(p => p.FkIdGirone)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Girone>()
                .HasOne(g => g.TorneoNavigation)
                .WithMany(t => t.Gironi)
                .HasForeignKey(g => g.FkIdTorneo)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
