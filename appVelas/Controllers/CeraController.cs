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
            return PartialView("Cera/_CrearCeraView");
        }

        [HttpPost]
        public async Task<PartialViewResult> _CrearCeraView(Cera cera)
        {
            var ceras = await _ceraRepo.InsertarCeraAsync(cera);

            return PartialView("Sucess", cera);

        }

        public async Task<PartialViewResult> _ActCeraView(Guid IDCera)
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
            return PartialView("Cera/_ActCeraView", cera);
        }

        [HttpPost]
        public async Task<PartialViewResult> _ActCeraView(Cera cera)
        {
            var ceras = await _ceraRepo.ActualizarCeraAsync(cera);

            return PartialView("Sucess", cera);
        }

        public async Task<PartialViewResult> _DetallesCeraView()
        {
            return PartialView("Detalles/_DetallesCeraView", await _ceraRepo.GetCerasAsync());
        }

        public async Task<PartialViewResult> _DetallesCeraView1(Guid IDCera)
        {
            var cera = await _ceraRepo.BuscarCeraAsync(IDCera);

            ViewData["CERA"] = cera;
            return PartialView("Detalles/_DetallesCeraView1", cera);
        }

    }
}