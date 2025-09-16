using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class PackController : Controller
    {
        private readonly RepositoryPacks repo;

        public PackController(RepositoryPacks repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- PACK ---------------------------------------------

        public PartialViewResult _CrearPackView()
        {
            return PartialView("Crear/_CrearPackView");
        }

        [HttpPost]
        public PartialViewResult _CrearPackView(Pack pack)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarPack(pack);


            return PartialView("Sucess", pack);

        }

        public PartialViewResult _ActPackView(Guid IDPack)
        {
            Pack pack = this.repo.BuscarPack(IDPack);

            if (pack == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pack con el IDPack recibido. IDPack = " + IDPack +
                        "Error en el Controller de la vista _ActPackView"
                });
            }
            else
            {
                ViewData["IDPack"] = IDPack;
            }
            return PartialView("Actualizar/_ActPackView", pack);
        }

        [HttpPost]
        public PartialViewResult _ActPackView(Pack pack)
        {
            this.repo.ActualizarPack(pack);

            return PartialView("Sucess", pack);
        }

        public PartialViewResult _DetallesPackView()
        {
            List<Pack> pack = this.repo.GetPacks();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesPackView", pack);
        }

        public PartialViewResult _DetallesPackView1(Guid IDPack)
        {
            Pack pack = this.repo.BuscarPack(IDPack);

            ViewData["PACK"] = pack;
            return PartialView("Detalles/_DetallesPackView1", pack);
        }

    }
}