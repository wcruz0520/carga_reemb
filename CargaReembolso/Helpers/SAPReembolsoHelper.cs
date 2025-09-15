using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CargaReembolso.Entidades;
using SAPbobsCOM;

namespace CargaReembolso.Helpers
{
    public class SAPReembolsoHelper
    {
        private Company _company;

        public SAPReembolsoHelper(Company company)
        {
            _company = company;
        }

        public bool CrearOActualizarReembolso(Reembolso reembolso, out string mensaje)
        {
            mensaje = "";
            try
            {
                CompanyService companyService = _company.GetCompanyService();
                GeneralService generalService = companyService.GetGeneralService("SS_REEMCAB");

                GeneralDataParams oParams = (GeneralDataParams)generalService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oParams.SetProperty("Code", reembolso.Code);

                GeneralData oGeneralData = null;
                bool exists = true;
                try
                {
                    oGeneralData = generalService.GetByParams(oParams);
                }
                catch
                {
                    exists = false;
                }

                if (!exists)
                {
                    oGeneralData = (GeneralData)generalService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                    oGeneralData.SetProperty("Code", reembolso.Code);
                    oGeneralData.SetProperty("Name", reembolso.Name);
                }

                if (exists)
                {
                    GeneralDataCollection oChildren = oGeneralData.Child("SS_REEMDET");
                    while (oChildren.Count > 0)
                        oChildren.Remove(0);
                }

                GeneralDataCollection oLines = oGeneralData.Child("SS_REEMDET");
                foreach (var det in reembolso.ReembolsoDet)
                {
                    if(reembolso.Code == det.Code)
                    {
                        GeneralData oLine = oLines.Add();

                        // Campos Alpha / Texto
                        oLine.SetProperty("U_SS_TipoId", det.SS_TipoId ?? "");
                        oLine.SetProperty("U_SS_IdProv", det.SS_IdProv ?? "");
                        oLine.SetProperty("U_SS_TipoComp", det.SS_TipoComp ?? "");
                        oLine.SetProperty("U_SS_Est", det.SS_Est ?? "");
                        oLine.SetProperty("U_SS_PtoEmi", det.SS_PtoEmi ?? "");
                        oLine.SetProperty("U_SS_NumAut", det.SS_NumAut ?? "");

                        // Fecha en formato YYYYMMDD
                        string fechafinal = det.SS_FecEmi.ToString("yyyyMMdd");
                        oLine.SetProperty("U_SS_FecEmi", fechafinal);

                        // Campos tipo Price: validar y enviar con punto decimal
                        oLine.SetProperty("U_SS_IVA0", SafeDouble(det.SS_IVA0));
                        oLine.SetProperty("U_SS_IvaDif0", SafeDouble(det.SS_IvaDif0));
                        oLine.SetProperty("U_SS_NoObjIVA", SafeDouble(det.SS_NoObjIVA));
                        oLine.SetProperty("U_SS_MontoIVA", SafeDouble(det.SS_MontoIVA));
                        oLine.SetProperty("U_SS_MontoICE", SafeDouble(det.SS_MontoICE));
                        oLine.SetProperty("U_SS_IvaExe", SafeDouble(det.SS_IvaExe));
                    }
                }

                if (exists)
                    generalService.Update(oGeneralData);
                else
                    generalService.Add(oGeneralData);

                mensaje = $"Reembolso {reembolso.Code} procesado correctamente";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"Error en procesamiento {reembolso.Code}. Exception: {ex.Message}";
                return false;
            }
        }

        // Convierte cualquier valor numérico a double seguro, devuelve 0 si es null o inválido
        private double SafeDouble(decimal? value)
        {
            if (value.HasValue)
                return Convert.ToDouble(value.Value, CultureInfo.InvariantCulture);
            return 0;
        }
    }
}
