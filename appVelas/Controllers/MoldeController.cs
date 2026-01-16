using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace appVelas.Controllers
{
    public class MoldeController : Controller
    {
        private readonly RepositoryMoldes _moldeRepo;

        public MoldeController(RepositoryMoldes moldeService)
        {
            _moldeRepo = moldeService;
        }

        public async Task<IActionResult> Index()
        {
            var Moldes = await _moldeRepo.GetMoldesAsync();
            return View(Moldes);
        }

        // ------------------------------------- MOLDE ---------------------------------------------

        public async Task<PartialViewResult> _CrearMoldeView()
        {
            return PartialView("_CrearMoldeView", new Molde());
        }

        [HttpPost]
        public async Task<IActionResult> _CrearMoldeView(Molde molde, IFormFile file)
        {
            using var form = new MultipartFormDataContent();

            // 🔹 Imagen
            if (file != null && file.Length > 0)
            {
                var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                form.Add(streamContent, "file", file.FileName);
            }


            // 🔹 Datos del Molde
            form.Add(new StringContent(molde.MoldeNombre ?? ""), "MoldeNombre");
            form.Add(new StringContent(molde.Coste.ToString()), "Coste");
            form.Add(new StringContent(molde.Tipo ?? ""), "Tipo");
            form.Add(new StringContent(molde.CMMecha.ToString()), "CMMecha");
            form.Add(new StringContent(molde.GramCera.ToString()), "GramCera");
            form.Add(new StringContent(molde.Medidas ?? ""), "Medidas");
            form.Add(new StringContent(molde.Duracion.ToString()), "Duracion");
            form.Add(new StringContent(molde.Observ ?? ""), "Observ");
            form.Add(new StringContent(molde.CompradoEn ?? ""), "CompradoEn");
            form.Add(new StringContent(molde.Firma ?? ""), "Firma");
            form.Add(new StringContent(molde.Cantidad.ToString()), "Cantidad");
            form.Add(new StringContent(molde.MilAgua.ToString()), "MilAgua");
            form.Add(new StringContent(molde.IDVela?.ToString() ?? ""), "IDVela");

            var response = await _moldeRepo.InsertarMoldeAsync(form);

            if (response.Data.IDMolde != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDMolde = response.Data.IDMolde });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDMolde)
        {
            var mol = await _moldeRepo.BuscarMoldeAsync(IDMolde);

            if (mol == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna Molde con el IDMolde recibido. IDMolde = " + IDMolde +
                        "Error en el Controller de la vista _ActMoldeView"
                });
            }
            else
            {
                ViewData["IDMolde"] = IDMolde;
                var moldes = await _moldeRepo.BuscarMoldeAsync(IDMolde);
                return View("~/Views/Molde/_ActMoldeView.cshtml", moldes.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Molde molde)
        {
            var response = await _moldeRepo.ActualizarMoldeAsync(molde.IDMolde, molde);

            if (response.Data.IDMolde != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDMolde = response.Data.IDMolde });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        public async Task<IActionResult> _DetallesMoldeView()
        {
            var moldes = await _moldeRepo.GetMoldesAsync();

            ViewData["Moldes"] = moldes.Data;
            return PartialView("~/Views/Molde/_DetallesMoldeView.cshtml", moldes.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDMolde)
        {
            var mol = await _moldeRepo.BuscarMoldeAsync(IDMolde);

            ViewData["MOLDE"] = mol.Data;
            return View("~/Views/Molde/_DetallesMoldeView1.cshtml", mol.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _moldeRepo.EliminarAsync(id);

            if (res.Error != null) { ViewData["Error"] = res.Error.Mensaje; }
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesMoldeView");
        }

        [HttpGet]
        [Route("Molde/Imagen/{id}")]
        public async Task<IActionResult> ObtenerImagen(Guid id)
        {
            var mol = await _moldeRepo.BuscarMoldeAsync(id);

            if (mol?.Data?.Image == null || mol.Data.Image.Length == 0)
                return NotFound();

            return File(mol.Data.Image, mol.Data.ImagenContentType);
        }

    }
}