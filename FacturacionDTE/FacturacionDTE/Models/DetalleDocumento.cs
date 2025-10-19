using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class DetalleDocumento
{
    public int DetalleId { get; set; }

    public int DocumentId { get; set; }

    public int ProductoId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Document Document { get; set; } = null!;

    public virtual ICollection<Impuesto> Impuestos { get; set; } = new List<Impuesto>();

    public virtual Producto Producto { get; set; } = null!;
}
