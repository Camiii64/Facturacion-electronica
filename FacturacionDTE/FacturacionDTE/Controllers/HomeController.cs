using System.Diagnostics;
using FacturacionDTE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionDTE.Controllers
{
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                ViewBag.TotalFacturas = 3;
                ViewBag.Empresas = 1;
                ViewBag.Clientes = 2;
                ViewBag.Productos = 5;
                return View();
            }
        }
    }
