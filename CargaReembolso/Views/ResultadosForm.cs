using CargaReembolso.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargaReembolso.Views
{
    public partial class ResultadosForm : Form
    {
        public ResultadosForm(List<ResultadoTransaccion> reembolsos, List<ResultadoTransaccion> facturas)
        {
            InitializeComponent();
            dgvReembolsos.DataSource = reembolsos;
            dgvFacturas.DataSource = facturas;
        }
    }
}
