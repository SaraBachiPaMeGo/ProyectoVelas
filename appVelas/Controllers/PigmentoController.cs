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
    public class PigmentoController : Controller
    {
        private readonly RepositoryPigmentos _pigmentoRepo;

        public PigmentoController(RepositoryPigmentos pigmentoService)
        {
            _pigmentoRepo = pigmentoService;
        }

        public async Task<IActionResult> Index()
        {
            var Pigmentos = await _pigmentoRepo.GetPigmentosAsync();
            return View(Pigmentos);
        }

        // ------------------------------------- PIGMENTO ---------------------------------------------

        public async Task<IActionResult> _CrearPigView()
        {
            return PartialView("Crear/_CrearPigView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearPigView(Pigmento pig)
        {
            await _pigmentoRepo.InsertarPigmentoAsync(pig);

            return PartialView("Sucess", pig);
        }


        public async Task<PartialViewResult>  _ActPigView(Guid IDPig)
        {
            var pig = await _pigmentoRepo.BuscarPigmentoAsync(IDPig);

            if (pig == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pigmento con el IDPig recibido. IDPig = " + IDPig +
                        "Error en el Controller de la vista _ActPigView"
                });
            }
            else
            {
                ViewData["IDPig"] = IDPig;
                return PartialView("Actualizar/_ActPigView", pig);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult>  _ActPigView(Pigmento pig)
        {
            await _pigmentoRepo.ActualizarPigmentoAsync(pig);

            return PartialView("Sucess", pig);
        }

        public async Task<PartialViewResult>  _DetallesPigView()
        {
            var pig = await _pigmentoRepo.GetPigmentosAsync();

            //ViewData["PigmentoS"] = Pigmentos;
            return PartialView("Detalles/_DetallesPigView", pig);
        }

        public async Task<PartialViewResult>  _DetallesPigView1(Guid IDPig)
        {
            var pig = await _pigmentoRepo.BuscarPigmentoAsync(IDPig);

            ViewData["PIG"] = pig;
            return PartialView("Detalles/_DetallesPigView1", pig);
        }

    }
}