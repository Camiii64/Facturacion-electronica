using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string? Rol { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
}
