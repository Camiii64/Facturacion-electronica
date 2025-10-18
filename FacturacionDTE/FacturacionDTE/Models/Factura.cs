    using System;
    using System.Collections.Generic;

    namespace FacturacionDTE.Models
    {
        public class Factura
        {
            public int Id { get; set; }
            public string NumeroFactura { get; set; }
            public DateTime FechaEmision { get; set; } = DateTime.Now;
            public Empresa Empresa { get; set; }
            public Cliente Cliente { get; set; }
            public List<LineaFactura> Lineas { get; set; } = new();
            public decimal SubTotal => CalcularSubTotal();
            public decimal Impuesto => Math.Round(SubTotal * 0.13m, 2);
            public decimal Total => SubTotal + Impuesto;

            private decimal CalcularSubTotal()
            {
                decimal suma = 0;
                foreach (var l in Lineas)
                    suma += l.TotalLinea;
                return suma;
            }
        }
    }
