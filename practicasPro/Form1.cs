using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practicasPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsCliente x = new clsCliente();

            x.Listar(dgvClientes);

            lblCant.Text = x.CantidadDeudores.ToString();
            lblDeuda.Text = x.TotalDeuda.ToString();
            

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            prtVentana.ShowDialog(); //creamos la ventana para seleccionar la impresora
            prtDocumento.PrinterSettings = prtVentana.PrinterSettings; //le asignamos a la impresora que seleccionamos en la ventana
            prtDocumento.Print();  //imprimimos
            MessageBox.Show("Reporte Impreso Correctamente");
        }

        private void prtDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            clsCliente x = new clsCliente();
            x.Imprimir(e);
        }
    }
}
