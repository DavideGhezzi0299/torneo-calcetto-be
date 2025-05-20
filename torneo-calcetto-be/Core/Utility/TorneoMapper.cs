using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Models;

namespace torneo_calcetto_be.Core.Utility
{
    public static class TorneoMapper
    {
        public static Torneo DtoToEntity(NuovoTorneoDto dto)
        {
            if (dto == null) return null;

            return new Torneo
            {
                IdPk = dto.IdPk,
                Nome = dto.Nome,
                Descrizione = dto.Descrizione,
                DataCreazione = dto.DataCreazione,
                NumeroPartecipanti = dto.NumeroPartecipanti,
                TipoTorneo = dto.TipoTorneo,
                FkIdUtenteCreatore = 1
            };
        }
    }
}
