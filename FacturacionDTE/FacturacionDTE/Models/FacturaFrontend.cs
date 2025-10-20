using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models
{
    public class FacturaFrontend
    {
        public string TipoDte { get; set; } = string.Empty;
        public string NumeroControl { get; set; } = string.Empty;
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteDocumento { get; set; } = string.Empty;
        public string ClienteDepartamento { get; set; } = string.Empty;
        public string ClienteMunicipio { get; set; } = string.Empty;

        public decimal TotalNoSujetas { get; set; }
        public decimal TotalExentas { get; set; }
        public decimal TotalGravadas { get; set; }
        public decimal Iva { get; set; }
        public decimal TotalPagar { get; set; }

        public List<ItemFactura> Detalle { get; set; } = new();
    }

    public class ItemFactura
    {
        public int NroLinea { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal VentaGravada { get; set; }
        public decimal Subtotal { get; set; }
    }
}
