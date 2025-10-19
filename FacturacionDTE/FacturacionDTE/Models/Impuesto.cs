using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Impuesto
{
    public int ImpuestoId { get; set; }

    public int DetalleId { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal Tasa { get; set; }

    public decimal Monto { get; set; }

    public virtual DetalleDocumento Detalle { get; set; } = null!;
}
