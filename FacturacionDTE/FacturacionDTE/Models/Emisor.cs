using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Emisor
{
    public int EmisorId { get; set; }

    public string Nit { get; set; } = null!;

    public string Nrc { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
