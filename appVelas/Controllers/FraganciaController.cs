using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class FraganciaController : Controller
    {
        private readonly RepositoryFragancias repo;

        public FraganciaController(RepositoryFragancias repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- FRAGANCIA ---------------------------------------------

        public IActionResult _CrearFragView()
        {
            return PartialView("Crear/_CrearFragView", new Fragancia());
        }

        [HttpPost]
        public IActionResult _CrearFragView(Fragancia frag)
        {
            this.repo.InsertarFragancia(frag);
            return PartialView("Sucess", frag);
        }

        public PartialViewResult _ActFragView(Guid IDFrag)
        {
            Fragancia frag = this.repo.BuscarFragancia(IDFrag);

            if (frag == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna fragancua con el IDFrag recibido. IDFrag = " + IDFrag +
                        "Error en el Controller de la vista _ActFragView"
                });
            }
            else
            {
                ViewData["IDFrag"] = IDFrag;
                return PartialView("Actualizar/_ActFragView", frag);
            }
        }

        [HttpPost]
        public PartialViewResult _ActFragView(Fragancia frag)
        {
            this.repo.ActualizarFragancia(frag);

            return PartialView("Sucess", frag);
        }

        public PartialViewResult _DetallesFragView()
        {
            List<Fragancia> frag = this.repo.GetFragancias();

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView", frag);
        }

        public PartialViewResult _DetallesFragView1(Guid IDFrag)
        {
            Fragancia frag = this.repo.BuscarFragancia(IDFrag);

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView1", frag);
        }
    }
}