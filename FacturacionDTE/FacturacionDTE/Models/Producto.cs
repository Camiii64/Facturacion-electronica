namespace FacturacionDTE.Models
{
        public class Producto
        {
            public int IdProducto { get; set; }
            public string Codigo { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public decimal PrecioUnitario { get; set; }
            public int Existencias { get; set; }
            public string Categoria { get; set; } = string.Empty;
            public int TipoItem { get; set; } = 1; // 1 = Bien, 2 = Servicio
            public bool Estado { get; set; } = true;
        }
    }
