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
            return PartialView("_CrearFragView",  new Fragancia());
        }

        [HttpPost]
        public async Task<IActionResult> _CrearFragView(Fragancia frag)
        {
            var response = await _fraganciaRepo.InsertarFraganciaAsync(frag);


            if (response.Data.IDFrag != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDFrag = response.Data.IDFrag });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDFrag)
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
                return View("~/Views/Fragancia/_ActFragView.cshtml", frag.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Fragancia frag)
        {
            var response = await _fraganciaRepo.ActualizarFraganciaAsync(frag.IDFrag, frag);


            if (response.Data.IDFrag != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDFrag = response.Data.IDFrag });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult>  _DetallesFragView()
        {
            var frag = await _fraganciaRepo.GetFraganciasAsync();

            ViewData["FRAGS"] = frag.Data;
            return PartialView("~/Views/Fragancia/_DetallesFragView.cshtml", frag.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDFrag)
        {
            var frag =  await _fraganciaRepo.BuscarFraganciaAsync(IDFrag);

            ViewData["FRAGS"] = frag.Data;
            return View("~/Views/Fragancia/_DetallesFragView1.cshtml", frag.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _fraganciaRepo.EliminarAsync(id);

            ViewData["Error"] = res.Error.Mensaje;
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesFragView");
        }
    }
}