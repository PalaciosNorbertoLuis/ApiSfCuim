﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public partial class SfViewConsultaArma
    {
        [Key]
        public int? IdDescripcionArma { get; set; }
        public string TipoArma { get; set; }
        public string Modelo { get; set; }
        public string Clase { get; set; }
        public string Calibre { get; set; }
        public string Medida { get; set; }

    }
}
