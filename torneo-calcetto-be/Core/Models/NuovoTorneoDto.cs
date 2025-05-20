using torneo_calcetto.EF.Models;

namespace torneo_calcetto_be.Core.Models
{
    public class NuovoTorneoDto
    {
        public int IdPk { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descrizione { get; set; } = string.Empty;

        public DateTime DataCreazione { get; set; }

        public int NumeroPartecipanti { get; set; }

        public TipoTorneo TipoTorneo { get; set; }
    }
}
