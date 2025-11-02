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

        public async Task<PartialViewResult>  _CrearMoldeView()
        {
            return PartialView("_CrearMoldeView",  new Molde());
        }

        [HttpPost]
        public async Task<PartialViewResult>  _CrearMoldeView(Molde molde)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            await _moldeRepo.InsertarMoldeAsync(molde);


            return PartialView("Sucess", molde);

        }

        public async Task<PartialViewResult>  _ActMoldeView(Guid IDMolde)
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
                return PartialView("Molde/_CrearMoldeView", await _moldeRepo.BuscarMoldeAsync(IDMolde));
            }
        }

        [HttpPost]
        public async Task<PartialViewResult>  _ActMoldeView(Molde molde)
        {
            await _moldeRepo.ActualizarMoldeAsync(molde);

            return PartialView("Sucess", molde);
        }

        public async Task<PartialViewResult>  _DetallesMoldeView()
        {
            var moldes = await _moldeRepo.GetMoldesAsync();

            //ViewData["MoldeS"] = Moldes;
            return PartialView("Detalles/_DetallesMoldeView", moldes);
        }

        public async Task<PartialViewResult>  _DetallesMoldeView1(Guid IDMolde)
        {
            var mol = await _moldeRepo.BuscarMoldeAsync(IDMolde);

            ViewData["MOLDE"] = mol;
            return PartialView("Detalles/_DetallesMoldeView1", mol);
        }
    }
}