using FacturacionDTE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FacturacionDTE.Controllers
{
    public class FacturasController : Controller
    {
        private readonly DteDbContext _context;

        public FacturasController(DteDbContext context)
        {
            _context = context;
        }

        // Mostrar todas las facturas (DTEs)
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Documents
                .Include(f => f.Cliente)
                .Include(f => f.Emisor)
                .ToListAsync();

            return View(facturas);
        }

        // Detalles de una factura
        public async Task<IActionResult> Details(int id)
        {
            var factura = await _context.Documents
                .Include(f => f.Cliente)
                .Include(f => f.Emisor)
                .Include(f => f.DetalleDocumentos)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(f => f.DocumentId == id);

            if (factura == null)
                return NotFound();

            // Generar XML o JSON de ejemplo
            ViewBag.Xml = "<DTE><Factura>" + factura.NumeroFactura + "</Factura></DTE>";
            ViewBag.Json = System.Text.Json.JsonSerializer.Serialize(factura, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });

            return View(factura);
        }

        // Crear nueva factura
        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Emisores = _context.Emisors.ToList();
            ViewBag.Productos = _context.Productos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Document factura)
        {
            if (ModelState.IsValid)
            {
                factura.FechaEmision = DateTime.Now;
                //factura.Estado = "Emitida";
                factura.TipoDocumento = "Factura consumidor final";

                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Emisores = _context.Emisors.ToList();
            ViewBag.Productos = _context.Productos.ToList();
            return View(factura);
        }
    }
}

