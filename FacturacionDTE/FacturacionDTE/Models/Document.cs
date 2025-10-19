using System;
using System.Collections.Generic;

namespace FacturacionDTE.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int EmisorId { get; set; }

    public int? ClienteId { get; set; }

    public string NumeroConsecutivo { get; set; } = null!;

    public string? NumeroFactura { get; set; }

    public DateTime FechaEmision { get; set; }

    public string? TipoDocumento { get; set; }

    public string? Moneda { get; set; }

    public string? Estado { get; set; }

    public decimal? Total { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetalleDocumento> DetalleDocumentos { get; set; } = new List<DetalleDocumento>();

    public virtual Emisor Emisor { get; set; } = null!;

    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();

    public virtual ICollection<Firma> Firmas { get; set; } = new List<Firma>();

    public virtual ICollection<MedioPago> MedioPagos { get; set; } = new List<MedioPago>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<Resuman> Resumen { get; set; } = new List<Resuman>();
}
