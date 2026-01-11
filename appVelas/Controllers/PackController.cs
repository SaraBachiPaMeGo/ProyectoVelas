using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Diagnostics;

namespace appVelas.Controllers
{
    public class PackController : Controller
    {
        private readonly RepositoryPacks _packRepo;

        public PackController(RepositoryPacks PackService)
        {
            _packRepo = PackService;
        }

        public async Task<IActionResult> Index()
        {
            var Packs = await _packRepo.GetPacksAsync();
            return View(Packs.Data);
        }

        // ------------------------------------- PACK ---------------------------------------------

        public async Task<IActionResult>  _CrearPackView()
        {
            return PartialView("_CrearPackView");
        }

        [HttpPost]
        public async Task<IActionResult>  _CrearPackView(Pack pack)
        {
            //if (!ModelState.IsValid)
            //{
            //}
           var response= await _packRepo.InsertarPackAsync(pack);

            if (response.Data.IDPack != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPack = response.Data.IDPack });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }

        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDPack)
        {
            var pack = await _packRepo.BuscarPackAsync(IDPack);

            if (pack == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pack con el IDPack recibido. IDPack = " + IDPack +
                        "Error en el Controller de la vista _ActPackView"
                });
            }
            else
            {
                ViewData["IDPack"] = IDPack;
            }
            return View("~/Views/Pack/_ActPackView.cshtml", pack.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Pack pack)
        {
            var response = await _packRepo.ActualizarPackAsync(pack.IDPack,pack);


            if (response.Data.IDPack != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPack = response.Data.IDPack });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        public async Task<IActionResult>  _DetallesPackView()
        {
            var pack = await _packRepo.GetPacksAsync();

            ViewData["Packes"] = pack.Data;
            return PartialView("~/Views/Pack/_DetallesPackView.cshtml", pack.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDPack)
        {
            var pack = await _packRepo.BuscarPackAsync(IDPack);

            ViewData["PACK"] = pack.Data;
            return View("~/Views/Pack/_DetallesPackView1.cshtml", pack.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _packRepo.EliminarAsync(id);

if (res.Error != null){ViewData["Error"] = res.Error.Mensaje;}
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesPackView");
        }

    }
}