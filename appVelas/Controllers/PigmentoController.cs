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
            return PartialView("_CrearPigView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearPigView(Pigmento pig)
        {
            var response = await _pigmentoRepo.InsertarPigmentoAsync(pig);


            if (response.Data.IDPig != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPig = response.Data.IDPig });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }


        public async Task<IActionResult> ActualizarView(Guid IDPig)
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
                return View("~/Views/Pigmento/_ActPigView.cshtml", pig.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Pigmento pig)
        {
            var response = await _pigmentoRepo.ActualizarPigmentoAsync(pig.IDPig, pig);


            if (response.Data.IDPig != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPig = response.Data.IDPig });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult>  _DetallesPigView()
        {
            try
            {
                var pig = await _pigmentoRepo.GetPigmentosAsync();

                ViewData["PigmentoS"] = pig.Data;
                return PartialView("~/Views/Pigmento/_DetallesPigView.cshtml", pig.Data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error: {ex.Message}");

            }

        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDPig)
        {
            try
            {
                var pig = await _pigmentoRepo.BuscarPigmentoAsync(IDPig);

                ViewData["PIG"] = pig.Data;
                return View("~/Views/Pigmento/_DetallesPigView1.cshtml", pig.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
           
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _pigmentoRepo.EliminarAsync(id);

            ViewData["Error"] = res.Error.Mensaje;
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesPigView");
        }

    }
}