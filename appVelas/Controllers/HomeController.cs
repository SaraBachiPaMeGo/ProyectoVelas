using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using appVelas.Models;
using appVelas.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using appVelas.Data;

namespace appVelas.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositoryVelas repo;

        public HomeController(RepositoryVelas repo )
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ActualizarView(Guid id, string vista)
        {
            ViewData["id"] = id;
            ViewData["vista"] = vista;
            string buscarModelo = "";

            if (vista == "Frag")
            {
                buscarModelo = $"BuscarFragancia";
            }
            else if (vista == "Pig")
            {

                buscarModelo = $"BuscarPigmento";
            }
            else { 
                buscarModelo = $"Buscar{vista}";
            }

            var metodo = this.repo.GetType().GetMethod(buscarModelo);

            var model = metodo.Invoke(this.repo, new object[] { id });

            // Arma el nombre del parcial de forma dinámica
            string vistaParcial = $"~/Views/Shared/Actualizar/_Act{vista}View.cshtml";

            // Retorna la vista principal contenedora
            return View("ActualizarView", (vistaParcial, model));
        }

        public IActionResult DetallesView1(Guid id, string vista)
        {
            ViewData["id"] = id;
            ViewData["vista"] = vista;
            string buscarModelo = "";

            if (vista == "Frag")
            {
                buscarModelo = $"BuscarFragancia";
            }
            else if (vista == "Pig")
            {

                buscarModelo = $"BuscarPigmento";
            }
            else
            {
                buscarModelo = $"Buscar{vista}";
            }

            var metodo = this.repo.GetType().GetMethod(buscarModelo);

            var model = metodo.Invoke(this.repo, new object[] { id });

            // Arma el nombre del parcial de forma dinámica
            string vistaParcial = $"~/Views/Shared/Detalles/_Detalles{vista}View1.cshtml";

            // Retorna la vista principal contenedora
            return View("DetallesView1", (vistaParcial, model));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string mensaje)
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Mensaje = mensaje
            });
        }
    }
}
