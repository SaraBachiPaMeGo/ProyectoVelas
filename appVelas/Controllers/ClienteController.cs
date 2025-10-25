using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Diagnostics;

namespace appVelas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly RepositoryClientes _clienteRepo;

        public ClienteController(RepositoryClientes clienteService)
        {
            _clienteRepo = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var Clientes = await _clienteRepo.GetClientesAsync();
            return View(Clientes);
        }

        // ------------------------------------- CLIENTE ---------------------------------------------

        public async Task<IActionResult> _CrearClienteView()
        {
            return PartialView("Cliente/_CrearClienteView",  new Cliente());
        }

        [HttpPost]
        public async Task<IActionResult> _CrearClienteView(Cliente cli)
        {
             await _clienteRepo.InsertarClienteAsync(cli);

            return PartialView("Sucess", cli);
        }

        public async Task<PartialViewResult> _ActClienteView(Guid IDCli)
        {
            var cli =  await _clienteRepo.BuscarClienteAsync(IDCli);

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
                return PartialView("Cliente/_ActClienteView", cli);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> _ActClienteView(Cliente cliente)
        {
             await _clienteRepo.ActualizarClienteAsync(cliente);

            return PartialView("Sucess", cliente);
        }

        public async Task<PartialViewResult> _DetallesClienteView()
        {
            var clientes =  await _clienteRepo.GetClientesAsync();

            ViewData["Clientes"] = clientes;
            return PartialView("Detalles/_DetallesClienteView", clientes);
        }

        public async Task<PartialViewResult> _DetallesClienteView1(Guid IDCli)
        {
            var cli =  await _clienteRepo.BuscarClienteAsync(IDCli);

            ViewData["Cliente"] = cli;
            return PartialView("Detalles/_DetallesClienteView1", cli);
        }

        public IActionResult _CrearCosteView()
        {
            //await  await _clienteRepo.InsertarCoste(NombreUs, email,
            //  nickname, password);
            return PartialView("_CrearCosteView");
        }
    }
}