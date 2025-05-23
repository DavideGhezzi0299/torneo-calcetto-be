﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torneo_calcetto.EF.Models
{
    public class Girone
    {
        [Key]
        public int IdPk { get; set; }
        public int FkIdTorneo { get; set; }

        public Torneo TorneoNavigation { get; set; }
        public ICollection<Squadra> Squadre { get; set; } = new List<Squadra>();
        public ICollection<Partita> Partite { get; set; } = new List<Partita>();
    }
}
