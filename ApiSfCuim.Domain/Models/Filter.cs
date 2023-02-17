using System;

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
