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

            foreach (Control ctrl in pnlSuperior.Controls)
            {
                if (ctrl is Button btn)
                    btn.BackColor = colorDefault;
            }

            // Pintar el botón seleccionado
            botonSeleccionado.BackColor = colorActivo;
        }

    }
}
