using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Service.Interfaces;
using appVelas.Models;
using System.Diagnostics;

namespace appVelas.Controllers
{
    public class EndurecedorController : Controller
    {
        private readonly RepositoryEndurecedores _endurecedorRepo;

        public EndurecedorController(RepositoryEndurecedores endurecedorService)
        {
            _endurecedorRepo = endurecedorService;
        }

        public async Task<IActionResult> Index()
        {
            var Endurecedors = await _endurecedorRepo.GetEndurecedorsAsync();
            return View(Endurecedors);
        }

        // ------------------------------------- ENDURECEDOR ---------------------------------------------

        public async Task<PartialViewResult> _CrearEndurecedorView()
        {
            return PartialView("_CrearEndurecedorView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearEndurecedorView(Endurecedor end)
        {
             var response = await _endurecedorRepo.InsertarEndurecedorAsync(end);

            if (response.Data.IDEndurecedor != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDEndurecedoc = response.Data.IDEndurecedor });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDEndurecedor)
        {
            var end =  await _endurecedorRepo.BuscarEndurecedorAsync(IDEndurecedor);

            if (end == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna endurecedor con el IDEnd recibido. IDEnd = " + IDEndurecedor +
                        "Error en el Controller de la vista _ActEndurecedorView"
                });
            }
            else
            {
                ViewData["IDEnd"] = IDEndurecedor;
            }
            return View("~/Views/Endurecedor/_ActEndurecedorView.cshtml", end.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Endurecedor end)
        {
            var response = await _endurecedorRepo.ActualizarEndurecedorAsync(end.IDEndurecedor, end);

            if (response.Data.IDEndurecedor != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDMecha = response.Data.IDEndurecedor });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        public async Task<PartialViewResult> _DetallesEndurecedorView()
        {
            var end =  await _endurecedorRepo.GetEndurecedorsAsync();

            ViewData["Endurecedores"] = end.Data;
            return PartialView("~/Views/Endurecedor/_DetallesEndurecedorView.cshtml", end.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDEndurecedor)
        {
            var end =  await _endurecedorRepo.BuscarEndurecedorAsync(IDEndurecedor);

            ViewData["END"] = end.Data;
            return View("~/Views/Endurecedor/_DetallesEndurecedorView1.cshtml", end.Data);
        }

    }
}