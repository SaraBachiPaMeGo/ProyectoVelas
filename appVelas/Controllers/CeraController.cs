using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Repository;
using appVelas.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace appVelas.Controllers
{
    public class CeraController : Controller
    {
        private readonly RepositoryCeras _ceraRepo;

        public CeraController(RepositoryCeras ceraService)
        {
            _ceraRepo = ceraService;
        }

        public async Task<IActionResult> Index()
        {
            var ceras = await _ceraRepo.GetCerasAsync();
            return View(ceras);
        }
        
        // ------------------------------------- CERA ---------------------------------------------

        public PartialViewResult _CrearCeraView()
        {
            return PartialView("_CrearCeraView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearCeraView(Cera cera)
        {
            var response = await _ceraRepo.InsertarCeraAsync(cera);

            if (response.Data.IDCera != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDCera = response.Data.IDCera });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }

        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDCera)
        {
            var cera = await _ceraRepo.BuscarCeraAsync(IDCera);

            if (cera == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna cera con el IDCera recibido. IDCera = " + IDCera +
                        "Error en el Controller de la vista _ActCeraView"
                });
            }
            else
                ViewData["IDCera"] = IDCera;
                return View("~/Views/Cera/_ActCeraView.cshtml", cera.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Cera cera)
        {
            var response = await _ceraRepo.ActualizarCeraAsync(cera.IDCera, cera);

            if (response.Data.IDCera != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDCera = response.Data.IDCera });

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
            var cera = await _ceraRepo.EliminarAsync(id);

            if (cera.Error != null)
            {
                ViewData["Error"] = cera.Error.Mensaje;

            }
            else
            {
                ViewData["OK"] = cera.Data;
            }

            

            return RedirectToAction("_DetallesCeraView");
        }

        public async Task<IActionResult> _DetallesCeraView()
        {
            var cera = await _ceraRepo.GetCerasAsync();
            ViewBag.TotalCoste = cera.Data.Sum(x => x.Coste);

            return PartialView("~/Views/Cera/_DetallesCeraView.cshtml",cera.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDCera)
        {
            var cera = await _ceraRepo.BuscarCeraAsync(IDCera);

            ViewData["CERA"] = cera.Data;
            return View("~/Views/Cera/_DetallesCeraView1.cshtml", cera.Data);
        }

    }
}