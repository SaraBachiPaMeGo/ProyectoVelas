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
    public class PedidoController : Controller
    {
        private readonly RepositoryPedidos _pedidoRepo;
        private readonly RepositoryClientes _cliRepo;

        public PedidoController(RepositoryPedidos pedidoService, RepositoryClientes cliService)
        {
            _pedidoRepo = pedidoService;
            _cliRepo = cliService;
        }

        public async Task<IActionResult> Index()
        {
            var Pedidos = await _pedidoRepo.GetPedidosAsync();
            return View(Pedidos);
        }

        // ------------------------------------- PEDIDO ---------------------------------------------

        public async Task<PartialViewResult>  _CrearPedidoView()
        {
            var clientes = await _cliRepo.GetClientesAsync();

            ViewData["Clientes"] = clientes.Data;
            return PartialView("_CrearPedidoView");
        }

        [HttpPost]
        public async Task<IActionResult>  _CrearPedidoView(Pedido pedi)
        {
            var response = await _pedidoRepo.InsertarPedidoAsync(pedi);

            if (response.Data.IDPedido != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPedido = response.Data.IDPedido });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDPedido)
        {
            var ped = await _pedidoRepo.BuscarPedidoAsync(IDPedido);

            if (ped == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna Pedido con el IDPedido recibido. IDPedido = " + IDPedido +
                        "Error en el Controller de la vista _ActPedidoView"
                });
            }
            else
            {

                var listaPedidos = await _pedidoRepo.GetPedidosAsync();

                ViewData["Pedidos"] = listaPedidos.Data;
                ViewData["IDPedido"] = IDPedido;
                return View("~/Views/Pedido/_ActPedidoView.cshtml", ped.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Guid id, Pedido pedido)
        {
            var response = await _pedidoRepo.ActualizarPedidoAsync(id, pedido);


            if (response.Data.IDPedido != Guid.Empty)
            {
                return RedirectToAction("DetallesView1", new { IDPedido = response.Data.IDPedido });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]

        public async Task<IActionResult>  _DetallesPedidoView()
        {
            try
            {
                var pedidos = await _pedidoRepo.GetPedidosAsync();

                ViewData["PedidoS"] = pedidos.Data;
                return PartialView("~/Views/Pedido/_DetallesPedidoView.cshtml", pedidos.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpGet]

        public async Task<IActionResult> DetallesView1(Guid IDPedido)
        {
            var pedo = await _pedidoRepo.BuscarPedidoAsync(IDPedido);

            ViewData["PEDIDO"] = pedo.Data;
            return View("~/Views/Pedido/_DetallesPedidoView1.cshtml", pedo.Data);
        }
    }
}