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
            return PartialView("Crear/_CrearEndurecedorView");
        }

        [HttpPost]
        public async Task<PartialViewResult> _CrearEndurecedorView(Endurecedor end)
        {
            //if (!ModelState.IsValid)
            //{
            //}
             await _endurecedorRepo.InsertarEndurecedorAsync(end);


            return PartialView("Sucess", end);

        }

        public async Task<PartialViewResult> _ActEndurecedorView(Guid IDEnd)
        {
            var end =  await _endurecedorRepo.BuscarEndurecedorAsync(IDEnd);

            if (end == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna endurecedor con el IDEnd recibido. IDEnd = " + IDEnd +
                        "Error en el Controller de la vista _ActEndurecedorView"
                });
            }
            else
            {
                ViewData["IDEnd"] = IDEnd;
            }
            return PartialView("Actualizar/_ActEndurecedorView", end);
        }

        [HttpPost]
        public async Task<PartialViewResult> _ActEndurecedorView(Endurecedor end)
        {
             await _endurecedorRepo.ActualizarEndurecedorAsync(end);

            return PartialView("Sucess", end);
        }

        public async Task<PartialViewResult> _DetallesEndurecedorView()
        {
            var end =  await _endurecedorRepo.GetEndurecedorsAsync();

            //ViewData["EndurecedorS"] = Endurecedors;
            return PartialView("Detalles/_DetallesEndurecedorView", end);
        }

        public async Task<PartialViewResult> _DetallesEndurecedorView1(Guid IDEnd)
        {
            var end =  await _endurecedorRepo.BuscarEndurecedorAsync(IDEnd);

            ViewData["END"] = end;
            return PartialView("Detalles/_DetallesEndurecedorView1", end);
        }

    }
}