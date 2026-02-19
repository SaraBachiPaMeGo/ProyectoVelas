using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Repository;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace appVelas.Controllers
{
    public class InventarioController : Controller
    {
        private readonly RepositoryInventarios _InventarioRepo;

        public InventarioController(RepositoryInventarios InventarioService)
        {
            _InventarioRepo = InventarioService;
        }

        public async Task<IActionResult> Index()
        {
            var Inventarios = await _InventarioRepo.GetInventariosAsync();
            return View(Inventarios);
        }

        // ------------------------------------- Inventario ---------------------------------------------

        public PartialViewResult _CrearInventarioView()
        {
            return PartialView("_CrearInventarioView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearInventarioView(Inventario inv)
        {
            var response = await _InventarioRepo.InsertarInventarioAsync(inv);

            if (response.Data.IDInventario != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDInventario = response.Data.IDInventario });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }

        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDInventario)
        {
            var Inventario = await _InventarioRepo.BuscarInventarioAsync(IDInventario);

            if (Inventario == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna Inventario con el IDInventario recibido. IDInventario = " + IDInventario +
                        "Error en el Controller de la vista _ActInventarioView"
                });
            }
            else
                ViewData["IDInventario"] = IDInventario;
            return View("~/Views/Inventario/_ActInventarioView.cshtml", Inventario.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Inventario Inventario)
        {
            var response = await _InventarioRepo.ActualizarInventarioAsync(Inventario.IDInventario, Inventario);

            if (response.Data.IDInventario != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDInventario = response.Data.IDInventario });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Inventario = await _InventarioRepo.EliminarAsync(id);
            if (Inventario.Error != null)
            {
                ViewData["Error"] = Inventario.Error.Mensaje;

            }
            else
            {
                ViewData["OK"] = Inventario.Data;

            }

            return RedirectToAction("_DetallesInventarioView");
        }

        public async Task<IActionResult> _DetallesInventarioView()
        {
            var inv = await _InventarioRepo.GetInventariosAsync();
            ViewBag.TotalCoste = inv.Data.Sum(x => x.Coste);


            return PartialView("~/Views/Inventario/_DetallesInventarioView.cshtml", inv.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDInventario)
        {
            var Inventario = await _InventarioRepo.BuscarInventarioAsync(IDInventario);

            ViewData["Inventario"] = Inventario.Data;
            return View("~/Views/Inventario/_DetallesInventarioView1.cshtml", Inventario.Data);
        }

    }
}