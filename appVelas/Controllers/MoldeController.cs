using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class MoldeController : Controller
    {
        private readonly RepositoryMoldes repo;

        public MoldeController(RepositoryMoldes repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- MOLDE ---------------------------------------------

        public PartialViewResult _CrearMoldeView()
        {
            return PartialView("Crear/_CrearMoldeView", new Molde());
        }

        [HttpPost]
        public PartialViewResult _CrearMoldeView(Molde molde)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarMolde(molde);


            return PartialView("Sucess", molde);

        }

        public PartialViewResult _ActMoldeView(Guid IDMolde)
        {
            Molde mol = this.repo.BuscarMolde(IDMolde);

            if (mol == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDMolde recibido. IDMolde = " + IDMolde +
                        "Error en el Controller de la vista _ActMoldeView"
                });
            }
            else
            {
                ViewData["IDMolde"] = IDMolde;
                return PartialView("Actualizar/_CrearMoldeView", this.repo.BuscarMolde(IDMolde));
            }
        }

        [HttpPost]
        public PartialViewResult _ActMoldeView(Molde molde)
        {
            this.repo.ActualizarMolde(molde);

            return PartialView("Sucess", molde);
        }

        public PartialViewResult _DetallesMoldeView()
        {
            List<Molde> moldes = this.repo.GetMoldes();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesMoldeView", moldes);
        }

        public PartialViewResult _DetallesMoldeView1(Guid IDMolde)
        {
            Molde mol = this.repo.BuscarMolde(IDMolde);

            ViewData["MOLDE"] = mol;
            return PartialView("Detalles/_DetallesMoldeView1", mol);
        }
    }
}