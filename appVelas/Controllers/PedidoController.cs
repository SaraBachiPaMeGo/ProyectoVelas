using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class PedidoController : Controller
    {
        private readonly RepositoryPedidos repo;
   
        public PedidoController(RepositoryPedidos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- PEDIDO ---------------------------------------------

        public PartialViewResult _CrearPedidoView()
        {
            List<Cliente> listaCliente = this.repo.GetClientes();

            ViewData["Cliente"] = listaCliente;

            return PartialView("Crear/_CrearPedidoView", new Pedido());
        }

        [HttpPost]
        public PartialViewResult _CrearPedidoView(Guid idCliente, Guid iDVela)
        {
            this.repo.InsertarPedido(idCliente, iDVela);
            return PartialView("Sucess");
        }

        public PartialViewResult _ActPedidoView(Guid IDPedido)
        {
            Pedido ped = this.repo.BuscarPedido(IDPedido);

            if (ped == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDPedido recibido. IDPedido = " + IDPedido +
                        "Error en el Controller de la vista _ActPedidoView"
                });
            }
            else
            {

                List<Cliente> listaClientes = this.repo.GetClientes();

                ViewData["clientes"] = listaClientes;
                ViewData["IDPedido"] = IDPedido;
                return PartialView("Actualizar/_ActPedidoView", ped);
            }
        }

        [HttpPost]
        public PartialViewResult _ActPedidoView(Pedido pedido)
        {
            //this.repo.ActualizarPedido(pedido);

            return PartialView("Actualizar/_ActPedidoView", pedido);
        }

        public PartialViewResult _DetallesPedidoView()
        {
            List<Pedido> pedidos = this.repo.GetPedidos();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesPedidoView", pedidos);
        }

        public PartialViewResult _DetallesPedidoView1(Guid IDPedido)
        {
            Pedido pedo = this.repo.BuscarPedido(IDPedido);

            ViewData["PEDIDO"] = pedo;
            return PartialView("Detalles/_DetallesPedidoView1", pedo);
        }
    }
}