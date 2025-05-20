using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Squadra
    {
        [Key]
        public int IdPk { get; set; }
        public string Nome { get; set; }
        public int PuntiFatti { get; set; }
        public int GolFatti { get; set; }
        public int GolSubiti { get; set; }
        public int PartiteGiocate { get; set; }
        public int Vittorie { get; set; }
        public int Sconfitte { get; set; }
        public int Pareggi { get; set; }
        public int DifferenzaReti { get; set; }
        public Fascia Fascia { get; set; }
        public int FkIdGirone { get; set; }
        public Girone GironeNavigation { get; set; }
    }

    public enum Fascia
    {
        Prima = 1,
        Seconda = 2,
        Terza = 3,
        Quarta = 4,
        Quinta = 5
    }
}
