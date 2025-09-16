using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace appVelas.Controllers
{
    public class CeraController : Controller
    {
        private readonly RepositoryCeras repo;

        public CeraController(RepositoryCeras repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- CERA ---------------------------------------------

        public PartialViewResult _CrearCeraView()
        {
            return PartialView("Crear/_CrearCeraView");
        }

        [HttpPost]
        public async Task<PartialViewResult> _CrearCeraView(Cera cera)
        {
            this.repo.InsertarCera(cera);

            return PartialView("Sucess", cera);

        }

        public PartialViewResult _ActCeraView(Guid IDCera)
        {
            Cera cera = this.repo.BuscarCera(IDCera);

            if (cera == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna cera con el IDCera recibido. IDCera = " + IDCera +
                        "Error en el Controller de la vista _ActCeraView"
                });
            }
            else
                ViewData["IDCera"] = IDCera;
            return PartialView("Actualizar/_ActCeraView", cera);
        }

        [HttpPost]
        public PartialViewResult _ActCeraView(Cera cera)
        {
            this.repo.ActualizarCera(cera);

            return PartialView("Sucess", cera);
        }

        public async Task<PartialViewResult> _DetallesCeraView()
        {
            return PartialView("Detalles/_DetallesCeraView", await this.repo.GetCeras());
        }

        public async Task<PartialViewResult> _DetallesCeraView1(Guid IDCera)
        {
            Cera cera = await this.repo.BuscarCera(IDCera);

            ViewData["CERA"] = cera;
            return PartialView("Detalles/_DetallesCeraView1", cera);
        }

    }
}