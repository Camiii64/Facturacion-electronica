using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Codigo { get; set; }

    public decimal Precio { get; set; }

    public string? UnidadMedida { get; set; }

    public int? CategoriaId { get; set; }

    public int? TipoItem { get; set; }

    public bool? Estado { get; set; }

    public int Existencias { get; set; }

    public virtual CategoriaProducto? Categoria { get; set; }

    public virtual ICollection<DetalleDocumento> DetalleDocumentos { get; set; } = new List<DetalleDocumento>();
}
