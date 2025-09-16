using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class PigmentoController : Controller
    {
        private readonly RepositoryPigmentos repo;

        public PigmentoController(RepositoryPigmentos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- PIGMENTO ---------------------------------------------

        public IActionResult _CrearPigView()
        {
            return PartialView("Crear/_CrearPigView");
        }

        [HttpPost]
        public IActionResult _CrearPigView(Pigmento pig)
        {
            this.repo.InsertarPigmento(pig);

            return PartialView("Sucess", pig);
        }


        public PartialViewResult _ActPigView(Guid IDPig)
        {
            Pigmento pig = this.repo.BuscarPigmento(IDPig);

            if (pig == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pigmento con el IDPig recibido. IDPig = " + IDPig +
                        "Error en el Controller de la vista _ActPigView"
                });
            }
            else
            {
                ViewData["IDPig"] = IDPig;
                return PartialView("Actualizar/_ActPigView", pig);
            }
        }

        [HttpPost]
        public PartialViewResult _ActPigView(Pigmento pig)
        {
            this.repo.ActualizarPigmento(pig);

            return PartialView("Sucess", pig);
        }

        public PartialViewResult _DetallesPigView()
        {
            List<Pigmento> pig = this.repo.GetPigmentos();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesPigView", pig);
        }

        public PartialViewResult _DetallesPigView1(Guid IDPig)
        {
            Pigmento pig = this.repo.BuscarPigmento(IDPig);

            ViewData["PIG"] = pig;
            return PartialView("Detalles/_DetallesPigView1", pig);
        }

    }
}