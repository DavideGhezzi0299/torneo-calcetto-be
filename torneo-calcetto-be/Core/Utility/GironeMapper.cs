using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Models;

namespace torneo_calcetto_be.Core.Utility
{
    public static class GironeMapper
    {
        public static Girone GironeDtoToEntity(GironeDto gironeDto)
        {
            if (gironeDto == null) 
                return null;

            var entity = new Girone();
            entity.FkIdTorneo = gironeDto.FkIdTorneo;
            if (gironeDto.Partite.Count != 0)
            {
                for (int i = 0; i < gironeDto.Partite.Count; i++)
                {
                    entity.Partite.Add(PartitaDtoToEntity(gironeDto.Partite[i]));
                }
            }
            if(entity.Squadre.Count != 0)
            {
                for(int i = 0;i < entity.Squadre.Count; i++)
                {
                    entity.Squadre.Add(SquadraDtoToEntity(gironeDto.Squadre[i]));
                }
            }
            return entity;
        }

        public static Partita PartitaDtoToEntity(PartitaDto partitaDto) {
            if (partitaDto == null)
                return null;
            var entity = new Partita();
            return entity;
        }

        public static Squadra SquadraDtoToEntity(SquadraDto squadraDto) {
            if (squadraDto == null)
                return null;
            var entity = new Squadra
            {
                Nome = squadraDto.Nome ?? string.Empty,  // se vuoi gestire il null
                PuntiFatti = squadraDto.PuntiFatti,
                GolFatti = squadraDto.GolFatti,
                GolSubiti = squadraDto.GolSubiti,
                PartiteGiocate = squadraDto.PartiteGiocate,
                Vittorie = squadraDto.Vittorie,
                Sconfitte = squadraDto.Sconfitte,
                Pareggi = squadraDto.Pareggi,
                DifferenzaReti = squadraDto.DifferenzaReti,
                FkIdGirone = squadraDto.FkIdGirone
                // Fascia e GironeNavigation NON li mappiamo qui, vanno risolti a parte
            };

            return entity;

        }
    }
}
