using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using torneo_calcetto.EF.Context;
using torneo_calcetto.EF.Models;
using torneo_calcetto_be.Core.Models;

namespace torneo_calcetto_be.Core.Repository
{
    public class PartiteRepository
    {
        private readonly TorneoCalcettoContext _context;
        public PartiteRepository(TorneoCalcettoContext context)
        {
            _context = context;
        }
        public async Task<List<Partita>> GetPartite()
        {
            // TODO: Implement the logic to retrieve the list of Partite
            return await (from row in _context.Partite
                          join squadra1 in _context.Squadre on row.SquadraCasa equals squadra1.Nome
                          join squadra2 in _context.Squadre on row.SquadraOspite equals squadra2.Nome
                          select new Partita(row, squadra1, squadra2))
                          .ToListAsync();
        }
        public async Task<List<Partita>> GeneraCalendarioGirone()
        {
            List<Partita> calendario = new List<Partita>();
            var random = new Random();
            var tutteLeSquadre = await _context.Squadre
           .OrderBy(x => Guid.NewGuid()) // Genera un GUID casuale per ogni riga
           .ToListAsync();


            var numeroGiornate = 5;
            for (int giornata = 1; giornata <= numeroGiornate; giornata++)
            {
                // Insieme per tenere traccia delle squadre già utilizzate in questa giornata
                var squadreUsateInGiornata = new HashSet<string>();
                foreach (var squadra in tutteLeSquadre)
                {
                    // Salta la squadra se è già stata utilizzata in questa giornata
                    if (squadreUsateInGiornata.Contains(squadra.Nome))
                    {
                        continue;
                    }

                    // Trova le squadre che la squadra può affrontare
                    var squadreChePuoAffrontare = tutteLeSquadre.Where(avversario =>
                        avversario.IdPk != squadra.IdPk && // Esclude sé stessa
                        !squadreUsateInGiornata.Contains(avversario.Nome) && // Esclude squadre già utilizzate in questa giornata
                        !calendario.Any(partita =>
                            (partita.SquadraCasa == squadra.Nome && partita.SquadraOspite == avversario.Nome) ||
                            (partita.SquadraCasa == avversario.Nome && partita.SquadraOspite == squadra.Nome)
                        ) &&
                        // Nuovo criterio: controlla che l'avversario non abbia già affrontato squadre della stessa fascia
                        !calendario.Any(partita =>
                            (partita.SquadraCasa == avversario.Nome && _context.Squadre.First(x => x.Nome == partita.SquadraOspite).Fascia == squadra.Fascia) ||
                            (partita.SquadraOspite == avversario.Nome && _context.Squadre.First(x => x.Nome == partita.SquadraCasa).Fascia == squadra.Fascia)
                        ) &&
                        // Esclude squadre della stessa fascia che la squadra corrente ha già affrontato
                        !calendario.Any(partita =>
                            (partita.SquadraCasa == squadra.Nome && _context.Squadre.First(x => x.Nome == partita.SquadraOspite).Fascia == avversario.Fascia) ||
                            (partita.SquadraOspite == squadra.Nome && _context.Squadre.First(x => x.Nome == partita.SquadraCasa).Fascia == avversario.Fascia)
                        )
                    ).ToList();

                    if (!squadreChePuoAffrontare.Any())
                    {
                        // Nessuna squadra disponibile da affrontare, salta la squadra
                        continue;
                    }

                    // Seleziona casualmente un avversario dalla lista filtrata
                    var avversarioSelezionato = squadreChePuoAffrontare[random.Next(squadreChePuoAffrontare.Count)];

                    // Aggiungi la partita al calendario
                    calendario.Add(new Partita
                    {
                        SquadraCasa = squadra.Nome,
                        SquadraOspite = avversarioSelezionato.Nome,
                        Giornata = giornata
                    });

                    // Aggiorna le squadre usate in questa giornata
                    squadreUsateInGiornata.Add(squadra.Nome);
                    squadreUsateInGiornata.Add(avversarioSelezionato.Nome);
                }
            }
            _context.Partite.AddRange(calendario);
            await _context.SaveChangesAsync();

            var partiteFase2DaAggiungere = new List<Partita>();
            var tutteLePartite = await _context.Partite.ToListAsync();
            // Prendi tutte le squadre che hanno giocato meno di 5 partite
            var squadreCheHannoGiocatoMenoDi5Partite = tutteLeSquadre
                .Where(squadra =>
                    tutteLePartite.Count(partita =>
                        partita.SquadraCasa == squadra.Nome || partita.SquadraOspite == squadra.Nome
                    ) < 5
                )
                .ToList();

            // Raggruppa il calendario per giornata e trova le giornate con meno di 5 partite
            var giornateConMenoDi5Partite = tutteLePartite
                .GroupBy(partita => partita.Giornata)  // Raggruppa per Giornata
                .Where(g => g.Count() < 5)  // Filtra le giornate con meno di 5 partite
                .Select(g => g.Key)  // Prendi solo il numero della giornata
                .ToList();

            foreach (var squadraConMenoDi5Partite in squadreCheHannoGiocatoMenoDi5Partite)
            {
                // Trova le squadre che non hanno partecipato a questa giornata
                var squadreCheNonHannoPartecipatoInGiornata = squadreCheHannoGiocatoMenoDi5Partite
                    .Where(squadra =>
                       //!squadreUsateInGiornata2.Contains(squadra.Nome) &&
                       squadra.Nome != squadraConMenoDi5Partite.Nome
                    )
                    .ToList();

                // Trova le squadre che la squadra può affrontare
                var squadreChePuoAffrontareFase2 = squadreCheNonHannoPartecipatoInGiornata.Where(avversarioConMenoDi5Partite =>
                    avversarioConMenoDi5Partite.IdPk != squadraConMenoDi5Partite.IdPk && // Esclude sé stessa
                                                                                         //!squadreUsateInGiornata2.Contains(avversarioConMenoDi5Partite.Nome) && // Esclude squadre già utilizzate in questa giornata
                    !tutteLePartite.Any(partitaFase2 =>
                        (partitaFase2.SquadraCasa == squadraConMenoDi5Partite.Nome && partitaFase2.SquadraOspite == avversarioConMenoDi5Partite.Nome) ||
                        (partitaFase2.SquadraCasa == avversarioConMenoDi5Partite.Nome && partitaFase2.SquadraOspite == squadraConMenoDi5Partite.Nome)
                    ) &&
                    // Nuovo criterio: controlla che l'avversario non abbia già affrontato squadre della stessa fascia
                    !tutteLePartite.Any(partitaFase2 =>
                        (partitaFase2.SquadraCasa == avversarioConMenoDi5Partite.Nome && _context.Squadre.First(x => x.Nome == partitaFase2.SquadraOspite).Fascia == squadraConMenoDi5Partite.Fascia) ||
                        (partitaFase2.SquadraOspite == avversarioConMenoDi5Partite.Nome && _context.Squadre.First(x => x.Nome == partitaFase2.SquadraCasa).Fascia == squadraConMenoDi5Partite.Fascia)
                    ) &&
                    // Esclude squadre della stessa fascia che la squadra corrente ha già affrontato
                    !tutteLePartite.Any(partitaFase2 =>
                        (partitaFase2.SquadraCasa == squadraConMenoDi5Partite.Nome && _context.Squadre.First(x => x.Nome == partitaFase2.SquadraOspite).Fascia == avversarioConMenoDi5Partite.Fascia) ||
                        (partitaFase2.SquadraOspite == squadraConMenoDi5Partite.Nome && _context.Squadre.First(x => x.Nome == partitaFase2.SquadraCasa).Fascia == avversarioConMenoDi5Partite.Fascia)
                    )
                ).ToList();

                if (!squadreChePuoAffrontareFase2.Any())
                {
                    // Nessuna squadra disponibile da affrontare, salta la squadra
                    continue;
                }

                // Seleziona casualmente un avversario dalla lista filtrata
                var avversarioSelezionatoFase2 = squadreChePuoAffrontareFase2[random.Next(squadreChePuoAffrontareFase2.Count)];

                if (partiteFase2DaAggiungere.Where(x => x.SquadraCasa == squadraConMenoDi5Partite.Nome && x.SquadraOspite == avversarioSelezionatoFase2.Nome).Any() ||
                    partiteFase2DaAggiungere.Where(x => x.SquadraCasa == avversarioSelezionatoFase2.Nome && x.SquadraOspite == squadraConMenoDi5Partite.Nome).Any())
                {
                    continue;
                }
                // Aggiungi la partita al calendario
                partiteFase2DaAggiungere.Add(new Partita
                {
                    SquadraCasa = squadraConMenoDi5Partite.Nome,
                    SquadraOspite = avversarioSelezionatoFase2.Nome,
                    Giornata = 0
                });
            }

            _context.Partite.AddRange(partiteFase2DaAggiungere);
            await _context.SaveChangesAsync();

            return partiteFase2DaAggiungere;
        }

        public async Task CalcolaGiornate()
        {
            var tutteLePartite = await _context.Partite.ToListAsync();
            var squadreString = await _context.Squadre.Select(x => x.Nome).ToListAsync();
            var partitePrese = new List<Partita>();
            for (int i = 1; i <= 5; i++)
            {
                var partiteFiltrate = tutteLePartite
                        .Where(p => squadreString.Contains(p.SquadraCasa) || squadreString.Contains(p.SquadraOspite))
                        .Take(5) // Prendi le prime 5 partite
                        .ToList();

                // Imposta il campo Giornata per ogni partita filtrata
                foreach (var partita in partiteFiltrate)
                {
                    partita.Giornata = i; // Imposta Giornata come l'indice corrente del ciclo
                }

                //differenza tra partite prese e tutte
                tutteLePartite = tutteLePartite.Except(partiteFiltrate).ToList();

                _context.Partite.UpdateRange(partiteFiltrate);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<SquadraCalendario>> GetAvversariPerSquadra(int idSquadra)
        {
            var squadra = await _context.Squadre.FirstOrDefaultAsync(x => x.IdPk == idSquadra);
            var partite = await _context.Partite.Where(x => (x.SquadraCasa == squadra.Nome || x.SquadraOspite == squadra.Nome) && !x.FaseEliminatoria).ToListAsync();
            var avversari = new List<SquadraCalendario>();
            foreach (var partita in partite)
            {
                if (partita.SquadraCasa == squadra.Nome)
                {
                    var avversario = await _context.Squadre.FirstOrDefaultAsync(x => x.Nome == partita.SquadraOspite);
                    var avversarioDto = new SquadraCalendario
                    {
         
                        Nome = avversario.Nome,
                        HaGiocato = partita.Vincitore != null,
                        Risultato = $"{partita.GolCasa} - {partita.GolOspite}",
                        IsCasa = false, //avversario gioca in trasferta se la squadra gioca in casa
                        Fascia = (int)avversario.Fascia
                    };
                    avversari.Add(avversarioDto);
                }
                else
                {
                    var avversario = await _context.Squadre.FirstOrDefaultAsync(x => x.Nome == partita.SquadraCasa);
                    var avversarioDto = new SquadraCalendario
                    {
             
                        Nome = avversario.Nome,
                        HaGiocato = partita.Vincitore != null,
                        Risultato = $"{partita.GolOspite} - {partita.GolCasa}",
                        IsCasa = true, //avversario gioca in casa se la squadra gioca in trasferta
                        Fascia = (int)avversario.Fascia
                    };
                    avversari.Add(avversarioDto);
                }
            }
            return avversari;
        }

        public async Task<Partita> GetPartitaFromNomiSquadra(string squadra, string avversario)
        {
            return await _context.Partite.FirstOrDefaultAsync(x => (x.SquadraCasa == squadra && x.SquadraOspite == avversario) || (x.SquadraCasa == avversario && x.SquadraOspite == squadra));
        }

        public async Task<Partita> InserisciRisultato(Partita partita)
        {
            var partitaDaAggiornare = await _context.Partite.FirstOrDefaultAsync(x => x.IdPk == partita.IdPk);
            if (partitaDaAggiornare == null)
            {
                return null;
            }
            string squadraVincenteString = partita.GolCasa > partita.GolOspite ? partita.SquadraCasa : partita.SquadraOspite;
            string squadraPerdenteString = partita.GolCasa < partita.GolOspite ? partita.SquadraCasa : partita.SquadraOspite;

            partitaDaAggiornare.GolCasa = partita.GolCasa;
            partitaDaAggiornare.GolOspite = partita.GolOspite;
            partitaDaAggiornare.Vincitore = squadraVincenteString;

            //aggiorno i dati delle due squadre per il calcolo della classifica
            var squadraVincente = await _context.Squadre.FirstOrDefaultAsync(x => x.Nome == squadraVincenteString);
            var squadraPerdente = await _context.Squadre.FirstOrDefaultAsync(x => x.Nome == squadraPerdenteString);

            //controllo se la squadra di casa è la vincente per incrementare i punti
            var isSquadraCasaVincente = partita.SquadraCasa == squadraVincenteString;

            squadraVincente.PuntiFatti += 3;
            squadraVincente.GolFatti += isSquadraCasaVincente ? partita.GolCasa : partita.GolOspite;
            squadraVincente.GolSubiti += isSquadraCasaVincente ? partita.GolOspite : partita.GolCasa;
            squadraVincente.PartiteGiocate += 1;
            squadraVincente.Vittorie += 1;
            squadraVincente.DifferenzaReti = squadraVincente.GolFatti - squadraVincente.GolSubiti;

            squadraPerdente.GolFatti += isSquadraCasaVincente ? partita.GolOspite : partita.GolCasa;
            squadraPerdente.GolSubiti += isSquadraCasaVincente ? partita.GolCasa : partita.GolOspite;
            squadraPerdente.PartiteGiocate += 1;
            squadraPerdente.Sconfitte += 1;
            squadraPerdente.DifferenzaReti = squadraPerdente.GolFatti - squadraPerdente.GolSubiti;

            await _context.SaveChangesAsync();
            return partitaDaAggiornare;
        }

        public async Task<Partita> InserisciRisultatoFaseEliminatoria(Partita partita)
        {
            var partitaDaAggiornare = await _context.Partite.FirstOrDefaultAsync(x => x.IdPk == partita.IdPk);
            if (partitaDaAggiornare == null)
            {
                return null;
            }
            string squadraVincenteString = partita.GolCasa > partita.GolOspite ? partita.SquadraCasa : partita.SquadraOspite;
            string squadraPerdenteString = partita.GolCasa < partita.GolOspite ? partita.SquadraCasa : partita.SquadraOspite;

            partitaDaAggiornare.GolCasa = partita.GolCasa;
            partitaDaAggiornare.GolOspite = partita.GolOspite;
            partitaDaAggiornare.Vincitore = squadraVincenteString;

            //creo eventualmente le semifinali o finale
            var classifica = await _context.Squadre.OrderByDescending(x => x.PuntiFatti).ThenByDescending(x => x.DifferenzaReti).ToListAsync();
            //prendo la posizione della squadra vincente
            var posizioneSquadraVincente = classifica.FindIndex(x => x.Nome == squadraVincenteString);
            //vado a prendere la semifinale corretta, se la squadra vincente era la quarta/quinta allora gioca contro la prima altrimenti contro la seconda
            if (partita.TipoPartita == TipoPartita.QuartiFinale)
            {
                if (posizioneSquadraVincente == 3 || posizioneSquadraVincente == 4)
                {
                    var squadraCasa = classifica[0];
                    var squadraOspite = classifica[posizioneSquadraVincente];
                    var partitaSemifinale = await _context.Partite.FirstOrDefaultAsync(x => x.TipoPartita == TipoPartita.Semifinale && x.SquadraCasa == squadraCasa.Nome);
                    if (partitaSemifinale == null)
                    {
                        partitaSemifinale = new Partita
                        {
                            SquadraCasa = squadraCasa.Nome,
                            SquadraOspite = squadraOspite.Nome,
                            Data = DateTime.Now,
                            FaseEliminatoria = true,
                            TipoPartita = TipoPartita.Semifinale
                        };
                        _context.Partite.Add(partitaSemifinale);
                    }
                    partitaSemifinale.SquadraOspite = squadraOspite.Nome;

                }
                else if (posizioneSquadraVincente == 2 || posizioneSquadraVincente == 5)
                {
                    var squadraCasa = classifica[1];
                    var squadraOspite = classifica[posizioneSquadraVincente];
                    var partitaSemifinale = await _context.Partite.FirstOrDefaultAsync(x => x.TipoPartita == TipoPartita.Semifinale && x.SquadraCasa == squadraCasa.Nome);
                    if (partitaSemifinale == null)
                    {
                        partitaSemifinale = new Partita
                        {
                            SquadraCasa = squadraCasa.Nome,
                            SquadraOspite = squadraOspite.Nome,
                            Data = DateTime.Now,
                            FaseEliminatoria = true,
                            TipoPartita = TipoPartita.Semifinale
                        };
                        _context.Partite.Add(partitaSemifinale);
                    }
                    partitaSemifinale.SquadraOspite = squadraOspite.Nome;
                }
            }
            if (partita.TipoPartita == TipoPartita.Semifinale)
            {
                var partitaFinale = await _context.Partite.FirstOrDefaultAsync(x => x.TipoPartita == TipoPartita.Finale);
                var terzoQuartoPosto = await _context.Partite.FirstOrDefaultAsync(x => x.TipoPartita == TipoPartita.FinaleTerzoPosto);
                if (partitaFinale == null)
                {
                    partitaFinale = new Partita
                    {
                        SquadraCasa = squadraVincenteString,
                        SquadraOspite = null,
                        Data = DateTime.Now,
                        FaseEliminatoria = true,
                        TipoPartita = TipoPartita.Finale
                    };
                    _context.Partite.Add(partitaFinale);
                }
                if (terzoQuartoPosto == null)
                {
                    terzoQuartoPosto = new Partita
                    {
                        SquadraCasa = squadraPerdenteString,
                        SquadraOspite = null,
                        Data = DateTime.Now,
                        FaseEliminatoria = true,
                        TipoPartita = TipoPartita.FinaleTerzoPosto
                    };
                    _context.Partite.Add(terzoQuartoPosto);
                }
                if (partitaFinale != null)
                {
                    if (squadraVincenteString == partitaFinale.SquadraCasa)
                        partitaFinale.SquadraCasa = squadraVincenteString;
                    else if (squadraVincenteString == partitaFinale.SquadraOspite)
                        partitaFinale.SquadraOspite = squadraVincenteString;
                    else if (squadraPerdenteString == partitaFinale.SquadraCasa)
                    {
                        partitaFinale.SquadraCasa = squadraVincenteString;
                    }
                    else if (squadraPerdenteString == partitaFinale.SquadraOspite)
                    {
                        partitaFinale.SquadraOspite = squadraVincenteString;
                    }
                    else if (String.IsNullOrEmpty(partitaFinale.SquadraOspite))
                        partitaFinale.SquadraOspite = squadraVincenteString;
                }
                if (terzoQuartoPosto != null)
                {
                    if (squadraPerdenteString == terzoQuartoPosto.SquadraCasa)
                    {
                        terzoQuartoPosto.SquadraCasa = squadraPerdenteString;
                    }
                    else if (squadraPerdenteString == terzoQuartoPosto.SquadraOspite)
                    {
                        terzoQuartoPosto.SquadraOspite = squadraPerdenteString;
                    }
                    else if (squadraVincenteString == terzoQuartoPosto.SquadraCasa)
                    {

                        terzoQuartoPosto.SquadraCasa = squadraPerdenteString;
                    }
                    else if (squadraVincenteString == terzoQuartoPosto.SquadraOspite)
                    {
                        terzoQuartoPosto.SquadraOspite = squadraPerdenteString;
                    }
                    else if (String.IsNullOrEmpty(terzoQuartoPosto.SquadraOspite))
                        terzoQuartoPosto.SquadraOspite = squadraPerdenteString;
                }
            }
            await _context.SaveChangesAsync();
            return partitaDaAggiornare;
        }

        public async Task<Partita> ModificaRisultato(Partita partita)
        {
            //devo aggiornare i dati sulle squadre che sono nella partita oltre ai dati della partita
            var partitaDaAggiornare = await _context.Partite.FirstOrDefaultAsync(x => x.IdPk == partita.IdPk);
            if (partitaDaAggiornare == null)
            {
                return null;
            }

            var squadre = await _context.Squadre.ToListAsync();
            var squadraCasa = squadre.FirstOrDefault(x => x.Nome == partitaDaAggiornare.SquadraCasa);
            var squadraOspite = squadre.FirstOrDefault(x => x.Nome == partitaDaAggiornare.SquadraOspite);

            squadraCasa.GolFatti -= partitaDaAggiornare.GolCasa;
            squadraCasa.GolSubiti -= partitaDaAggiornare.GolOspite;
            squadraOspite.GolFatti -= partitaDaAggiornare.GolOspite;
            squadraOspite.GolSubiti -= partitaDaAggiornare.GolCasa;

            squadraCasa.PartiteGiocate -= 1;
            squadraOspite.PartiteGiocate -= 1;

            squadraCasa.DifferenzaReti = squadraCasa.GolFatti - squadraCasa.GolSubiti;
            squadraOspite.DifferenzaReti = squadraOspite.GolFatti - squadraOspite.GolSubiti;

            if (squadraCasa.Nome == partitaDaAggiornare.Vincitore)
            {
                squadraCasa.Vittorie -= 1;
                squadraOspite.Sconfitte -= 1;
                squadraCasa.PuntiFatti -= 3;
            }
            else
            {
                squadraCasa.Sconfitte -= 1;
                squadraOspite.Vittorie -= 1;
                squadraOspite.PuntiFatti -= 3;
            }

            await _context.SaveChangesAsync();
            return await InserisciRisultato(partita);
        }
        public async Task TerminaFaseGironi()
        {
            var squadre = await _context.Squadre.ToListAsync();
            var squadreOrdinate = squadre.OrderByDescending(x => x.PuntiFatti).ThenByDescending(x => x.DifferenzaReti).ThenByDescending(x => x.GolFatti).ToList();

            //faccio affrontare terza contro sesta e quarta contro quinta
            var listaPartite = new List<Partita>();
            listaPartite.Add(new Partita
            {
                SquadraCasa = squadreOrdinate[2].Nome,
                SquadraOspite = squadreOrdinate[5].Nome,
                Data = DateTime.Now,
                FaseEliminatoria = true,
                TipoPartita = TipoPartita.QuartiFinale

            });
            listaPartite.Add(new Partita
            {
                SquadraCasa = squadreOrdinate[3].Nome,
                SquadraOspite = squadreOrdinate[4].Nome,
                Data = DateTime.Now,
                FaseEliminatoria = true,
                TipoPartita = TipoPartita.QuartiFinale
            });
            listaPartite.Add(new Partita
            {
                SquadraCasa = squadreOrdinate[0].Nome,
                SquadraOspite = null,
                Data = DateTime.Now,
                FaseEliminatoria = true,
                TipoPartita = TipoPartita.Semifinale
            });
            listaPartite.Add(new Partita
            {
                SquadraCasa = squadreOrdinate[1].Nome,
                SquadraOspite = null,
                Data = DateTime.Now,
                FaseEliminatoria = true,
                TipoPartita = TipoPartita.Semifinale
            });
            _context.Partite.AddRange(listaPartite);
            await _context.SaveChangesAsync();
        }

        public async Task<Partita> FaseEliminatoria(int idCasa, int idOspite)
        {
            var squadraCasa = await _context.Squadre.FirstOrDefaultAsync(x => x.IdPk == idCasa);
            var squadraOspite = await _context.Squadre.FirstOrDefaultAsync(x => x.IdPk == idOspite);
            var partita = await _context.Partite.FirstOrDefaultAsync(x =>
                ((x.SquadraCasa == squadraCasa.Nome && x.SquadraOspite == squadraOspite.Nome) ||
                (x.SquadraCasa == squadraOspite.Nome && x.SquadraOspite == squadraCasa.Nome))
                && x.FaseEliminatoria == true);
            if (partita == null)
            {
                return null;
            }
            return new Partita(partita, squadraCasa, squadraOspite);
        }

        public async Task Reset()
        {
            var partite = await _context.Partite.ToListAsync();
            foreach (var partita in partite)
            {
                partita.Vincitore = null;
                partita.GolCasa = 0;
                partita.GolOspite = 0;
                partita.FaseEliminatoria = false;

            }
            await _context.SaveChangesAsync();
        }
    }
}
