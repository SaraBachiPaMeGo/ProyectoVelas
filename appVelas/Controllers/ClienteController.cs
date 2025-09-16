using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly RepositoryClientes repo;

        public ClienteController(RepositoryClientes repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- CLIENTE ---------------------------------------------

        public IActionResult _CrearClienteView()
        {
            return PartialView("Crear/_CrearClienteView", new Cliente());
        }

        [HttpPost]
        public IActionResult _CrearClienteView(Cliente cli)
        {
            this.repo.InsertarCliente(cli);
            return PartialView("Sucess", cli);
        }

        public PartialViewResult _ActClienteView(Guid IDCli)
        {
            Cliente cli = this.repo.BuscarCliente(IDCli);

            if (cli == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDCli recibido. IDCli = " + IDCli +
                        "Error en el Controller de la vista _ActClienteView"
                });
            }
            else
            {
                ViewData["IDCli"] = IDCli;
                return PartialView("Actualizar/_ActClienteView", cli);
            }
        }

        [HttpPost]
        public PartialViewResult _ActClienteView(Cliente cliente)
        {
            this.repo.ActualizarCliente(cliente);

            return PartialView("Sucess", cliente);
        }

        public PartialViewResult _DetallesClienteView()
        {
            List<Cliente> clientes = this.repo.GetClientes();

            ViewData["Clientes"] = clientes;
            return PartialView("Detalles/_DetallesClienteView", clientes);
        }

        public PartialViewResult _DetallesClienteView1(Guid IDCli)
        {
            Cliente cli = this.repo.BuscarCliente(IDCli);

            ViewData["Cliente"] = cli;
            return PartialView("Detalles/_DetallesClienteView1", cli);
        }

        public IActionResult _CrearCosteView()
        {
            //await this.repo.InsertarCoste(NombreUs, email,
            //  nickname, password);
            return PartialView("_CrearCosteView");
        }
    }
}