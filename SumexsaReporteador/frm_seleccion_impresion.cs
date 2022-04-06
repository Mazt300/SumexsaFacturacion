using SumexsaReporteador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SumexsaFacturacion
{
    public partial class frm_seleccion_impresion : Form
    {
        frm_Reporte_Facturas frm_Reporte_Facturas = new frm_Reporte_Facturas();
        public frm_seleccion_impresion()
        {
            InitializeComponent();
        }
        public int seleccion = 0;
        private void btn_original_Click(object sender, EventArgs e)
        {
            seleccion = 1;
            Close();
        }

        private void btn_copia_Click(object sender, EventArgs e)
        {
            seleccion = 2;
            Close();
        }
    }
}
