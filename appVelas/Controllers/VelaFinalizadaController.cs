using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace appVelas.Controllers
{
    public class VelaFinalizadaController : Controller
    {
        private readonly RepositoryVelasFinalizadas _VelaFinalizadaRepo;

        public VelaFinalizadaController(RepositoryVelasFinalizadas VelaFinalizadaService)
        {
            _VelaFinalizadaRepo = VelaFinalizadaService;
        }

        [Route("/VelaFinalizada/Index")]
        public async Task<IActionResult> Index()
        {
            var VelaFinalizadas = await _VelaFinalizadaRepo.GetVelaFinalizadasAsync();
            return View(VelaFinalizadas);
        }

        // ------------------------------------- VelaFinalizada ---------------------------------------------
        [HttpGet]
        public async Task<IActionResult> _CrearVelaFinalizadaView()
        {
            try
            {
                return PartialView("_CrearVelaFinalizadaView");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> _CrearVelaFinalizadaView(VelaFinalizada VelaFinalizada)
        {

            try
            {
                var response = await _VelaFinalizadaRepo.InsertarVelaFinalizadaAsync(VelaFinalizada);
                if (response.Data.IDVelaFin != Guid.Empty)
                {
                    return RedirectToAction("DetallesView1", new { IDVelaFin = response.Data.IDVelaFin });

                }
                else
                {
                    ViewData["Error"] = response.Error.Mensaje;

                    return View();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDVelaFinalizada)
        {
            try
            {
                var VelaFinalizada = await _VelaFinalizadaRepo.BuscarVelaFinalizadaAsync(IDVelaFinalizada);

                if (VelaFinalizada == null)
                {
                    return PartialView("Error", new
                        ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        Mensaje = "No se encontró ninguna VelaFinalizada con el IDVelaFinalizada recibido. IDVelaFinalizada = " + IDVelaFinalizada +
                            "Error en el Controller de la vista _ActVelaFinalizadaView"
                    });
                }
                else
                {
                    ViewData["IDVelaFinalizada"] = IDVelaFinalizada;
                    return View("~/Views/VelaFinalizada/_ActVelaFinalizadaView.cshtml", VelaFinalizada.Data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(VelaFinalizada VelaFinalizada)
        {
            var response = await _VelaFinalizadaRepo.ActualizarVelaFinalizadaAsync(VelaFinalizada.IDVelaFin, VelaFinalizada);

            if (response.Data.IDVelaFin != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDVelaFin = response.Data.IDVelaFin });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> _DetallesVelaFinalizadaView()
        {
            try
            {
                var VelaFinalizadas = await _VelaFinalizadaRepo.GetVelaFinalizadasAsync();

                //ViewData["VelaFinalizadaS"] = VelaFinalizadas;
                return PartialView("~/Views/VelaFinalizada/_DetallesVelaFinalizadaView.cshtml", VelaFinalizadas.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDVelaFinalizada)
        {
            try
            {
                var me = await _VelaFinalizadaRepo.BuscarVelaFinalizadaAsync(IDVelaFinalizada);

                ViewData["VelaFinalizada"] = me.Data;
                //return PartialView("DetallesView1/_DetallesVelaFinalizadaView1", me);
                return View("~/Views/VelaFinalizada/_DetallesVelaFinalizadaView1.cshtml", me.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var res = await _VelaFinalizadaRepo.EliminarAsync(id);

            ViewData["Error"] = res.Error.Mensaje;
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesVelaFinalizadaView");
        }
    }
}