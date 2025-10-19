using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Mensaje
{
    public int MensajeId { get; set; }

    public int DocumentId { get; set; }

    public string? Codigo { get; set; }

    public string? Texto { get; set; }

    public virtual Document Document { get; set; } = null!;
}
