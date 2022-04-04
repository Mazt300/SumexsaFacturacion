using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumexsaReporteador
{
    public partial class Factura
    {
        public int item { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public decimal unitario { get; set; }
        public decimal total { get; set; }

        public Factura()
        {

        }
    }
}
