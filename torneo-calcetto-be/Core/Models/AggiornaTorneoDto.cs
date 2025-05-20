using torneo_calcetto.EF.Models;

namespace torneo_calcetto_be.Core.Models
{
    public class AggiornaTorneoDto
    {
        public List<Girone> Gironi { get; set; }
        public List<Squadra> Partecipanti { get; set; }

    }
}
