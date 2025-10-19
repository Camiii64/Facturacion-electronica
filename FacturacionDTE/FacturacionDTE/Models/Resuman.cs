using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Resuman
{
    public int ResumenId { get; set; }

    public int DocumentId { get; set; }

    public decimal? MontoExento { get; set; }

    public decimal? MontoGrabado { get; set; }

    public decimal? MontoImpuestos { get; set; }

    public decimal? TotalVenta { get; set; }

    public decimal? TotalPagado { get; set; }

    public virtual Document Document { get; set; } = null!;
}
