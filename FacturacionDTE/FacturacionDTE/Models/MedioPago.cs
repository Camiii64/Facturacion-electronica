using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class MedioPago
{
    public int MedioPagoId { get; set; }

    public int DocumentId { get; set; }

    public string? CodigoMedio { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Monto { get; set; }

    public string? Banco { get; set; }

    public string? NumeroOperacion { get; set; }

    public virtual Document Document { get; set; } = null!;
}
