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

       [Route("/Mecha/Index")]
        public async Task<IActionResult> Index()
        {
            var Mechas = await _mechaRepo.GetMechasAsync();
            return View(Mechas);
        }

        // ------------------------------------- MECHA ---------------------------------------------
        [HttpGet]
        public async Task<IActionResult> _CrearMechaView()
        {
            try
            {
                return PartialView("_CrearMechaView");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> _CrearMechaView(Mecha mecha)
        {
            
            try
            {
                var mech = await _mechaRepo.InsertarMechaAsync(mecha);
                if (mech.Data.IDMecha != Guid.Empty)
                {
                    return RedirectToAction("DetallesView1", new { IDMecha = mech.Data.IDMecha });

                }
                else
                {
                    ViewData["Error"] = mech.Error.Mensaje;

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return PartialView("~/Views/Mecha/DetallesMechaView1.cshtml", mecha.IDMecha);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDMecha)
        {
            try
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
                    return View("~/Views/Mecha/_ActMechaView.cshtml", mecha.Data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Mecha mecha)
        {
            var response = await _mechaRepo.ActualizarMechaAsync(mecha.IDMecha, mecha);

            if (response.Data.IDMecha != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDMecha = response.Data.IDMecha });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> _DetallesMechaView()
        {
            try
            {
                var mechas = await _mechaRepo.GetMechasAsync();

                //ViewData["MechaS"] = Mechas;
                return PartialView("~/Views/Mecha/_DetallesMechaView.cshtml", mechas.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDMecha)
        {
            try
            {
                var me = await _mechaRepo.BuscarMechaAsync(IDMecha);

                ViewData["MECHA"] = me.Data;
                //return PartialView("DetallesView1/_DetallesMechaView1", me);
                return View("~/Views/Mecha/_DetallesMechaView1.cshtml", me.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _mechaRepo.EliminarAsync(id);

            ViewData["Error"] = res.Error.Mensaje;
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesMechaView");
        }
    }
}