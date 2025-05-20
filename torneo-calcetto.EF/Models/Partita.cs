using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Partita
    {
        public Partita()
        {
            Data = DateTime.Now;
            TipoPartita = TipoPartita.Girone;
            GolCasa = 0;
            GolOspite = 0;
            Vincitore = null;
            FaseEliminatoria = false;
            TipoPartita = TipoPartita.Girone;
        }
        public Partita(Partita row, Squadra casa, Squadra trasferta)
        {
            if (row.Vincitore is not null)
            {
                if (row.Vincitore == casa.Nome)
                {
                    IdVincitore = casa.IdPk;
                }
                else
                {
                    IdVincitore = trasferta.IdPk;
                }
            }
            IdPk = row.IdPk;
            SquadraCasa = casa.Nome;
            SquadraOspite = trasferta.Nome;
            GolCasa = row.GolCasa;
            GolOspite = row.GolOspite;
            Data = row.Data;
            Vincitore = row.Vincitore;
            FaseEliminatoria = row.FaseEliminatoria;
            IdSquadraCasa = casa.IdPk;
            IdSquadraOspite = trasferta.IdPk;
            Giornata = row.Giornata;
        }
        [Key]
        public int IdPk { get; set; }
        public string? SquadraCasa { get; set; }
        public string? SquadraOspite { get; set; }
        public int GolCasa { get; set; }
        public int GolOspite { get; set; }
        public DateTime Data { get; set; }
        public string? Vincitore { get; set; }
        public bool FaseEliminatoria { get; set; }
        public TipoPartita TipoPartita { get; set; }
        public int FkIdGirone { get; set; }
        public Girone GironeNavigation { get; set; }
        public int FkIdSquadraCasa { get; set; }
        public int FkIdTrasferta { get; set; }
        public virtual Squadra SquadraCasaNavigation { get; set; }
        public virtual Squadra SquadraTrasfertaNavigation { get; set; }
        public int Giornata { get; set; }
        [NotMapped]
        public int IdSquadraCasa { get; set; }
        [NotMapped]
        public int IdSquadraOspite { get; set; }
        [NotMapped]
        public int IdVincitore { get; set; }
        [NotMapped]
        public bool Modifica { get; set; }
    }

    public enum TipoPartita
    {
        Girone,
        QuartiFinale,
        Semifinale,
        Finale,
        FinaleTerzoPosto
    }

}
