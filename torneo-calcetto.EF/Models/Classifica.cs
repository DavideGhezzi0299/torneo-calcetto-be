using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Classifica
    {
        public int IdPk { get; set; }
        public int PuntiFatti { get; set; }
        public int GolFatti { get; set; }
        public int GolSubiti { get; set; }
        public int PartiteGiocate { get; set; }
        public int Vittorie { get; set; }
        public int Sconfitte { get; set; }
        public int Pareggi { get; set; }
        public int DifferenzaReti { get; set; }
        public Fascia Fascia { get; set; }
        public int FkIdTorneo { get; set; }
        public int FkIdSquadra { get; set; }
        public string Girone { get; set; }
        public virtual Squadra SquadraNavigation { get; set; }
        public virtual Torneo TorneoNavigation { get; set; }

    }
}
