using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace SumexsaReporteador
{
    public partial class frm_Reporte_Facturas : Form
    {
        string tipo_factura = "Contado";
        int cantidad = 0, CantidadFilas = 0,Item = 1, Fila = 0;
        double subtotal = 0,total_subtotal = 0,IvaCalculado = 0;
        NumberFormatInfo formato_decimal = new CultureInfo("es-NI").NumberFormat;

        public frm_Reporte_Facturas()
        {
            InitializeComponent();
        }
        public List<Factura> data = new List<Factura>();
        public List<EncabezadoFactura> encabezado_list = new List<EncabezadoFactura>();
        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            if(Validaciones())
            {
                Imprimir(false);
                var resultado = MessageBox.Show("¿Desea imprimir copia?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Imprimir(true);
                }
            }
        }
        private void Imprimir(bool copia)
        {
            encabezado_list.Clear();
            data.Clear();

            EncabezadoFactura encabezado = new EncabezadoFactura();
            encabezado.tipo_factura = tipo_factura;
            encabezado.fecha = mtxt_fecha.Text;
            encabezado.cliente = txt_cliente.Text;
            encabezado.direccion = txt_direccion.Text;
            encabezado.email = txt_email.Text;
            encabezado.concepto = txt_concepto.Text;
            encabezado.origen = txt_origen.Text;
            encabezado.telefono = txt_telefono.Text;
            encabezado.atencion = txt_atencion.Text;
            encabezado.sub_total = Convert.ToDecimal(txt_subtotal.Text);
            encabezado.iva = Convert.ToDecimal(IvaCalculado);
            encabezado.totalnetousd = Convert.ToDecimal(txt_totalnetousd.Text);
            encabezado.tc = Convert.ToDecimal(txt_tc.Text);
            encabezado.otros = Convert.ToDecimal(txt_otros.Text);
            encabezado.totalcs = Convert.ToDecimal(txt_totalCS.Text);

            encabezado_list.Add(encabezado);

            for (int i = 0; i < grd_contenidos.RowCount; i++)
            {
                Factura factura = new Factura();
                factura.item = Convert.ToInt32(this.grd_contenidos.Rows[i].Cells[0].Value);
                factura.cantidad = Convert.ToInt32(this.grd_contenidos.Rows[i].Cells[1].Value);
                factura.descripcion = this.grd_contenidos.Rows[i].Cells[2].Value.ToString();
                factura.unitario = Convert.ToDecimal(this.grd_contenidos.Rows[i].Cells[3].Value);
                factura.total = Convert.ToDecimal(this.grd_contenidos.Rows[i].Cells[4].Value);
                data.Add(factura);
            }
            //
            //ReportParameter[] parameters = new ReportParameter[14];
            //parameters[0] = new ReportParameter("tipo_factura",tipo_factura);
            //parameters[1] = new ReportParameter("fecha_factura", mtxt_fecha.Text);
            //parameters[2] = new ReportParameter("Cliente", txt_cliente.Text);
            //parameters[3] = new ReportParameter("Direccion", txt_direccion.Text);
            //parameters[4] = new ReportParameter("Email", txt_email.Text);
            //parameters[5] = new ReportParameter("Concepto", txt_concepto.Text);
            //parameters[6] = new ReportParameter("Origen", txt_origen.Text);
            //parameters[7] = new ReportParameter("Telefono", txt_telefono.Text);
            //parameters[8] = new ReportParameter("Atencion", txt_atencion.Text);
            //parameters[9] = new ReportParameter("sub_total", txt_subtotal.Text);
            //parameters[10] = new ReportParameter("iva", txt_iva.Text);
            //parameters[11] = new ReportParameter("Total_neto", txt_totalnetousd.Text);
            //parameters[12] = new ReportParameter("tipo_cambio", txt_tc.Text);
            //parameters[13] = new ReportParameter("otros", txt_otros.Text);
            //
            LocalReport report = new LocalReport();
            if(copia == false)
            {
                report.ReportPath = Application.StartupPath + "\\rpt_Factura.rdlc";
            }
            else
            {
                report.ReportPath = Application.StartupPath + "\\rpt_Factura_copia.rdlc";
            }
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("Ds_encabezado_factura", encabezado_list));
            report.DataSources.Add(new ReportDataSource("Ds_detalle_factura", data));
            report.Refresh();
            report.PrintToPrinter();
            //
        }

        private void frm_Reporte_Facturas_Load(object sender, EventArgs e)
        {
            Agregar_fila();
            mtxt_fecha.Text = DateTime.Now.ToShortDateString().ToString();
        }
        private void Agregar_fila()
        {
            grd_contenidos.Rows.Clear();
            Item = 1;
            grd_contenidos.Rows.Add();
            grd_contenidos.Rows[Fila].Cells[0].Value = Item;
        }

        private void rb_credito_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_credito.Checked)
            {
                tipo_factura = "Credito";
                rb_contado.Checked = false;
            }
        }

        private void rb_contado_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_contado.Checked)
            {
                tipo_factura = "Contado";
                rb_credito.Checked = false;
            }
        }

        private void grd_contenidos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                if (grd_contenidos.Columns[3].Name == "clm_unitario" && grd_contenidos.RowCount >= 0)
                {
                    double numero = 0;
                    bool esnumerico = double.TryParse(grd_contenidos.Rows[e.RowIndex].Cells[1].Value.ToString(), out numero);
                    if (esnumerico)
                    {
                        subtotal = Convert.ToDouble(grd_contenidos.Rows[e.RowIndex].Cells[3].Value);
                        cantidad = Convert.ToInt32(grd_contenidos.Rows[e.RowIndex].Cells[1].Value);
                        grd_contenidos.Rows[e.RowIndex].Cells[4].Value = subtotal * cantidad;
                        SumarColumna();
                    }
                    else if(grd_contenidos.Rows[e.RowIndex].Cells[3].Value.ToString() != "")
                    {
                        MessageBox.Show("El valor para Unitario debe ser númerico","Validaciones",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        grd_contenidos.Rows[e.RowIndex].Cells[3].Value = "";
                        grd_contenidos.Rows[e.RowIndex].Cells[4].Value = "";
                    }
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (grd_contenidos.Columns[1].Name == "cl_cant" && grd_contenidos.RowCount >= 0)
                {
                    double numero = 0;
                    bool esnumerico = double.TryParse(grd_contenidos.Rows[e.RowIndex].Cells[1].Value.ToString(), out numero);
                    if (!esnumerico && grd_contenidos.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                    {
                        MessageBox.Show("El valor para Cantidad debe ser númerico", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        grd_contenidos.Rows[e.RowIndex].Cells[1].Value = "";
                    }
                }
            }
        }

        private void SumarColumna()
        {
            total_subtotal = 0;
            foreach (DataGridViewRow row in grd_contenidos.Rows)
            {
                total_subtotal += Convert.ToDouble(row.Cells[4].Value);
            }
            txt_subtotal.Text = total_subtotal.ToString();
            txt_totalnetousd.Text = total_subtotal.ToString();
            if(!string.IsNullOrEmpty(txt_iva.Text))
            {
                CalcularIVA();
            }
            if (!string.IsNullOrEmpty(txt_tc.Text))
            {
                ConvertirUSD_CS();
            }
        }
        private void RestarColumna()
        {
            total_subtotal = 0;
            foreach (DataGridViewRow row in grd_contenidos.Rows)
            {
                total_subtotal += Convert.ToDouble(row.Cells[4].Value);
            }
            txt_subtotal.Text = total_subtotal.ToString();
            txt_totalnetousd.Text = total_subtotal.ToString();
            if (!string.IsNullOrEmpty(txt_iva.Text))
            {
                CalcularIVA();
            }
            if (!string.IsNullOrEmpty(txt_tc.Text))
            {
                ConvertirUSD_CS();
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            txt_cliente.Text = "";
            txt_direccion.Text = "";
            txt_email.Text = "";
            txt_concepto.Text = "";
            txt_origen.Text = "";
            txt_telefono.Text = "";
            txt_atencion.Text = "";
            txt_subtotal.Text = "";
            txt_iva.Text = "0";
            txt_otros.Text = "0";
            txt_totalnetousd.Text = "";
            txt_tc.Text = "";
            txt_totalCS.Text = "";
            data.Clear();
            Agregar_fila();

        }
        private bool Validaciones()
        {
            if (string.IsNullOrEmpty(mtxt_fecha.Text))
            {
                MessageBox.Show("La fecha no puede ser nula", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_cliente.Text))
            {
                MessageBox.Show("Nombre de cliente no puede estar vacio","Validaciones",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_direccion.Text))
            {
                MessageBox.Show("El campo dirección no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_email.Text))
            {
                MessageBox.Show("El e-mail no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_concepto.Text))
            {
                MessageBox.Show("El concepto no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_origen.Text))
            {
                MessageBox.Show("El origen no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_telefono.Text))
            {
                MessageBox.Show("El teléfono no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_atencion.Text))
            {
                MessageBox.Show("Atención no puede estar vacio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            for (int i = 0; i < grd_contenidos.RowCount; i++)
            {
                var valor = this.grd_contenidos.Rows[i].Cells[3].Value;
                if (valor == null || valor.ToString() == "")
                {
                    MessageBox.Show("No se ha especificado valor unitario en la fila " + (i + 1), "Validaciones", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return false;
                }
            }
            for (int i = 0; i < grd_contenidos.RowCount; i++)
            {
                var valor = this.grd_contenidos.Rows[i].Cells[1].Value;
                if (valor == null || valor.ToString() == "")
                {
                    MessageBox.Show("No se ha especificado valor de cantidad en la fila " + (i + 1), "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txt_tc.Text))
            {
                MessageBox.Show("Es necesario especificar la tasa de cambio", "Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void CalcularIVA()
        {
            bool validacion = false;
            double totalconiva = 0;
            for (int i = 0; i< grd_contenidos.RowCount;i++)
            {
                var valor = this.grd_contenidos.Rows[i].Cells[3].Value;
                if (valor == null || valor.ToString() == "")
                {
                    MessageBox.Show("No se ha especificado valor unitario en la fila "+(i + 1),"Validaciones",MessageBoxButtons.OK);
                    validacion = true;
                    break;
                }
            }
            if(validacion == false)
            {
                double totalusd = Convert.ToDouble(txt_totalnetousd.Text, formato_decimal);
                double iva = Convert.ToDouble(txt_iva.Text, formato_decimal) / 100;
                //if (iva == 0)
                //{
                //    MessageBox.Show("Iva no puede ser igual a 0","Validaciones",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //}
                IvaCalculado = Math.Round((totalusd * iva), 2);
                totalconiva = Math.Round(totalusd + (totalusd * iva), 2);
                txt_totalnetousd.Text = Convert.ToString(totalconiva);
            }
            else
            {
                txt_iva.Text = "";
            }
        }
        private void ConvertirUSD_CS()
        {
            double totalusd = Convert.ToDouble(txt_totalnetousd.Text, formato_decimal);
            double tc = Convert.ToDouble(txt_tc.Text, formato_decimal);
            double totalCS = Convert.ToDouble(Math.Round((totalusd * tc), 2), formato_decimal);
            txt_totalCS.Text = totalCS.ToString();
        }
        private void txt_tc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                ConvertirUSD_CS();
            }
        }
        private void btn_agregar_fila_Click(object sender, EventArgs e)
        {
            Fila++;
            Item++;
            grd_contenidos.Rows.Add();
            grd_contenidos.Rows[Fila].Cells[0].Value = Item;
            CantidadFilas = grd_contenidos.RowCount;
        }

        private void txt_iva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CalcularIVA();
            }
        }

    }
}
