using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargaReembolso.Entidades
{
    class Reembolso
    {
        public string code;
        public List<ReembolsoDet> reembolsodet;
    }

    class ReembolsoDet
    {
        string code;
        string SS_TipoId;
        string SS_IdProv;
        string SS_TipoComp;
        string SS_Est;
        string SS_PtoEmi;
        string SS_NumAut;
        DateTime SS_FecEmi;
        decimal SS_IVA0;
        decimal SS_IvaDif0;
        decimal SS_NoObjIVA;
        decimal SS_MontoIVA;
        decimal SS_MontoICE;
        decimal SS_IvaExe;
    }
}
