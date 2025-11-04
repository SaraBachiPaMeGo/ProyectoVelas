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
        public async Task<PartialViewResult> _CrearMoldeView(Molde molde)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            await _moldeRepo.InsertarMoldeAsync(molde);


            return PartialView("Sucess", molde);

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
        public async Task<PartialViewResult> ActualizarView(Guid id, Molde molde)
        {
            await _moldeRepo.ActualizarMoldeAsync(id, molde);

            return PartialView("Sucess", molde);
        }

        public async Task<PartialViewResult>  _DetallesMoldeView()
        {
            var moldes = await _moldeRepo.GetMoldesAsync();

            //ViewData["MoldeS"] = Moldes;
            return PartialView("~/Views/Molde/_DetallesMoldeView.cshtml", moldes.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDMolde)
        {
            var mol = await _moldeRepo.BuscarMoldeAsync(IDMolde);

            ViewData["MOLDE"] = mol;
            return View("~/Views/Molde/_DetallesMoldeView1.cshtml", mol.Data);
        }
    }
}