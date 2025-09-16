using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace CargaReembolso.Interfaces
{
    public partial class FactInterface : UserControl
    {
        public FactInterface()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenExcelAndLoad(grdFact);
        }

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
                Anchor = placeholderGrid.Anchor,
                Dock = DockStyle.Fill
            };
            parent.Controls.Add(tabs);
            tabs.BringToFront();
            placeholderGrid.Visible = false;   // ocultamos el grid “placeholder”
            return tabs;
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
                        BackgroundColor = Color.White,
                        RowHeadersVisible = false
                    };
                    page.Controls.Add(grid);
                    tabs.TabPages.Add(page);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearTabsFor(grdFact);
        }

        private void ClearTabsFor(DataGridView placeholderGrid)
        {
            var parent = placeholderGrid.Parent;
            var tabName = placeholderGrid.Name + "Tabs";
            var tabs = parent.Controls.Find(tabName, false).FirstOrDefault() as TabControl;
            if (tabs != null)
            {
                tabs.TabPages.Clear();
                parent.Controls.Remove(tabs);
                placeholderGrid.Visible = true; // mostramos de nuevo el grid original
                placeholderGrid.RowHeadersVisible = false;
                Dock = DockStyle.Fill;
            }

            // Limpiamos también el grid placeholder
            placeholderGrid.DataSource = null;
            placeholderGrid.Rows.Clear();
        }

        public List<DataGridView> GetAllGrids()
        {
            var grids = new List<DataGridView>();

            // Buscar todos los TabControl dentro del UserControl (en todos los niveles)
            foreach (var tabControl in FindControlsRecursive<TabControl>(this))
            {
                foreach (TabPage tab in tabControl.TabPages)
                {
                    grids.AddRange(tab.Controls.OfType<DataGridView>());
                }
            }

            // Si no hay tabs, usar el grid placeholder
            if (grids.Count == 0 && grdFact.Visible && grdFact.DataSource != null)
            {
                grids.Add(grdFact);
            }

            return grids;
        }

        private IEnumerable<T> FindControlsRecursive<T>(Control parent) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control is T typed)
                    yield return typed;

                foreach (var child in FindControlsRecursive<T>(control))
                    yield return child;
            }
        }
    }
}
