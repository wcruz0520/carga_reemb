using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargaReembolso.Entidades
{
    public class ResultadoTransaccion
    {
        public string Codigo { get; set; }
        public string Estado { get; set; } // "Exitoso" / "Fallido"
        public string Observacion { get; set; }
    }
}
