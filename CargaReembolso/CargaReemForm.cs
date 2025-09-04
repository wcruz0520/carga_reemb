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

namespace CargaReembolso
{
    public partial class CargaReemForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        private readonly Color ColorPrimary = Color.FromArgb(44, 76, 96);   // Azul barra superior / botones
        private readonly Color ColorPrimaryHover = Color.FromArgb(36, 62, 78);
        private readonly Color ColorBorder = Color.FromArgb(44, 76, 96);    // Borde
        private readonly Color ColorHeader = Color.FromArgb(224, 224, 224); // Encabezados grid
        private readonly Color ColorText = Color.White;
        private readonly int TopBarHeight = 30;
        private Rectangle _topBarRect;
        private Rectangle _closeButtonRect;
        private bool _hoverClose = false;
        public string[] strTest = new string[4];
        public string strConnString = "";
        public string sCookie;
        public bool conectarse = true;
        public int ret;
        public string strSQL;

        public SAPbouiCOM.Application rSboApp;
        public SAPbouiCOM.SboGuiApi rSboGui;
        public SAPbobsCOM.Company rCompany;

        public String Anio = DateTime.Today.Year.ToString(); 

        public CargaReemForm()
        {
            InitializeComponent();
            UpdateRects();
            ApplyTheme();
            UpdateRects();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = Properties.Resources.LogoV2;
            label3.Text = $"Copyright © {Anio} SOLSAP S.A.";

            strTest = System.Environment.GetCommandLineArgs();

            if (strTest.Length > 0)
            {
                if (strTest.Length > 1)
                {
                    if (strTest[0].LastIndexOf("\\") > 0)
                    {
                        strConnString = strTest[1];
                    }
                    else
                    {
                        strConnString = strTest[0];
                    }
                }
                else
                {
                    if (strTest[0].LastIndexOf("\\") == -1)
                    {
                        strConnString = strTest[0];
                    }
                    else
                    {
                        MessageBox.Show("El Add-on se debe ejecutar desde SAP Business One. (" + "Carga Reembolso" + "-Err1)");
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                MessageBox.Show("El Add-on se debe ejecutar desde SAP Business One. (" + "Carga Reembolso" + "-Err2)");
                Environment.Exit(0);
            }


            if (strConnString.Length > 0){
                try
                {
                    rSboGui = new SAPbouiCOM.SboGuiApi();
                    rCompany = new SAPbobsCOM.Company();

                    // Conexión con el UI
                    rSboGui.Connect(strConnString);
                    rSboApp = rSboGui.GetApplication();

                    if (conectarse)
                    {
                        rCompany = rSboApp.Company.GetDICompany();
                    }
                    else
                    {
                        sCookie = rCompany.GetContextCookie();
                        ret = rCompany.SetSboLoginContext(rSboApp.Company.GetConnectionContext(sCookie));
                        if(ret == 0)
                        {
                            ret = rCompany.Connect();
                            if(ret != 0)
                            {
                                rCompany.GetLastError(out ret, out strSQL);
                                rSboApp.StatusBar.SetText("Error al Conectar el Add-On " + "Carga Reembolso" + ": " + strSQL, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                Environment.Exit(0);

                            }
                        }
                        else
                        {
                            rSboApp.StatusBar.SetText("No se ha Conectado con AddOn " + "Carga Reembolso" + ": Error " + ret, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            Environment.Exit(0);
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            }

        }

        private void ApplyTheme()
        {
            // Fondo del form
            this.BackColor = Color.White;

            // --- Franja superior: reservamos área para pintar el "título" ---
            _topBarRect = new Rectangle(0, 0, this.ClientSize.Width, TopBarHeight);
            //this.Padding = new Padding(0, TopBarHeight, 0, 0); // baja el contenido

            // --- Botones principales ---
            StyleButton(btnProcesar);
            StyleButton(btnSimular);
            StyleButton(btnLimpUDO);
            StyleButton(btnLimpDoc);

            // Botones "..." (más pequeños, mismos colores)
            StyleSmallButton(btnCUDO);
            StyleSmallButton(btnCDOC);

            // Labels
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label2.ForeColor = Color.FromArgb(64, 64, 64);

            // Combos
            StyleCombo(cmbUDOs);
            StyleCombo(cmbDocs);

            // Grids
            StyleGrid(gridUDO);
            StyleGrid(gridDOC);

            // Redibujo
            this.Invalidate();
        }

        private void StyleButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = ColorPrimary;
            b.ForeColor = ColorText;
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.MouseOverBackColor = ColorPrimaryHover;
            b.FlatAppearance.MouseDownBackColor = ColorPrimaryHover;
        }

        private void StyleSmallButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = ColorPrimary;
            b.ForeColor = ColorText;
            b.Width = 36; // visual tipo "…"
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.MouseOverBackColor = ColorPrimaryHover;
            b.FlatAppearance.MouseDownBackColor = ColorPrimaryHover;
        }

        private void StyleCombo(ComboBox c)
        {
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            c.FlatStyle = FlatStyle.Standard;
            c.BackColor = Color.White;
            c.ForeColor = Color.FromArgb(40, 40, 40);
        }

        private void StyleGrid(DataGridView g)
        {
            //g.BackgroundColor = Color.White;
            //g.BorderStyle = BorderStyle.None;
            //g.EnableHeadersVisualStyles = false;

            //g.ColumnHeadersDefaultCellStyle.BackColor = ColorHeader;
            //g.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            //g.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorHeader;
            //g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);

            //g.DefaultCellStyle.BackColor = Color.White;
            //g.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            //g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 243, 249);
            //g.DefaultCellStyle.SelectionForeColor = Color.FromArgb(32, 32, 32);
            //g.GridColor = Color.FromArgb(230, 230, 230);

            //g.RowHeadersVisible = false;
            //g.ColumnHeadersHeight = 28;
        }

        // Pintamos barra superior + borde coloreado
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Barra superior
            using (var b = new SolidBrush(ColorPrimary))
                e.Graphics.FillRectangle(b, _topBarRect);

            // Texto del título
            using (var t = new SolidBrush(Color.White))
            using (var f = new Font("Segoe UI", 9F, FontStyle.Bold))
                e.Graphics.DrawString("CARGA DE REEMBOLSOS - " + rCompany.CompanyName.ToString().ToUpper(), f, t, 12, (_topBarRect.Height - f.Height) / 2);

            // Botón cerrar (rectángulo)
            int btnSize = 30;
            _closeButtonRect = new Rectangle(this.ClientSize.Width - btnSize, 0, btnSize, TopBarHeight);

            using (var b = new SolidBrush(_hoverClose ? Color.Red : ColorPrimary))
                e.Graphics.FillRectangle(b, _closeButtonRect);

            // Dibujar la "X"
            using (var pen = new Pen(Color.White, 2))
            {
                int padding = 8;
                e.Graphics.DrawLine(pen, _closeButtonRect.Left + padding, _closeButtonRect.Top + padding,
                                          _closeButtonRect.Right - padding, _closeButtonRect.Bottom - padding);
                e.Graphics.DrawLine(pen, _closeButtonRect.Right - padding, _closeButtonRect.Top + padding,
                                          _closeButtonRect.Left + padding, _closeButtonRect.Bottom - padding);
            }
        }

        // Redimensionar: mantener la barra superior al ancho
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _topBarRect = new Rectangle(0, 0, this.ClientSize.Width, TopBarHeight);
            UpdateRects();
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left &&
                _topBarRect.Contains(e.Location) &&
                !_closeButtonRect.Contains(e.Location))   // << clave
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_closeButtonRect.Contains(e.Location))
            {
                if (!_hoverClose)
                {
                    _hoverClose = true;
                    this.Invalidate(_closeButtonRect);
                }
            }
            else if (_hoverClose)
            {
                _hoverClose = false;
                this.Invalidate(_closeButtonRect);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_closeButtonRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                Application.Exit();
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

        private void UpdateRects()
        {
            _topBarRect = new Rectangle(0, 0, this.ClientSize.Width, TopBarHeight);
            int btnSize = 30;
            _closeButtonRect = new Rectangle(this.ClientSize.Width - btnSize, 0, btnSize, TopBarHeight);
        }

        private void btnCUDO_Click(object sender, EventArgs e)
        {
            OpenExcelAndLoad(gridUDO);
        }

        private TabControl EnsureTabControlFor(DataGridView placeholderGrid)
        {
            var parent = placeholderGrid.Parent;
            var tabName = placeholderGrid.Name + "Tabs";
            var existing = parent.Controls.Find(tabName, false).FirstOrDefault() as TabControl;
            if (existing != null) return existing;

            var tabs = new TabControl
            {
                Name = tabName,
                Bounds = placeholderGrid.Bounds,
                Anchor = placeholderGrid.Anchor
            };
            parent.Controls.Add(tabs);
            tabs.BringToFront();
            placeholderGrid.Visible = false;   // ocultamos el grid “placeholder”
            return tabs;
        }

        // Aplica tu estilo a cada DataGridView nuevo (reusa tu StyleGrid)
        private void ApplyGridStyle(DataGridView g)
        {
            // Usa tu método existente para estilizar
            StyleGrid(g); // ya lo tienes en tu form
        }

        // Carga un archivo Excel y crea una pestaña por cada hoja
        private void LoadExcelToTabs(string excelPath, DataGridView placeholderGrid)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = reader.AsDataSet(conf);

                var tabs = EnsureTabControlFor(placeholderGrid);
                tabs.TabPages.Clear();

                foreach (DataTable table in dataSet.Tables)
                {
                    var page = new TabPage
                    {
                        Text = string.IsNullOrWhiteSpace(table.TableName) ? "Hoja" : table.TableName
                    };

                    var grid = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        ReadOnly = false,
                        AllowUserToAddRows = false,
                        DataSource = table,
                        BackgroundColor = Color.White
                    };

                    ApplyGridStyle(grid);
                    page.Controls.Add(grid);
                    tabs.TabPages.Add(page);
                }
            }
        }

        // Diálogo de abrir Excel (solo .xlsx/.xls) y carga al grid destino
        private void OpenExcelAndLoad(DataGridView targetGrid)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccione archivo de Excel";
                ofd.Filter = "Archivos de Excel|*.xlsx;*.xls";
                ofd.Multiselect = false;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    LoadExcelToTabs(ofd.FileName, targetGrid);
                }
            }
        }

        private void btnCDOC_Click(object sender, EventArgs e)
        {
            OpenExcelAndLoad(gridDOC);
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessGridUDO();
                MessageBox.Show("Procesamiento finalizado", "Carga de UDO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar: " + ex.Message, "Carga de UDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessGridUDO()
        {
            var tabs = this.Controls.Find(gridUDO.Name + "Tabs", true).FirstOrDefault() as TabControl;
            if (tabs == null) return;

            foreach (TabPage page in tabs.TabPages)
            {
                var grid = page.Controls.OfType<DataGridView>().FirstOrDefault();
                if (grid == null) continue;
                var table = grid.DataSource as DataTable;
                if (table == null) continue;

                foreach (DataRow row in table.Rows)
                {
                    ProcessRow(page.Text, table, row);
                }
            }
        }

        private void ProcessRow(string tableName, DataTable table, DataRow row)
        {
            if (!tableName.StartsWith("@"))
            {
                tableName = "@" + tableName;
            }

            var rs = (SAPbobsCOM.Recordset)rCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            // Quoting rules for SQL Server and SAP HANA
            string qStart = rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB ? "\"" : "[";
            string qEnd = rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB ? "\"" : "]";

            string where = $"{qStart}Code{qEnd} = '{row["Code"]}'";

            if (table.Columns.Contains("LineId"))
            {
                where += $" AND {qStart}LineId{qEnd} = {row["LineId"]}";
            }

            string tableSql = $"{qStart}{tableName}{qEnd}";
            rs.DoQuery($"SELECT COUNT(*) FROM {tableSql} WHERE {where}");
            bool exists = Convert.ToInt32(rs.Fields.Item(0).Value) > 0;

            var setClauses = new List<string>();
            var cols = new List<string>();
            var vals = new List<string>();

            foreach (DataColumn col in table.Columns)
            {
                var colName = col.ColumnName;
                var rawVal = row[col]?.ToString();
                bool isKey = colName.Equals("Code", StringComparison.OrdinalIgnoreCase) ||
                             colName.Equals("LineId", StringComparison.OrdinalIgnoreCase);

                string formattedVal;
                if (string.IsNullOrWhiteSpace(rawVal))
                {
                    formattedVal = "NULL";
                }
                else if (DateTime.TryParse(rawVal, out var dateVal))
                {
                    formattedVal = $"'{dateVal:yyyyMMdd}'";
                }
                else if (double.TryParse(rawVal, out _))
                {
                    formattedVal = rawVal.Replace(',', '.');
                }
                else
                {
                    formattedVal = $"'{rawVal.Replace("'", "''")}'";
                }

                string colSql = $"{qStart}{colName}{qEnd}";
                if (exists)
                {
                    if (!isKey)
                    {
                        setClauses.Add($"{colSql} = {formattedVal}");
                    }
                }
                else
                {
                    cols.Add(colSql);
                    vals.Add(formattedVal);
                }
            }

            string sql;
            if (exists)
            {
                sql = $"UPDATE {tableSql} SET {string.Join(",", setClauses)} WHERE {where}";
            }
            else
            {
                sql = $"INSERT INTO {tableSql} ({string.Join(",", cols)}) VALUES ({string.Join(",", vals)})";
            }

            rs.DoQuery(sql);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(rs);
        }
    }
}
