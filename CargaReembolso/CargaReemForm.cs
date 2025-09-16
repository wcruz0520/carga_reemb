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
using CargaReembolso.Views;
using SAPbobsCOM;

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
        //public static SAPbobsCOM.Company rCompany;

        private const string UDO_CODE = "SS_REEMCAB";
        private const string HEADER_TABLE = "@SS_REEMCAB";
        private const string DETAIL_COLLECTION = "SS_REEMDET";

        public String Anio = DateTime.Today.Year.ToString();

        private FactInterface ucFact;
        private ReembInterface ucReemb;

        private Color colorDefault = SystemColors.Control;
        private Color colorActivo = Color.LightBlue;

        private List<ResultadoTransaccion> resultadosReemb = new List<ResultadoTransaccion>();
        private List<ResultadoTransaccion> resultadosFact = new List<ResultadoTransaccion>();

        public CargaReemForm()
        {
            InitializeComponent();
            
            this.Icon = Properties.Resources.LogoV2;
            //label3.Text = $"Copyright © {Anio} SOLSAP S.A.";

            this.AutoSize = true;
            this.btnProcesar.Enabled = false;
            this.btnSimular.Enabled = false;
            this.btnConnect.Enabled = true;

            strTest = Environment.GetCommandLineArgs();

            //Comentado para testear
            //strConnString = ObtenerCadenaConexion(strTest);

            strConnString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";

            if (string.IsNullOrEmpty(strConnString))
            {
                MessageBox.Show("El programa se debe ejecutar desde SAP Business One. (Carga Reembolso -Err2)");
                Environment.Exit(0);
            }

            if (ConectarSAP(strConnString))
            {
                this.Text = $"CARGA REEMBOLSOS {Globals.rCompany.CompanyName.ToString().ToUpper()}";
                //this.btnConnect.Enabled = false;
                this.btnConnect.Text = "Desconectar";
                this.btnProcesar.Enabled = true;
                this.btnSimular.Enabled = true;
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
        /// Conecta con SAP y obtiene la compañía usando UI API -> DI API (método robusto)
        /// </summary>
        private bool ConectarSAP(string connectionString)
        {
            try
            {
                rSboGui = new SAPbouiCOM.SboGuiApi();
                rSboGui.Connect(connectionString);
                rSboApp = rSboGui.GetApplication();

                // Crear objeto company desde cero
                Globals.rCompany = new SAPbobsCOM.Company();

                // Obtener cookie de contexto
                sCookie = Globals.rCompany.GetContextCookie();

                // Pasar el contexto de la sesión UI al objeto Company
                ret = Globals.rCompany.SetSboLoginContext(rSboApp.Company.GetConnectionContext(sCookie));
                if (ret != 0)
                {
                    rSboApp.StatusBar.SetText("Error SetSboLoginContext: " + ret,
                        SAPbouiCOM.BoMessageTime.bmt_Medium,
                        SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }

                // Conectar usando ese contexto
                ret = Globals.rCompany.Connect();
                if (ret != 0)
                {
                    Globals.rCompany.GetLastError(out int errorCode, out string errorMsg);
                    rSboApp.StatusBar.SetText("Error al conectar DI API: " + errorMsg,
                        SAPbouiCOM.BoMessageTime.bmt_Medium,
                        SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }

                // Si todo va bien, mostrar info
                rSboApp.StatusBar.SetText($"Conectado a {Globals.rCompany.CompanyName} ({Globals.rCompany.CompanyDB})",
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error conectando a SAP: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void btnProcesar_Click(object sender, EventArgs e)
        {
            var Factgrids_valid = ucFact.GetAllGrids();
            var Reembgrids_valid = ucReemb.GetAllGrids();
            this.btnConnect.Enabled = false;
            this.btnSimular.Enabled = false;
            this.btnFact.Enabled = false;
            this.btnReemb.Enabled = false;

            //await Task.Run(() =>
            //{
            //    crearReembolsos();
            //});

            if (Reembgrids_valid.Count >= 1)
            {
                crearReembolsos();
            }
            
            if (Factgrids_valid.Count >= 1)
            {
                crearFactura();
            }

            var frmResultados = new ResultadosForm(resultadosReemb, resultadosFact);
            frmResultados.Text = "Resultado procesamiento - Real";
            frmResultados.StartPosition = FormStartPosition.CenterParent;
            frmResultados.ShowDialog();

            this.btnConnect.Enabled = true;
            this.btnSimular.Enabled = true;
            this.btnFact.Enabled = true;
            this.btnReemb.Enabled = true;
            prgBar.Value = 0;
            //this.prgBar = new ProgressBar();
        }

        private void crearReembolsos()
        {
            resultadosReemb.Clear();

            var Reembhelper = new SAPReembolsoHelper(Globals.rCompany);
            var Reembgrids = ucReemb.GetAllGrids();

            if (Reembgrids.Count < 2)
            {
                MessageBox.Show("Debe cargar al menos 2 hojas (Cabecera y Detalle) para la sección de reembolsos.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ReembgridCabecera = Reembgrids[0];
            var ReembgridDetalle = Reembgrids[1];

            InicializarProgressBar(ReembgridCabecera.Rows.Count);

            foreach (DataGridViewRow cabRow in ReembgridCabecera.Rows)
            {
                if (cabRow.IsNewRow) continue;

                var codigo = cabRow.Cells["Code"].Value?.ToString();
                string mensaje;

                try
                {
                    var reembolso = new Reembolso
                    {
                        Code = codigo,
                        Name = cabRow.Cells["Name"].Value?.ToString(),
                        ReembolsoDet = new List<ReembolsoDet>()
                    };

                    foreach (DataGridViewRow detRow in ReembgridDetalle.Rows)
                    {
                        if (detRow.IsNewRow) continue;

                        if (codigo == detRow.Cells["Code"].Value?.ToString())
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
                                SS_FecEmi = Convert.ToDateTime(detRow.Cells["U_SS_FecEmi"].Value),
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

                    bool exito = Reembhelper.CrearOActualizarReembolso(reembolso, out mensaje);

                    resultadosReemb.Add(new ResultadoTransaccion
                    {
                        Codigo = codigo,
                        Estado = exito ? "Exitoso" : "Fallido",
                        Observacion = mensaje
                    });

                }
                catch (Exception ex)
                {
                    resultadosReemb.Add(new ResultadoTransaccion
                    {
                        Codigo = codigo,
                        Estado = "Fallido",
                        Observacion = ex.Message
                    });
                }

                ActualizarProgressBar();
            }

            //prgBar.Visible = false;
        }

        private void crearFactura()
        {
            resultadosFact.Clear();

            var Facthelper = new SAPFacturaHelper(Globals.rCompany);
            var Factgrids = ucFact.GetAllGrids();

            if (Factgrids.Count < 2)
            {
                MessageBox.Show("Debe cargar al menos 2 hojas (Cabecera y Detalle) para la sección de facturas.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var FactgridCabecera = Factgrids[0];
            var FactgridDetalle = Factgrids[1];

            foreach (DataGridViewRow cabRow in FactgridCabecera.Rows)
            {
                if (cabRow.IsNewRow) continue;

                var codigoReemb = cabRow.Cells["U_SS_Reembolsos"].Value?.ToString();

                // Evitar crear factura si el reembolso asociado falló
                if (resultadosReemb.Any(r => r.Codigo == codigoReemb && r.Estado == "Fallido"))
                {
                    resultadosFact.Add(new ResultadoTransaccion
                    {
                        Codigo = cabRow.Cells["DocEntry"].Value?.ToString(),
                        Estado = "Fallido",
                        Observacion = $"No se creó factura porque el reembolso {codigoReemb} falló."
                    });
                    continue;
                }

                string mensaje;
                                
                try
                {
                    Factura factura = null;

                    try
                    {
                        factura = new Factura
                        {
                            Id = TryGet(() => Convert.ToInt32(cabRow.Cells["Id"].Value), "Id", obligatorio: true),
                            DocEntry = TryGet(() =>
                            (cabRow.Cells["DocEntry"].Value == null || cabRow.Cells["DocEntry"].Value == DBNull.Value)
                                ? (int?)null
                                : Convert.ToInt32(cabRow.Cells["DocEntry"].Value),
                            "DocEntry", obligatorio: false),
                            CardCode = TryGet(() => cabRow.Cells["CardCode"].Value?.ToString(), "CardCode"),
                            DocDate = TryGet(() => Convert.ToDateTime(cabRow.Cells["DocDate"].Value), "DocDate"),
                            TaxDate = TryGet(() => Convert.ToDateTime(cabRow.Cells["TaxDate"].Value), "TaxDate"),
                            DocDueDate = TryGet(() => Convert.ToDateTime(cabRow.Cells["DocDueDate"].Value), "DocDueDate"),
                            Series = TryGet(() => Convert.ToInt32(cabRow.Cells["Series"].Value), "Series"),
                            DocCurrency = TryGet(() => cabRow.Cells["DocCurrency"].Value?.ToString(), "DocCurrency"),
                            DocType = TryGet(() => ConvertToDocType(cabRow.Cells["DocType"].Value), "DocType"),
                            HandWritten = TryGet(() => ConvertToYesNo(cabRow.Cells["HandWritten"].Value), "HandWritten"),
                            Comments = TryGet(() => cabRow.Cells["Comments"].Value?.ToString(), "Comments"),
                            SS_Est = TryGet(() => cabRow.Cells["U_SS_Est"].Value?.ToString(), "SS_Est"),
                            SS_Pemi = TryGet(() => cabRow.Cells["U_SS_Pemi"].Value?.ToString(), "SS_Pemi"),
                            SS_TipCom = TryGet(() => cabRow.Cells["U_SS_TipCom"].Value?.ToString(), "SS_TipCom"),
                            SS_FormaPagos = TryGet(() => cabRow.Cells["U_SS_FormaPagos"].Value?.ToString(), "SS_FormaPagos"),
                            SS_Reembolsos = TryGet(() => cabRow.Cells["U_SS_Reembolsos"].Value?.ToString(), "SS_Reembolsos"),
                            SS_CONTRATO = TryGet(() => cabRow.Cells["U_SS_CONTRATO"].Value?.ToString(), "SS_CONTRATO"),
                            SSCONTRATO = TryGet(() => Convert.ToInt32(cabRow.Cells["U_SSCONTRATO"].Value), "SSCONTRATO"),
                            SSLOCAL = TryGet(() => cabRow.Cells["U_SSLOCAL"].Value?.ToString(), "SSLOCAL"),
                            Denominacion = TryGet(() => cabRow.Cells["U_Denominacion"].Value?.ToString(), "Denominacion"),
                            Detalles = new List<FacturaDetalle>()
                        };
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al asignar propiedad de Factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (DataGridViewRow detRow in FactgridDetalle.Rows)
                    {
                        if (detRow.IsNewRow) continue;
                        if (cabRow.Cells["Id"].Value?.ToString() == detRow.Cells["Id"].Value?.ToString())
                        {
                            var detalle = new FacturaDetalle
                            {
                                ItemCode = detRow.Cells["ItemCode"].Value?.ToString(),
                                Quantity = detRow.Cells["Quantity"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["Quantity"].Value),
                                Price = detRow.Cells["Price"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["Price"].Value),
                                DiscPrcnt = detRow.Cells["DiscPrcnt"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["DiscPrcnt"].Value),
                                TaxCode = detRow.Cells["TaxCode"].Value?.ToString(),
                                WarehouseCode = detRow.Cells["WhsCode"].Value?.ToString()
                            };
                            factura.Detalles.Add(detalle);
                        }
                    }

                    bool exito = Facthelper.CrearOActualizarFactura(factura, out mensaje);

                    resultadosFact.Add(new ResultadoTransaccion
                    {
                        Codigo = factura.Id.ToString(),
                        Estado = exito ? "Exitoso" : "Fallido",
                        Observacion = mensaje
                    });
                }
                catch (Exception ex)
                {
                    resultadosFact.Add(new ResultadoTransaccion
                    {
                        Codigo = cabRow.Cells["Id"].Value?.ToString(),
                        Estado = "Fallido",
                        Observacion = ex.Message
                    });
                }
                ActualizarProgressBar();
            }
            //prgBar.Visible = false;
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            resultadosReemb.Clear();
            resultadosFact.Clear();

            var Reembhelper = new SAPReembolsoHelper(Globals.rCompany);
            var Facthelper = new SAPFacturaHelper(Globals.rCompany);

            // Reembolsos
            var Reembgrids = ucReemb.GetAllGrids();
            if (Reembgrids.Count >= 2)
            {
                var cabecera = Reembgrids[0];
                var detalle = Reembgrids[1];

                foreach (DataGridViewRow cabRow in cabecera.Rows)
                {
                    if (cabRow.IsNewRow) continue;

                    var codigo = cabRow.Cells["Code"].Value?.ToString();
                    string mensaje;

                    var reembolso = new Reembolso
                    {
                        Code = codigo,
                        Name = cabRow.Cells["Name"].Value?.ToString(),
                        ReembolsoDet = new List<ReembolsoDet>()
                    };

                    foreach (DataGridViewRow detRow in detalle.Rows)
                    {
                        if (detRow.IsNewRow) continue;
                        if (codigo == detRow.Cells["Code"].Value?.ToString())
                        {
                            reembolso.ReembolsoDet.Add(new ReembolsoDet
                            {
                                Code = detRow.Cells["Code"].Value?.ToString(),
                                SS_TipoId = detRow.Cells["U_SS_TipoId"].Value?.ToString(),
                                SS_IdProv = detRow.Cells["U_SS_IdProv"].Value?.ToString(),
                                SS_TipoComp = detRow.Cells["U_SS_TipoComp"].Value?.ToString(),
                                SS_FecEmi = Convert.ToDateTime(detRow.Cells["U_SS_FecEmi"].Value)
                            });
                        }
                    }

                    bool valido = Reembhelper.ValidarReembolso(reembolso, out mensaje);
                    resultadosReemb.Add(new ResultadoTransaccion
                    {
                        Codigo = codigo,
                        Estado = valido ? "Válido" : "Inválido",
                        Observacion = mensaje
                    });
                }
            }

            // Facturas
            var Factgrids = ucFact.GetAllGrids();
            if (Factgrids.Count >= 2)
            {
                var cabecera = Factgrids[0];
                var detalle = Factgrids[1];

                foreach (DataGridViewRow cabRow in cabecera.Rows)
                {
                    if (cabRow.IsNewRow) continue;

                    string mensaje;
                    var factura = new Factura
                    {
                        DocEntry = cabRow.Cells["DocEntry"].Value == null ? 0 : (int)cabRow.Cells["DocEntry"].Value,
                        CardCode = cabRow.Cells["CardCode"].Value?.ToString(),
                        DocDate = Convert.ToDateTime(cabRow.Cells["DocDate"].Value),
                        TaxDate = Convert.ToDateTime(cabRow.Cells["TaxDate"].Value),
                        DocDueDate = Convert.ToDateTime(cabRow.Cells["DocDueDate"].Value),
                        Detalles = new List<FacturaDetalle>()
                    };

                    foreach (DataGridViewRow detRow in detalle.Rows)
                    {
                        if (detRow.IsNewRow) continue;
                        if (cabRow.Cells["Id"].Value?.ToString() == detRow.Cells["Id"].Value?.ToString())
                        {
                            factura.Detalles.Add(new FacturaDetalle
                            {
                                ItemCode = detRow.Cells["ItemCode"].Value?.ToString(),
                                Quantity = detRow.Cells["Quantity"].Value == null ? 0 : Convert.ToDecimal(detRow.Cells["Quantity"].Value)
                            });
                        }
                    }

                    bool valido = Facthelper.ValidarFactura(factura, out mensaje);
                    resultadosFact.Add(new ResultadoTransaccion
                    {
                        Codigo = factura.DocEntry.ToString(),
                        Estado = valido ? "Válido" : "Inválido",
                        Observacion = mensaje
                    });
                }
            }

            // Mostrar resultados en la ventana existente
            var frmResultados = new ResultadosForm(resultadosReemb, resultadosFact);
            frmResultados.Text = "Resultado procesamiento - Simulación";
            frmResultados.StartPosition = FormStartPosition.CenterParent;
            frmResultados.ShowDialog();
        }

        private void InicializarProgressBar(int max)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => InicializarProgressBar(max)));
                return;
            }

            prgBar.Value = 0;
            prgBar.Minimum = 0;
            prgBar.Maximum = max;
            prgBar.Step = 1;
            prgBar.Visible = true;
        }

        private void ActualizarProgressBar()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ActualizarProgressBar));
                return;
            }

            if (prgBar.Value < prgBar.Maximum)
            {
                prgBar.PerformStep();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(this.btnConnect.Text == "Conectar")
            {
                if (string.IsNullOrEmpty(strConnString))
                {
                    MessageBox.Show("El programa se debe ejecutar desde SAP Business One. (Carga Reembolso -Err2)");
                    Environment.Exit(0);
                }

                if (ConectarSAP(strConnString))
                {
                    this.Text = $"CARGA REEMBOLSOS {Globals.rCompany.CompanyName.ToString().ToUpper()}";
                    //this.btnConnect.Enabled = false;
                    this.btnConnect.Text = "Desconectar";
                    this.btnProcesar.Enabled = true;
                    this.btnSimular.Enabled = true;
                }
            }
            else
            {
                try
                {
                    if (Globals.rCompany != null && Globals.rCompany.Connected)
                    {
                        Globals.rCompany.Disconnect();
                        Globals.rCompany = null;
                        rSboApp = null;
                        rSboGui = null;
                    }

                    this.Text = "CARGA REEMBOLSOS (Desconectado)";
                    this.btnConnect.Text = "Conectar";
                    this.btnProcesar.Enabled = false;
                    this.btnSimular.Enabled = false;

                    //MessageBox.Show("Se ha desconectado de SAP correctamente.",
                    //                "Desconexión",
                    //                MessageBoxButtons.OK,
                    //                MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al desconectarse de SAP: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            
        }

        private BoDocumentTypes? ConvertToDocType(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            // Si viene como string
            if (value is string str)
            {
                if (Enum.TryParse(str, true, out BoDocumentTypes result))
                    return result;

                if (int.TryParse(str, out int intVal))
                    return (BoDocumentTypes)intVal;
            }

            // Si viene como número (int)
            if (value is int intValue)
                return (BoDocumentTypes)intValue;

            // Si ya viene como enum
            if (value is BoDocumentTypes enumValue)
                return enumValue;

            return null;
        }

        private BoYesNoEnum? ConvertToYesNo(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is string str)
            {
                str = str.Trim().ToLowerInvariant();
                if (str == "y" || str == "s" || str == "yes" || str == "si" || str == "true" || str == "1")
                    return BoYesNoEnum.tYES;
                if (str == "n" || str == "no" || str == "false" || str == "0")
                    return BoYesNoEnum.tNO;

                if (Enum.TryParse(str, true, out BoYesNoEnum result))
                    return result;
            }

            if (value is int intValue)
                return (BoYesNoEnum)intValue;

            if (value is bool boolValue)
                return boolValue ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;

            if (value is BoYesNoEnum enumValue)
                return enumValue;

            return null;
        }

        private T TryGet<T>(Func<T> getter, string fieldName, bool obligatorio = false)
        {
            try
            {
                var value = getter();
                if (obligatorio && (value == null || (value is string s && string.IsNullOrWhiteSpace(s))))
                    throw new Exception($"El campo {fieldName} es obligatorio y no puede estar vacío.");
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer el campo {fieldName}: {ex.Message}");
            }
        }

    }
}
