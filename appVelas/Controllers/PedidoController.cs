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

            return PartialView("Pedido/_CrearPedidoView", new Pedido());
        }

        [HttpPost]
        public async Task<PartialViewResult>  _CrearPedidoView(Pedido pedi)
        {
            await _pedidoRepo.InsertarPedidoAsync(pedi);
            return PartialView("Sucess");
        }

        public async Task<PartialViewResult>  _ActPedidoView(Guid IDPedido)
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
                return PartialView("Pedido/_ActPedidoView", ped);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult>  _ActPedidoView(Pedido pedido)
        {
            //await _pedidoRepo.ActualizarPedido(pedido);

            return PartialView("Pedido/_ActPedidoView", pedido);
        }

        public async Task<PartialViewResult>  _DetallesPedidoView()
        {
            var pedidos = await _pedidoRepo.GetPedidosAsync();

            //ViewData["PedidoS"] = Pedidos;
            return PartialView("Detalles/_DetallesPedidoView", pedidos);
        }

        public async Task<PartialViewResult>  _DetallesPedidoView1(Guid IDPedido)
        {
            var pedo = await _pedidoRepo.BuscarPedidoAsync(IDPedido);

            ViewData["PEDIDO"] = pedo;
            return PartialView("Detalles/_DetallesPedidoView1", pedo);
        }
    }
}