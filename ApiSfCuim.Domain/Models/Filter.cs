using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public class Filter
    {
        public int IdTmpArma { get; set; }
        public DateTime FechaFiltro { get; set; }
        public string Filtro { get; set; }
        public string TipoOperadorFiltro { get; set; }
        public int IdOperadorFiltro { get; set; }
        public string Operador { get; set; }
    }
}
