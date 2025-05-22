using torneo_calcetto.EF.Models;

namespace torneo_calcetto_be.Core.Models
{
    public class GironeDto
    {
        public int IdPk { get; set; }
        public int FkIdTorneo { get; set; }
        public List<SquadraDto> Squadre { get; set; } = new List<SquadraDto>();
        public List<PartitaDto> Partite { get; set; } = new List<PartitaDto>();

    }

    public class SquadraDto
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
        public string? Nome { get; set; }
        public int FkIdGirone { get; set; }
    }

    public class PartitaDto
    {

    }
}
