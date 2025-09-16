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

        /// <summary>
        /// Crea o actualiza factura de ventas en SAP.
        /// </summary>
        public bool CrearOActualizarFactura(Factura factura, out string mensaje)
        {
            mensaje = "";
            Documents oFactura = null;

            try
            {
                // ====== VALIDACIONES DE CONEXIÓN ======
                if (Globals.rCompany == null)
                {
                    mensaje = "No existe conexión con SAP (rCompany es null).";
                    return false;
                }

                if (!Globals.rCompany.Connected)
                {
                    mensaje = "La conexión con SAP se ha perdido.";
                    return false;
                }

                // ====== CREAR OBJETO FACTURA ======
                oFactura = CrearObjetoFactura();
                if (oFactura == null)
                {
                    mensaje = "No se pudo crear el objeto oInvoices.";
                    return false;
                }

                // ====== VERIFICAR SI LA FACTURA EXISTE ======
                bool facturaExiste = false;
                if (factura.DocEntry > 0)
                {
                    facturaExiste = oFactura.GetByKey((int)factura.DocEntry);
                }

                if (facturaExiste)
                {
                    #region Actualizar factura
                    // Solo actualizar campos de usuario
                    if (!string.IsNullOrEmpty(factura.SS_Reembolsos))
                        oFactura.UserFields.Fields.Item("U_SS_Reembolsos").Value = factura.SS_Reembolsos ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_Est))
                        oFactura.UserFields.Fields.Item("U_SS_Est").Value = factura.SS_Est ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_Pemi))
                        oFactura.UserFields.Fields.Item("U_SS_Pemi").Value = factura.SS_Pemi ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_TipCom))
                        oFactura.UserFields.Fields.Item("U_SS_TipCom").Value = factura.SS_TipCom ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_FormaPagos))
                        oFactura.UserFields.Fields.Item("U_SS_FormaPagos").Value = factura.SS_FormaPagos ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_CONTRATO))
                        oFactura.UserFields.Fields.Item("U_SS_CONTRATO").Value = factura.SS_CONTRATO ?? "";

                    if (!string.IsNullOrEmpty(factura.SSLOCAL))
                        oFactura.UserFields.Fields.Item("U_SSLOCAL").Value = factura.SSLOCAL ?? "";

                    if (!string.IsNullOrEmpty(factura.Denominacion))
                        oFactura.UserFields.Fields.Item("U_Denominacion").Value = factura.Denominacion ?? "";

                    if (factura.SSCONTRATO > 0)
                        oFactura.UserFields.Fields.Item("U_SSCONTRATO").Value = factura.SSCONTRATO;

                    int resultado = oFactura.Update();
                    if (resultado != 0)
                    {
                        mensaje = $"Error al actualizar factura: {_company.GetLastErrorDescription()}";
                        return false;
                    }

                    mensaje = $"Factura actualizada correctamente. DocEntry: {factura.DocEntry}";
                    return true;
                    #endregion
                }
                else
                {
                    #region Crear nueva factura
                    oFactura.DocObjectCode = BoObjectTypes.oInvoices;
                    //oFactura.DocumentSubType = BoDocumentSubType.bod_None;

                    if (!Globals.rCompany.Connected)
                    {
                        mensaje = "La conexión con SAP se ha perdido.";
                        return false;
                    }

                    oFactura.CardCode = factura.CardCode.ToString();
                    oFactura.DocDate = (DateTime)factura.DocDate;
                    oFactura.TaxDate = (DateTime)factura.TaxDate;
                    oFactura.DocDueDate = (DateTime)factura.DocDueDate;

                    if (factura.Series > 0)
                        oFactura.Series = factura.Series;

                    if (factura.DocType.HasValue)
                        oFactura.DocType = factura.DocType.Value;

                    if (!string.IsNullOrEmpty(factura.DocCurrency))
                        oFactura.DocCurrency = factura.DocCurrency;

                    if (factura.HandWritten.HasValue)
                        oFactura.HandWritten = factura.HandWritten.Value;

                    if (!string.IsNullOrEmpty(factura.Comments))
                        oFactura.Comments = factura.Comments;

                    // Campos de usuario
                    if (!string.IsNullOrEmpty(factura.SS_Reembolsos))
                        oFactura.UserFields.Fields.Item("U_SS_Reembolsos").Value = factura.SS_Reembolsos ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_Est))
                        oFactura.UserFields.Fields.Item("U_SS_Est").Value = factura.SS_Est ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_Pemi))
                        oFactura.UserFields.Fields.Item("U_SS_Pemi").Value = factura.SS_Pemi ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_TipCom))
                        oFactura.UserFields.Fields.Item("U_SS_TipCom").Value = factura.SS_TipCom ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_FormaPagos))
                        oFactura.UserFields.Fields.Item("U_SS_FormaPagos").Value = factura.SS_FormaPagos ?? "";

                    if (!string.IsNullOrEmpty(factura.SS_CONTRATO))
                        oFactura.UserFields.Fields.Item("U_SS_CONTRATO").Value = factura.SS_CONTRATO ?? "";

                    if (!string.IsNullOrEmpty(factura.SSLOCAL))
                        oFactura.UserFields.Fields.Item("U_SSLOCAL").Value = factura.SSLOCAL ?? "";

                    if (!string.IsNullOrEmpty(factura.Denominacion))
                        oFactura.UserFields.Fields.Item("U_Denominacion").Value = factura.Denominacion ?? "";

                    if (factura.SSCONTRATO > 0)
                        oFactura.UserFields.Fields.Item("U_SSCONTRATO").Value = factura.SSCONTRATO;

                    // Líneas
                    foreach (var linea in factura.Detalles)
                    {
                        oFactura.Lines.ItemCode = linea.ItemCode;
                        oFactura.Lines.Quantity = SafeDouble(linea.Quantity);
                        oFactura.Lines.Price = SafeDouble(linea.Price);
                        oFactura.Lines.DiscountPercent = SafeDouble(linea.DiscPrcnt);
                        oFactura.Lines.TaxCode = linea.TaxCode ?? "";
                        oFactura.Lines.WarehouseCode = linea.WarehouseCode;
                        oFactura.Lines.Add();
                    }

                    int resultado = oFactura.Add();
                    if (resultado != 0)
                    {
                        mensaje = $"Error al crear factura: {_company.GetLastErrorDescription()}";
                        return false;
                    }

                    int docEntry = int.Parse(_company.GetNewObjectKey());
                    mensaje = $"Factura creada correctamente. DocEntry: {docEntry}";
                    return true;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al procesar factura: {ex.Message}";
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

        /// <summary>
        /// Helper para crear un objeto factura validando conexión y tipo.
        /// </summary>
        private Documents CrearObjetoFactura()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Globals.rCompany == null || !Globals.rCompany.Connected)
                throw new Exception("Sin conexión válida a SAP");

            Documents factura = null;
            try
            {
                factura = (Documents)Globals.rCompany.GetBusinessObject(BoObjectTypes.oInvoices);
                if (factura == null)
                    throw new Exception("No se pudo crear objeto Documents");

                return factura;
            }
            catch
            {
                if (factura != null)
                {
                    Marshal.ReleaseComObject(factura);
                }
                throw;
            }
        }

        public bool ValidarFactura(Factura factura, out string mensaje)
        {
            mensaje = "";

            try
            {
                if (string.IsNullOrEmpty(factura.CardCode))
                {
                    mensaje = "El campo CardCode (cliente) es obligatorio.";
                    return false;
                }

                if (factura.DocDate == DateTime.MinValue || factura.TaxDate == DateTime.MinValue)
                {
                    mensaje = $"La factura {factura.DocEntry} tiene fechas inválidas.";
                    return false;
                }

                if (factura.Detalles == null || factura.Detalles.Count == 0)
                {
                    mensaje = $"La factura {factura.DocEntry} no tiene líneas de detalle.";
                    return false;
                }

                foreach (var det in factura.Detalles)
                {
                    if (string.IsNullOrEmpty(det.ItemCode))
                    {
                        mensaje = $"Factura {factura.DocEntry} tiene líneas con ItemCode vacío.";
                        return false;
                    }
                    if (det.Quantity <= 0)
                    {
                        mensaje = $"Factura {factura.DocEntry} tiene cantidades inválidas.";
                        return false;
                    }
                }

                mensaje = $"Factura {factura.DocEntry} válida.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"Error validando factura {factura.DocEntry}. Exception: {ex.Message}";
                return false;
            }
        }

        private double SafeDouble(decimal? value)
        {
            if (value.HasValue)
                return Convert.ToDouble(value.Value, CultureInfo.InvariantCulture);
            return 0;
        }
    }
}
