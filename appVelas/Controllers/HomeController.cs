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


namespace appVelas.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositoryVelas _velaRepo;
        private readonly RepositoryMoldes _moldeRepo;
        private readonly RepositoryEndurecedores _endepo;
        private readonly RepositoryFragancias _fragRepo;
        private readonly RepositoryPigmentos _pigRepo;
        private readonly RepositoryCeras _ceraRepo;
        private readonly RepositoryMechas _mechaRepo;
        private readonly RepositoryVelaFragancias _vFragRepo;
        private readonly RepositoryVelaPigmentos _vPigRepo;
        private readonly RepositoryPedidos _pediRepo;
        private readonly RepositoryClientes _cliRepo;
        private readonly RepositoryPacks _packRepo;


        public HomeController(RepositoryVelas velaRepo, RepositoryMoldes moldeRepo, RepositoryFragancias fragRepo,
            RepositoryPigmentos pigRepo, RepositoryCeras ceraRepo, RepositoryMechas mechaRepo, RepositoryVelaFragancias velaFragRepo,
            RepositoryVelaPigmentos velaPigRepo, RepositoryPedidos pediRepo, RepositoryClientes cliRepo, RepositoryPacks packRepo,
            RepositoryEndurecedores endepo)
        {
            _velaRepo = velaRepo;
            _moldeRepo = moldeRepo;
            _fragRepo = fragRepo;
            _pigRepo = pigRepo;
            _ceraRepo = ceraRepo;
            _mechaRepo = mechaRepo;
            _vFragRepo = velaFragRepo;
            _vPigRepo = velaPigRepo;
            _pediRepo = pediRepo;
            _cliRepo = cliRepo;
            _packRepo = packRepo;
            _endepo = endepo;
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

            //var metodo = this.repo.GetType().GetMethod(buscarModelo);

            //var model = metodo.Invoke(this.repo, new object[] { id });

            //// Arma el nombre del parcial de forma dinámica
            string vistaParcial = $"~/Views/Shared/Actualizar/_Act{vista}View.cshtml";

            // Retorna la vista principal contenedora
            return View("ActualizarView", (vistaParcial, (object)buscarModelo));
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

            //var metodo = this.repo.GetType().GetMethod(buscarModelo);

            //var model = metodo.Invoke(this.repo, new object[] { id });

            // Arma el nombre del parcial de forma dinámica
            string vistaParcial = $"~/Views/{vista}/_Detalles{vista}View1.cshtml";

            // Retorna la vista principal contenedora
            return View("DetallesView1", (vistaParcial, (object)buscarModelo));
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
