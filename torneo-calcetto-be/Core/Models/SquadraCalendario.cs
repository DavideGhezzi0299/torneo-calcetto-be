using torneo_calcetto.EF.Models;

namespace torneo_calcetto_be.Core.Models
{
    public class SquadraCalendario
    {
        public string Nome { get; set; }
        public bool HaGiocato { get; set; }
        public string Risultato { get; set; }
        public bool IsCasa { get; set; }
        public int Fascia { get; set; }
    }
}
