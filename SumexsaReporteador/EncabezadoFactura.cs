using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumexsaReporteador
{
    public partial class EncabezadoFactura
    {
        public string tipo_factura { get; set; }
        public string fecha { get; set; }
        public string cliente { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string concepto { get; set; }
        public string origen { get; set; }
        public string telefono { get; set; }
        public string atencion { get; set; }
        public decimal sub_total { get; set; }
        public decimal iva { get; set; }
        public decimal otros { get; set; }
        public decimal totalnetousd { get; set; }
        public decimal tc { get; set; }
        public decimal totalcs { get; set; }

        public EncabezadoFactura()
        {

        }
    }
}
