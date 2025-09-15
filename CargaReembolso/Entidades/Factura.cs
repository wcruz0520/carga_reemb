using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargaReembolso.Entidades
{
    public class Factura
    {
        public int DocEntry { get; set; }
        public string CardCode { get; set; }            // Cliente
        public DateTime DocDate { get; set; }           // Fecha contable
        public DateTime TaxDate { get; set; }           // Fecha de impuestos
        public DateTime DocDueDate { get; set; }        // Fecha vencimiento
        public int Series { get; set; }                 // Serie (NNM1.Series)
        public string DocCurrency { get; set; }         // Moneda
        public string Comments { get; set; }            // Comentarios
        public string SS_Reembolsos { get; set; }
        public string SS_Est { get; set; }
        public string SS_Pemi { get; set; }
        public string SS_TipCom { get; set; }
        public string SS_FormaPagos { get; set; }
        public List<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();
    }

    public class FacturaDetalle
    {
        public string ItemCode { get; set; }            // Código de artículo
        public decimal Quantity { get; set; }           // Cantidad
        public decimal? Price { get; set; }             // Precio unitario
        public decimal? DiscPrcnt { get; set; }
        public string TaxCode { get; set; }             // Código de impuesto
        public string WarehouseCode { get; set; }       // Almacén
    }
}
