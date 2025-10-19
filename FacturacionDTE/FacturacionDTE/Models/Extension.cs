using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Extension
{
    public int ExtensionId { get; set; }

    public int DocumentId { get; set; }

    public string? Nombre { get; set; }

    public string? Valor { get; set; }

    public virtual Document Document { get; set; } = null!;
}
