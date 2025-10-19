using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Bitacora
{
    public int BitacoraId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Accion { get; set; }

    public string? Detalle { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
