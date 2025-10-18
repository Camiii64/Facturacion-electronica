using FacturacionDTE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionDTE.Controllers
{
        public class FacturasController : Controller
        {
            public IActionResult Index()
            {
                var facturas = new List<Factura> {
                new Factura {
                    Id = 1,
                    NumeroFactura = "F001",
                    Empresa = new Empresa { Nombre = "Mi Empresa" },
                    Cliente = new Cliente { Nombre = "Cliente Demo" },
                    Lineas = new List<LineaFactura> {
                        new LineaFactura { Descripcion = "Servicio", Cantidad = 1, PrecioUnitario = 100 }
                    }
                }
            };

                return View(facturas);
            }

            public IActionResult Create()
            {
                return View();
            }

            public IActionResult Details(int id)
            {
                var factura = new Factura
                {
                    NumeroFactura = "F001",
                    Empresa = new Empresa { Nombre = "Mi Empresa" },
                    Cliente = new Cliente { Nombre = "Cliente Demo" },
                    Lineas = new List<LineaFactura> {
                    new LineaFactura { Descripcion = "Servicio", Cantidad = 1, PrecioUnitario = 100 }
                }
                };
                ViewBag.Xml = "<DTE>Ejemplo XML</DTE>";
                ViewBag.Json = "{ \"ejemplo\": true }";
                return View(factura);
            }
        }
    }
