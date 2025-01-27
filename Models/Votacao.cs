﻿using System;
using System.Collections.Generic;

namespace ProjetoMySQL.Models
{
    public partial class Votacao
    {
        public int Id { get; set; }
        public int? IdEleitor { get; set; }
        public int? IdCandidato { get; set; }

        public virtual Candidato? IdCandidatoNavigation { get; set; }
        public virtual Eleitor? IdEleitorNavigation { get; set; }
    }
}
