using FacturacionDTE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionDTE.Controllers
{
        public class ProductosController : Controller
        {
            // Simulación en memoria (en un proyecto real usarías una base de datos)
            private static List<Producto> productos = new List<Producto>();

            public IActionResult Index()
            {
                return View(productos);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Producto nuevo)
            {
                nuevo.IdProducto = productos.Count + 1;
                productos.Add(nuevo);
                return RedirectToAction("Index");
            }

            public IActionResult Edit(int id)
            {
                var prod = productos.FirstOrDefault(p => p.IdProducto == id);
                if (prod == null) return NotFound();
                return View(prod);
            }

            [HttpPost]
            public IActionResult Edit(Producto editado)
            {
                var prod = productos.FirstOrDefault(p => p.IdProducto == editado.IdProducto);
                if (prod == null) return NotFound();

                prod.Nombre = editado.Nombre;
                prod.Descripcion = editado.Descripcion;
                prod.PrecioUnitario = editado.PrecioUnitario;
                prod.Existencias = editado.Existencias;
                prod.Categoria = editado.Categoria;
                prod.TipoItem = editado.TipoItem;
                prod.Estado = editado.Estado;

                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                var prod = productos.FirstOrDefault(p => p.IdProducto == id);
                if (prod != null) productos.Remove(prod);
                return RedirectToAction("Index");
            }
        }
    }
