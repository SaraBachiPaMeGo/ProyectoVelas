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

        public PedidoController(RepositoryPedidos pedidoService)
        {
            _pedidoRepo = pedidoService;
        }

        public async Task<IActionResult> Index()
        {
            var Pedidos = await _pedidoRepo.GetPedidosAsync();
            return View(Pedidos);
        }

        // ------------------------------------- PEDIDO ---------------------------------------------

        public async Task<PartialViewResult>  _CrearPedidoView()
        {
            var listaPedido = await _pedidoRepo.GetPedidosAsync();

            ViewData["Pedido"] = listaPedido;

            return PartialView("_CrearPedidoView", new Pedido());
        }

        [HttpPost]
        public async Task<PartialViewResult>  _CrearPedidoView(Pedido pedi)
        {
            await _pedidoRepo.InsertarPedidoAsync(pedi);
            return PartialView("Sucess");
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

                ViewData["Pedidos"] = listaPedidos;
                ViewData["IDPedido"] = IDPedido;
                return View("~/Views/Pedido/_ActPedidoView", ped.Data);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> ActualizarView(Guid id, Pedido pedido)
        {
            var pedi = await _pedidoRepo.ActualizarPedidoAsync(id, pedido);

            return PartialView("Sucess", pedi);
        }

        [HttpGet]

        public async Task<IActionResult>  _DetallesPedidoView()
        {
            try
            {
                var pedidos = await _pedidoRepo.GetPedidosAsync();

                ViewData["PedidoS"] = pedidos;
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

            ViewData["PEDIDO"] = pedo;
            return View("~/Views/Pedido/_DetallesPedidoView1.cshtml", pedo.Data);
        }
    }
}