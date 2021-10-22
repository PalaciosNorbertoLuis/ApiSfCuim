using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSfCuim.Domain.Models
{
    public class Operator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Los identificadores no deben contener caracteres de subrayado", Justification = "<pendiente>")]
        public int id_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Los identificadores no deben contener caracteres de subrayado", Justification = "<pendiente>")]
        public string login_id { get; set; }
    }
}
