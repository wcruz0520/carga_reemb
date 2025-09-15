using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargaReembolso.Entidades
{
    public class Reembolso
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<ReembolsoDet> ReembolsoDet { get; set; } = new List<ReembolsoDet>();
    }

    public class ReembolsoDet
    {
        public string Code { get; set; }
        public string SS_TipoId { get; set; }
        public string SS_IdProv { get; set; }
        public string SS_TipoComp { get; set; }
        public string SS_Est { get; set; }
        public string SS_PtoEmi { get; set; }
        public string SS_NumAut { get; set; }
        public DateTime SS_FecEmi { get; set; }
        public decimal SS_IVA0 { get; set; }
        public decimal SS_IvaDif0 { get; set; }
        public decimal SS_NoObjIVA { get; set; }
        public decimal SS_MontoIVA { get; set; }
        public decimal SS_MontoICE { get; set; }
        public decimal SS_IvaExe { get; set; }
    }
}
