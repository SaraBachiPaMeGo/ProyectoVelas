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
    public class FraganciaController : Controller
    {
        private readonly RepositoryFragancias _fraganciaRepo;

        public FraganciaController(RepositoryFragancias fraganciaService)
        {
            _fraganciaRepo = fraganciaService;
        }

        public async Task<IActionResult> Index()
        {
            var Fragancias = await _fraganciaRepo.GetFraganciasAsync();
            return View(Fragancias);
        }

        // ------------------------------------- FRAGANCIA ---------------------------------------------

        public async Task<IActionResult> _CrearFragView()
        {
            return PartialView("Crear/_CrearFragView",  new Fragancia());
        }

        [HttpPost]
        public async Task<IActionResult> _CrearFragView(Fragancia frag)
        {
            await _fraganciaRepo.InsertarFraganciaAsync(frag);
            return PartialView("Sucess", frag);
        }

        public async Task<PartialViewResult>  _ActFragView(Guid IDFrag)
        {
            var frag = await _fraganciaRepo.BuscarFraganciaAsync(IDFrag);

            if (frag == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna fragancua con el IDFrag recibido. IDFrag = " + IDFrag +
                        "Error en el Controller de la vista _ActFragView"
                });
            }
            else
            {
                ViewData["IDFrag"] = IDFrag;
                return PartialView("Actualizar/_ActFragView", frag);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> _ActFragView(Fragancia frag)
        {
            await _fraganciaRepo.ActualizarFraganciaAsync(frag);

            return PartialView("Sucess", frag);
        }

        public async Task<PartialViewResult>  _DetallesFragView()
        {
            var frag = await _fraganciaRepo.GetFraganciasAsync();

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView", frag);
        }

        public async Task<PartialViewResult>  _DetallesFragView1(Guid IDFrag)
        {
            var frag =  await _fraganciaRepo.BuscarFraganciaAsync(IDFrag);

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView1", frag);
        }
    }
}