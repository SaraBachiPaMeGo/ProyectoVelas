using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;

namespace appVelas.Controllers
{
    public class EndurecedorController : Controller
    {
        private readonly RepositoryEndurecedores repo;

        public EndurecedorController(RepositoryEndurecedores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- ENDURECEDOR ---------------------------------------------

        public PartialViewResult _CrearEndurecedorView()
        {
            return PartialView("Crear/_CrearEndurecedorView");
        }

        [HttpPost]
        public PartialViewResult _CrearEndurecedorView(Endurecedor end)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarEndurecedor(end);


            return PartialView("Sucess", end);

        }

        public PartialViewResult _ActEndurecedorView(Guid IDEnd)
        {
            Endurecedor end = this.repo.BuscarEndurecedor(IDEnd);

            if (end == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna endurecedor con el IDEnd recibido. IDEnd = " + IDEnd +
                        "Error en el Controller de la vista _ActEndurecedorView"
                });
            }
            else
            {
                ViewData["IDEnd"] = IDEnd;
            }
            return PartialView("Actualizar/_ActEndurecedorView", end);
        }

        [HttpPost]
        public PartialViewResult _ActEndurecedorView(Endurecedor end)
        {
            this.repo.ActualizarEndurecedor(end);

            return PartialView("Sucess", end);
        }

        public PartialViewResult _DetallesEndurecedorView()
        {
            List<Endurecedor> end = this.repo.GetEndurecedor();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesEndurecedorView", end);
        }

        public PartialViewResult _DetallesEndurecedorView1(Guid IDEnd)
        {
            Endurecedor end = this.repo.BuscarEndurecedor(IDEnd);

            ViewData["END"] = end;
            return PartialView("Detalles/_DetallesEndurecedorView1", end);
        }

    }
}