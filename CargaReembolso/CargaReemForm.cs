using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using ExcelDataReader;
using System.Globalization;
using CargaReembolso.Interfaces;
using CargaReembolso.Helpers;
using CargaReembolso.Entidades;

namespace CargaReembolso
{
    public partial class CargaReemForm : Form
    {
        
        public string[] strTest = new string[4];
        public string strConnString = "";
        public string sCookie;
        public bool conectarse = true;
        public int ret;
        public string strSQL;

        public SAPbouiCOM.Application rSboApp;
        public SAPbouiCOM.SboGuiApi rSboGui;
        public SAPbobsCOM.Company rCompany;

        private const string UDO_CODE = "SS_REEMCAB";
        private const string HEADER_TABLE = "@SS_REEMCAB";
        private const string DETAIL_COLLECTION = "SS_REEMDET";

        public String Anio = DateTime.Today.Year.ToString();

        private FactInterface ucFact;
        private ReembInterface ucReemb;

        private Color colorDefault = SystemColors.Control;
        private Color colorActivo = Color.LightBlue;

        public CargaReemForm()
        {
            InitializeComponent();
            
            this.Icon = Properties.Resources.LogoV2;
            //label3.Text = $"Copyright © {Anio} SOLSAP S.A.";

            this.AutoSize = true;

            strTest = Environment.GetCommandLineArgs();
            strConnString = ObtenerCadenaConexion(strTest);

            if (string.IsNullOrEmpty(strConnString))
            {
                MessageBox.Show("El programa se debe ejecutar desde SAP Business One. (Carga Reembolso -Err2)");
                Environment.Exit(0);
            }

            if (ConectarSAP(strConnString))
            {
                this.Text = $"CARGA REEMBOLSOS {rCompany.CompanyName.ToString().ToUpper()}";
            }

            ucFact = new FactInterface();
            ucReemb = new ReembInterface();

            ucFact.Dock = DockStyle.Fill;
            ucReemb.Dock = DockStyle.Fill;

            MostrarUserControl(ucReemb, btnReemb);

        }

        /// <summary>
        /// Obtiene la cadena de conexión desde los argumentos
        /// </summary>
        private string ObtenerCadenaConexion(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length > 1)
                {
                    if (args[0].LastIndexOf("\\") > 0)
                    {
                        return args[1];
                    }
                    else
                    {
                        return args[0];
                    }
                }
                else
                {
                    if (args[0].LastIndexOf("\\") == -1)
                    {
                        return args[0];
                    }
                    else
                    {
                        MessageBox.Show("El programa se debe ejecutar desde SAP Business One. (Carga Reembolso -Err1)");
                        Environment.Exit(0);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Conecta con SAP y obtiene la compañía
        /// </summary>
        private bool ConectarSAP(string connectionString)
        {
            try
            {
                rSboGui = new SAPbouiCOM.SboGuiApi();
                rCompany = new SAPbobsCOM.Company();

                rSboGui.Connect(connectionString);
                rSboApp = rSboGui.GetApplication();

                if (conectarse)
                {
                    rCompany = rSboApp.Company.GetDICompany();
                }
                else
                {
                    sCookie = rCompany.GetContextCookie();
                    ret = rCompany.SetSboLoginContext(rSboApp.Company.GetConnectionContext(sCookie));
                    if (ret == 0)
                    {
                        ret = rCompany.Connect();
                        if (ret != 0)
                        {
                            rCompany.GetLastError(out ret, out strSQL);
                            rSboApp.StatusBar.SetText("Error al Conectar el programa Carga Reembolso: " + strSQL,
                                SAPbouiCOM.BoMessageTime.bmt_Medium,
                                SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        rSboApp.StatusBar.SetText("No se ha Conectado con programa Carga Reembolso: Error " + ret,
                            SAPbouiCOM.BoMessageTime.bmt_Short,
                            SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        Environment.Exit(0);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error conectando a SAP: " + ex.Message);
                return false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = "https://solsaptech.com/"; // URL que deseas abrir
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true  // Necesario para .NET Framework 4.8 y Windows 10/11
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el enlace: " + ex.Message);
            }
        }

        private void btnReemb_Click(object sender, EventArgs e)
        {
            MostrarUserControl(ucReemb, btnReemb);
        }

        private void btnFact_Click(object sender, EventArgs e)
        {
            MostrarUserControl(ucFact, btnFact);
        }

        private void MostrarUserControl(UserControl uc, Button botonSeleccionado)
        {
            // Limpia el panel2 antes de agregar el nuevo control
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(uc);

            foreach (Control ctrl in TbLypanel.Controls)
            {
                if (ctrl is Button btn)
                    btn.BackColor = colorDefault;
            }

            // Pintar el botón seleccionado
            botonSeleccionado.BackColor = colorActivo;
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            var helper = new SAPReembolsoHelper(rCompany);

            var grids = ucReemb.GetAllGrids();
            if (grids.Count < 2)
            {
                MessageBox.Show("Debe cargar al menos 2 hojas (Cabecera y Detalle).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // La primera pestaña = cabecera
            var gridCabecera = grids[0];
            // La segunda pestaña = detalle
            var gridDetalle = grids[1];

            foreach (DataGridViewRow cabRow in gridCabecera.Rows)
            {
                if (cabRow.IsNewRow) continue;

                try
                {
                    var reembolso = new Reembolso
                    {
                        Code = cabRow.Cells["Code"].Value?.ToString(),
                        Name = cabRow.Cells["Name"].Value?.ToString(),
                        ReembolsoDet = new List<ReembolsoDet>()
                    };

                    // Recorremos detalle
                    foreach (DataGridViewRow detRow in gridDetalle.Rows)
                    {
                        if (detRow.IsNewRow) continue;

                        if (cabRow.Cells["Code"].Value?.ToString() == detRow.Cells["Code"].Value?.ToString())
                        {
                            var detalle = new ReembolsoDet
                            {
                                Code = detRow.Cells["Code"].Value?.ToString(),
                                SS_TipoId = detRow.Cells["U_SS_TipoId"].Value?.ToString(),
                                SS_IdProv = detRow.Cells["U_SS_IdProv"].Value?.ToString(),
                                SS_TipoComp = detRow.Cells["U_SS_TipoComp"].Value?.ToString(),
                                SS_Est = detRow.Cells["U_SS_Est"].Value?.ToString(),
                                SS_PtoEmi = detRow.Cells["U_SS_PtoEmi"].Value?.ToString(),
                                SS_NumAut = detRow.Cells["U_SS_NumAut"].Value?.ToString(),
                                SS_FecEmi = detRow.Cells["U_SS_FecEmi"].Value == null ? DateTime.MinValue :
                                       Convert.ToDateTime(detRow.Cells["U_SS_FecEmi"].Value),
                                SS_IVA0 = detRow.Cells["U_SS_IVA0"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_IVA0"].Value),
                                SS_IvaDif0 = detRow.Cells["U_SS_IvaDif0"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_IvaDif0"].Value),
                                SS_NoObjIVA = detRow.Cells["U_SS_NoObjIVA"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_NoObjIVA"].Value),
                                SS_MontoIVA = detRow.Cells["U_SS_MontoIVA"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_MontoIVA"].Value),
                                SS_MontoICE = detRow.Cells["U_SS_MontoICE"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_MontoICE"].Value),
                                SS_IvaExe = detRow.Cells["U_SS_IvaExe"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["U_SS_IvaExe"].Value),
                            };

                            reembolso.ReembolsoDet.Add(detalle);
                        }
                    }

                    // Guardamos en SAP
                    string msg;
                    if (!helper.CrearOActualizarReembolso(reembolso, out msg))
                    {
                        MessageBox.Show(msg, "Error SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        rSboApp.StatusBar.SetText(msg,
                            SAPbouiCOM.BoMessageTime.bmt_Short,
                            SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error procesando reembolso {cabRow.Cells["Code"].Value}: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {

        }
    }
}
