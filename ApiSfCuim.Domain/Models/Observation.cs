using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public partial class Observation
    {
        [Key]
        public int IdTmpArma { get; set; }
        public DateTime? Fecha { get; set; }
        public string Observacion { get; set; }
    }
}
