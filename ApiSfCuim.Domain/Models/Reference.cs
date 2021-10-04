using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public partial class Reference
    {
        [Key]
        public int? IdTmpArma { get; set; }
        public int? IdDescArmaFK { get; set; }
        public string NumeroSerie { get; set; }
        public int? IdDescripcionArma { get; set; }
        public string Estado { get; set; }
        public string TipoArma { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

    }
}
