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
    public class MechaController : Controller
    {
        private readonly RepositoryMechas _mechaRepo;

        public MechaController(RepositoryMechas MechaService)
        {
            _mechaRepo = MechaService;
        }

        public async Task<IActionResult> Index()
        {
            var Mechas = await _mechaRepo.GetMechasAsync();
            return View(Mechas);
        }

        // ------------------------------------- MECHA ---------------------------------------------

        public async Task<IActionResult> _CrearMechaView()
        {
            return PartialView("Crear/_CrearMechaView");
        }

        [HttpPost]
        public async Task<IActionResult> _CrearMechaView(Mecha mecha)
        {
            await _mechaRepo.InsertarMechaAsync(mecha);
            return PartialView("Sucess");
        }

        public async Task<PartialViewResult>  _ActMechaView(Guid IDMecha)
        {
            var mecha = await _mechaRepo.BuscarMechaAsync(IDMecha);

            if (mecha == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna mecha con el IDMecha recibido. IDMecha = " + IDMecha +
                        "Error en el Controller de la vista _ActMechaView"
                });
            }
            else
            {
                ViewData["IDMecha"] = IDMecha;
                return PartialView("Actualizar/_ActMechaView", mecha);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult>  _ActMechaView(Mecha mecha)
        {
            await _mechaRepo.ActualizarMechaAsync(mecha);

            return PartialView("Sucess", mecha);
        }

        public async Task<PartialViewResult>  _DetallesMechaView()
        {
            var mechas = await _mechaRepo.GetMechasAsync();

            //ViewData["MechaS"] = Mechas;
            return PartialView("Detalles/_DetallesMechaView", mechas);
        }

        public async Task<PartialViewResult>  _DetallesMechaView1(Guid IDMecha)
        {
            var me = await _mechaRepo.BuscarMechaAsync(IDMecha);

            ViewData["MECHA"] = me;
            return PartialView("Detalles/_DetallesMechaView1", me);
        }
    }
}