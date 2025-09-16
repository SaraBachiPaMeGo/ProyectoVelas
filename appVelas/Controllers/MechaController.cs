using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class MechaController : Controller
    {
        private readonly RepositoryMechas repo;

        public MechaController(RepositoryMechas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- MECHA ---------------------------------------------

        public IActionResult _CrearMechaView()
        {
            return PartialView("Crear/_CrearMechaView");
        }

        [HttpPost]
        public IActionResult _CrearMechaView(Mecha mecha)
        {
            this.repo.InsertarMecha(mecha);
            return PartialView("Sucess");
        }

        public PartialViewResult _ActMechaView(Guid IDMecha)
        {
            Mecha mecha = this.repo.BuscarMecha(IDMecha);

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
        public PartialViewResult _ActMechaView(Mecha mecha)
        {
            this.repo.ActualizarMecha(mecha);

            return PartialView("Sucess", mecha);
        }

        public PartialViewResult _DetallesMechaView()
        {
            List<Mecha> mechas = this.repo.GetMechas();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesMechaView", mechas);
        }

        public PartialViewResult _DetallesMechaView1(Guid IDMecha)
        {
            Mecha me = this.repo.BuscarMecha(IDMecha);

            ViewData["MECHA"] = me;
            return PartialView("Detalles/_DetallesMechaView1", me);
        }
    }
}