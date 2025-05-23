﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Torneo
    {
        [Key]
        public int IdPk { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataCreazione { get; set; }
        public int FkIdUtenteCreatore { get; set; }
        public Utente UtenteNavigation { get; set; }
        public TipoTorneo TipoTorneo { get; set; }
        public IEnumerable<Girone> Gironi { get; set; }
        public int NumeroPartecipanti { get; set; }
        public Torneo() { }

    }

    public enum TipoTorneo
    {
        GironeUnico = 0,
        GironiMultipli = 1
    }
}
