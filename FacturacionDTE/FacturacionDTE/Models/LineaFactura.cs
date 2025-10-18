namespace FacturacionDTE.Models
{
        public class LineaFactura
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public decimal Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal TotalLinea => Cantidad * PrecioUnitario;
        }
    }
