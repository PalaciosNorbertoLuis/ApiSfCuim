using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public  class Observation
    {
        public int IdTmpArma { get; set; }
        public DateTime FechaObservation { get; set; }
        public string Observacion { get; set; }
    }
}
