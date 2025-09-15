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

        //public bool CrearFactura(Factura factura, out string mensaje)
        //{
        //    mensaje = "";
        //    Documents oFactura = null;

        //    try
        //    {
        //        oFactura = (Documents)_company.GetBusinessObject(BoObjectTypes.oInvoices);

        //        // ====== CABECERA ======
        //        oFactura.CardCode = factura.CardCode;          // Cliente
        //        oFactura.DocDate = factura.DocDate;            // Fecha contable
        //        oFactura.TaxDate = factura.TaxDate;            // Fecha de impuestos
        //        oFactura.DocDueDate = factura.DocDueDate;      // Fecha vencimiento

        //        if (factura.Series > 0)
        //            oFactura.Series = factura.Series;          // Serie (NNM1)

        //        if (!string.IsNullOrEmpty(factura.DocCurrency))
        //            oFactura.DocCurrency = factura.DocCurrency;

        //        if (!string.IsNullOrEmpty(factura.Comments))
        //            oFactura.Comments = factura.Comments;

        //        //Sección para llenar campos de usaurio FE

        //        if (!string.IsNullOrEmpty(factura.SS_Reembolsos))
        //            oFactura.UserFields.Fields.Item("U_SS_Reembolsos").Value = factura.SS_Reembolsos  ?? "";

        //        if (!string.IsNullOrEmpty(factura.SS_Est))
        //            oFactura.UserFields.Fields.Item("U_SS_Est").Value = factura.SS_Est ?? "";

        //        if (!string.IsNullOrEmpty(factura.SS_Pemi))
        //            oFactura.UserFields.Fields.Item("U_SS_Pemi").Value = factura.SS_Pemi ?? "";

        //        if (!string.IsNullOrEmpty(factura.SS_TipCom))
        //            oFactura.UserFields.Fields.Item("U_SS_TipCom").Value = factura.SS_TipCom ?? "";

        //        if (!string.IsNullOrEmpty(factura.SS_FormaPagos))
        //            oFactura.UserFields.Fields.Item("U_SS_FormaPagos").Value = factura.SS_FormaPagos ?? "";



        //        // ====== LÍNEAS ======
        //        foreach (var linea in factura.Detalles)
        //        {
        //            oFactura.Lines.ItemCode = linea.ItemCode;
        //            oFactura.Lines.Quantity = SafeDouble(linea.Quantity);
        //            oFactura.Lines.Price = SafeDouble(linea.Price);
        //            oFactura.Lines.DiscountPercent = SafeDouble(linea.DiscPrcnt);
        //            oFactura.Lines.TaxCode = linea.TaxCode ?? "";
        //            oFactura.Lines.WarehouseCode = linea.WarehouseCode ?? "";

        //            oFactura.Lines.Add();
        //        }

        //        // ====== CREAR FACTURA ======
        //        int resultado = oFactura.Add();

        //        if (resultado != 0)
        //        {
        //            string errorMsg = _company.GetLastErrorDescription();
        //            mensaje = $"Error al crear factura: {errorMsg}";
        //            return false;
        //        }
        //        else
        //        {
        //            int docEntry = int.Parse(_company.GetNewObjectKey());
        //            mensaje = $"Factura creada correctamente. DocEntry: {docEntry}";
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = $"Error al procesar factura. Exception: {ex.Message}";
        //        return false;
        //    }
        //    finally
        //    {
        //        if (oFactura != null)
        //        {
        //            Marshal.ReleaseComObject(oFactura);
        //            oFactura = null;
        //        }
        //    }
        //}

        // Conversión segura para decimales

        public bool CrearOActualizarFactura(Factura factura, out string mensaje)
        {
            mensaje = "";
            Documents oFactura = null;

            try
            {
                oFactura = (Documents)_company.GetBusinessObject(BoObjectTypes.oInvoices);

                // ====== VERIFICAR SI LA FACTURA EXISTE ======
                bool facturaExiste = false;
                if (factura.DocEntry > 0)
                {
                    facturaExiste = oFactura.GetByKey(factura.DocEntry);
                }

                if (facturaExiste)
                {
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

                    // Actualizar la factura
                    int resultado = oFactura.Update();
                    if (resultado != 0)
                    {
                        mensaje = $"Error al actualizar factura: {_company.GetLastErrorDescription()}";
                        return false;
                    }

                    mensaje = $"Factura actualizada correctamente. DocEntry: {factura.DocEntry}";
                    return true;
                }
                else
                {
                    // ====== CREAR NUEVA FACTURA ======
                    oFactura.CardCode = factura.CardCode;
                    oFactura.DocDate = factura.DocDate;
                    oFactura.TaxDate = factura.TaxDate;
                    oFactura.DocDueDate = factura.DocDueDate;

                    if (factura.Series > 0)
                        oFactura.Series = factura.Series;

                    if (!string.IsNullOrEmpty(factura.DocCurrency))
                        oFactura.DocCurrency = factura.DocCurrency;

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

                    // Líneas
                    foreach (var linea in factura.Detalles)
                    {
                        oFactura.Lines.ItemCode = linea.ItemCode;
                        oFactura.Lines.Quantity = SafeDouble(linea.Quantity);
                        oFactura.Lines.Price = SafeDouble(linea.Price);
                        oFactura.Lines.DiscountPercent = SafeDouble(linea.DiscPrcnt);
                        oFactura.Lines.TaxCode = linea.TaxCode ?? "";
                        oFactura.Lines.WarehouseCode = linea.WarehouseCode ?? "";
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
