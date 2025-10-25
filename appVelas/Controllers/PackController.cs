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
            return View(Packs);
        }

        // ------------------------------------- PACK ---------------------------------------------

        public async Task<IActionResult>  _CrearPackView()
        {
            return PartialView("Pack/_CrearPackView");
        }

        [HttpPost]
        public async Task<IActionResult>  _CrearPackView(Pack pack)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            await _packRepo.InsertarPackAsync(pack);


            return PartialView("Sucess", pack);

        }

        public async Task<IActionResult>  _ActPackView(Guid IDPack)
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
            return PartialView("Pack/_ActPackView", pack);
        }

        [HttpPost]
        public async Task<IActionResult>  _ActPackView(Pack pack)
        {
            await _packRepo.ActualizarPackAsync(pack);

            return PartialView("Sucess", pack);
        }

        public async Task<IActionResult>  _DetallesPackView()
        {
            var pack = await _packRepo.GetPacksAsync();

            //ViewData["PackS"] = Packs;
            return PartialView("Detalles/_DetallesPackView", pack);
        }

        public async Task<IActionResult>  _DetallesPackView1(Guid IDPack)
        {
            var pack = await _packRepo.BuscarPackAsync(IDPack);

            ViewData["PACK"] = pack;
            return PartialView("Detalles/_DetallesPackView1", pack);
        }

    }
}