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
            return PartialView("_CrearClienteView",  new Cliente());
        }

        [HttpPost]
        public async Task<IActionResult> _CrearClienteView(Cliente cli)
        {
             var response = await _clienteRepo.InsertarClienteAsync(cli);

            if (response.Data.IDCliente != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDCliente = response.Data.IDCliente });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDCli)
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
                return View("~/Views/Cliente/_ActClienteView.cshtml", cli.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Cliente cliente)
        {
            var response = await _clienteRepo.ActualizarClienteAsync(cliente.IDCliente, cliente);

            if (response.Data.IDCliente != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDCliente = response.Data.IDCliente });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        public async Task<PartialViewResult> _DetallesClienteView()
        {
            var clientes =  await _clienteRepo.GetClientesAsync();

            ViewData["Clientes"] = clientes.Data;
            return PartialView("~/Views/Cliente/_DetallesClienteView.cshtml", clientes.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDCli)
        {
            var cli =  await _clienteRepo.BuscarClienteAsync(IDCli);

            ViewData["Cliente"] = cli.Data;
            return View("~/Views/Cliente/_DetallesClienteView1.cshtml", cli.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cli = await _clienteRepo.EliminarAsync(id);

            ViewData["Error"] = cli.Error.Mensaje;
            ViewData["OK"] = cli.Data;

            return RedirectToAction("_DetallesClienteView");
        }

        public IActionResult _CrearCosteView()
        {
            //await  await _clienteRepo.InsertarCoste(NombreUs, email,
            //  nickname, password);
            return PartialView("_CrearCosteView");
        }
    }
}