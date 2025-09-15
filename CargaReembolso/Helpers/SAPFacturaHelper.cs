using CargaReembolso.Entidades;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CargaReembolso.Helpers
{
    public class SAPFacturaHelper
    {
        private Company _company;

        public SAPFacturaHelper(Company company)
        {
            _company = company;
        }

        public bool CrearFactura(Factura factura, out string mensaje)
        {
            mensaje = "";
            Documents oFactura = null;

            try
            {
                oFactura = (Documents)_company.GetBusinessObject(BoObjectTypes.oInvoices);

                // ====== CABECERA ======
                oFactura.CardCode = factura.CardCode;          // Cliente
                oFactura.DocDate = factura.DocDate;            // Fecha contable
                oFactura.TaxDate = factura.TaxDate;            // Fecha de impuestos
                oFactura.DocDueDate = factura.DocDueDate;      // Fecha vencimiento

                if (factura.Series > 0)
                    oFactura.Series = factura.Series;          // Serie (NNM1)

                if (!string.IsNullOrEmpty(factura.DocCurrency))
                    oFactura.DocCurrency = factura.DocCurrency;

                if (!string.IsNullOrEmpty(factura.Comments))
                    oFactura.Comments = factura.Comments;

                if (!string.IsNullOrEmpty(factura.SS_Reembolsos))
                    oFactura.UserFields.Fields.Item("U_SS_Reembolsos").Value = factura.SS_Reembolsos  ?? "";

                // ====== LÍNEAS ======
                foreach (var linea in factura.Detalles)
                {
                    oFactura.Lines.ItemCode = linea.ItemCode;
                    oFactura.Lines.Quantity = SafeDouble(linea.Quantity);
                    oFactura.Lines.Price = SafeDouble(linea.Price);
                    oFactura.Lines.TaxCode = linea.TaxCode ?? "";
                    oFactura.Lines.WarehouseCode = linea.WarehouseCode ?? "";

                    oFactura.Lines.Add();
                }

                // ====== CREAR FACTURA ======
                int resultado = oFactura.Add();

                if (resultado != 0)
                {
                    string errorMsg = _company.GetLastErrorDescription();
                    mensaje = $"Error al crear factura: {errorMsg}";
                    return false;
                }
                else
                {
                    int docEntry = int.Parse(_company.GetNewObjectKey());
                    mensaje = $"Factura creada correctamente. DocEntry: {docEntry}";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al procesar factura. Exception: {ex.Message}";
                return false;
            }
            finally
            {
                if (oFactura != null)
                {
                    Marshal.ReleaseComObject(oFactura);
                    oFactura = null;
                }
            }
        }

        // Conversión segura para decimales
        private double SafeDouble(decimal? value)
        {
            if (value.HasValue)
                return Convert.ToDouble(value.Value, CultureInfo.InvariantCulture);
            return 0;
        }
    }
}
