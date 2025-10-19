using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Firma
{
    public int FirmaId { get; set; }

    public int DocumentId { get; set; }

    public DateTime? FechaFirma { get; set; }

    public string? FirmaXml { get; set; }

    public string? Certificado { get; set; }

    public virtual Document Document { get; set; } = null!;
}
