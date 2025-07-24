using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using appVelas.Models;
using appVelas.Repository;

namespace appVelas.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        RepositoryVelas repo;

        public HomeController(RepositoryVelas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ------------------------------------- VELA ---------------------------------------------

        public PartialViewResult _CrearVelaView()
        {
            List<Molde> listaMoldes = this.repo.GetMoldes();
            List<Fragancia> listaFrag = this.repo.GetFragancias();
            List<Pigmento> listaPig = this.repo.GetPigmentos();
            List<Cera> listaCera = this.repo.GetCeras();
            List<Mecha> listaMecha = this.repo.GetMechas();

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            return PartialView("Crear/_CrearVelaView");
        }

        [HttpPost]
        public PartialViewResult _CrearVelaView(Vela vela)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarVela(vela);


            return PartialView("Sucess", vela);

        }

        public PartialViewResult _ActVelaView(Guid IDVela)
        {
            List<Molde> listaMoldes = this.repo.GetMoldes();
            List<Fragancia> listaFrag = this.repo.GetFragancias();
            List<Pigmento> listaPig = this.repo.GetPigmentos();
            List<Cera> listaCera = this.repo.GetCeras();
            List<Mecha> listaMecha = this.repo.GetMechas();

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            Vela vela = this.repo.BuscarVela(IDVela);
            ViewData["IDVela"] = IDVela;

            return PartialView("Actualizar/_ActVelaView", vela);
        }

        [HttpPost]
        public PartialViewResult _ActVelaView(Vela vela)
        {
            this.repo.Actualizarvela(vela);

            return PartialView("Sucess");
        }

        public PartialViewResult _DetallesVelaView()
        {
            List<Vela> velas = this.repo.GetVelas();

            List<Molde> listaMoldes = this.repo.GetMoldes();
            List<Fragancia> listaFrag = this.repo.GetFragancias();
            List<Pigmento> listaPig = this.repo.GetPigmentos();
            List<Cera> listaCera = this.repo.GetCeras();
            List<Mecha> listaMecha = this.repo.GetMechas();

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesVelaView", velas);
        }

        public PartialViewResult _DetallesVelaView1(Guid IDVela)
        {
            Vela vela = this.repo.BuscarVela(IDVela);
            Molde Moldes = this.repo.BuscarMolde(vela.IDMolde);
            Fragancia Frag = this.repo.BuscarFragancia(vela.IDFrag);
            Pigmento Pig = this.repo.BuscarPigmento(vela.IDPig);
            Cera Cera = this.repo.BuscarCera(vela.IDCera);
            Mecha Mecha = this.repo.BuscarMecha(vela.IDMecha);
            Pedido pedi = this.repo.BuscarPedido(vela.IDPedido);
            Guid cli = pedi.IDCliente;
            string clien = this.repo.BuscarCliente(cli).Nombre;

            ViewData["Moldes"] = Moldes;
            ViewData["Frag"] = Frag;
            ViewData["Pig"] = Pig;
            ViewData["Cera"] = Cera;
            ViewData["Mecha"] = Mecha;
            ViewData["VELA"] = vela;
            ViewData["clien"] = clien;
            ViewData["pedi"] = pedi;

            return PartialView("Detalles/_DetallesVelaView1", vela);
        }
        // ------------------------------------- MOLDE ---------------------------------------------

        public PartialViewResult _CrearMoldeView()
        {
            return PartialView("Crear/_CrearMoldeView", new Molde());
        }

        [HttpPost]
        public PartialViewResult _CrearMoldeView(Molde molde)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarMolde(molde);


            return PartialView("Sucess", molde);

        }

        public PartialViewResult _ActMoldeView(Guid IDMolde)
        {
            ViewData["IDMolde"] = IDMolde;
            return PartialView("Actualizar/_CrearMoldeView", this.repo.BuscarMolde(IDMolde));
        }

        [HttpPost]
        public PartialViewResult _ActMoldeView(Molde molde)
        {
            this.repo.ActualizarMolde(molde);

            return PartialView("Sucess", molde);
        }

        public PartialViewResult _DetallesMoldeView()
        {
            List<Molde> moldes = this.repo.GetMoldes();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesMoldeView", moldes);
        }

        public PartialViewResult _DetallesMoldeView1(Guid IDMolde)
        {
            Molde mol = this.repo.BuscarMolde(IDMolde);

            ViewData["MOLDE"] = mol;
            return PartialView("Detalles/_DetallesMoldeView1", mol);
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
            List<Cliente> listaClientes = this.repo.GetClientes();

            ViewData["clientes"] = listaClientes;
            ViewData["IDPedido"] = IDPedido; 
            return PartialView("Actualizar/_ActPedidoView", this.repo.BuscarPedido(IDPedido));
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
            ViewData["IDCli"] = IDCli;
            return PartialView("Actualizar/_ActClienteView", this.repo.BuscarCliente(IDCli));
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

        // ------------------------------------- FRAGANCIA ---------------------------------------------

        public IActionResult _CrearFragView()
        {
            return PartialView("Crear/_CrearFragView", new Fragancia());
        }

        [HttpPost]
        public IActionResult _CrearFragView(Fragancia frag)
        {
            this.repo.InsertarFragancia(frag);
            return PartialView("Sucess", frag);
        }

        public PartialViewResult _ActFragView(Guid IDFrag)
        {
            ViewData["IDFrag"] = IDFrag;
            return PartialView("Actualizar/_ActFragView", this.repo.BuscarFragancia(IDFrag));
        }

        [HttpPost]
        public PartialViewResult _ActFragView(Fragancia frag)
        {
            this.repo.ActualizarFragancia(frag);

            return PartialView("Sucess", frag);
        }

        public PartialViewResult _DetallesFragView()
        {
            List<Fragancia> frag = this.repo.GetFragancias();

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView", frag);
        }

        public PartialViewResult _DetallesFragView1(Guid IDFrag)
        {
            Fragancia frag = this.repo.BuscarFragancia(IDFrag);

            ViewData["FRAGS"] = frag;
            return PartialView("Detalles/_DetallesFragView1", frag);
        }

        // ------------------------------------- MECHA ---------------------------------------------

        public IActionResult _CrearMechaView()
        {
            return PartialView("Crear/_CrearMechaView");
        }

        [HttpPost]
        public IActionResult _CrearMechaView(Mecha mecha)
        {
            this.repo.InsertarMecha(mecha);
            return PartialView("Sucess");
        }

        public PartialViewResult _ActMechaView(Guid IDMecha)
        {
            ViewData["IDMecha"] = IDMecha;
            return PartialView("Actualizar/_ActMechaView", this.repo.BuscarMecha(IDMecha));
        }

        [HttpPost]
        public PartialViewResult _ActMechaView(Mecha mecha)
        {
            this.repo.ActualizarMecha(mecha);

            return PartialView("Sucess", mecha);
        }

        public PartialViewResult _DetallesMechaView()
        {
            List<Mecha> mechas = this.repo.GetMechas();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesMechaView", mechas);
        }

        public PartialViewResult _DetallesMechaView1(Guid IDMecha)
        {
            Mecha me = this.repo.BuscarMecha(IDMecha);

            ViewData["MECHA"] = me;
            return PartialView("Detalles/_DetallesMechaView1", me);
        }

        // ------------------------------------- PIGMENTO ---------------------------------------------

        public IActionResult _CrearPigView()
        {
            return PartialView("Crear/_CrearPigView");
        }

        [HttpPost]
        public IActionResult _CrearPigView(Pigmento pig)
        {
            this.repo.InsertarPigmento(pig);

            return PartialView("Sucess", pig);
        }


        public PartialViewResult _ActPigView(Guid IDPig)
        {
            ViewData["IDFrag"] = IDPig; 
            return PartialView("Actualizar/_ActPigView", this.repo.BuscarPigmento(IDPig));
        }

        [HttpPost]
        public PartialViewResult _ActPigView(Pigmento pig)
        {
            this.repo.ActualizarPigmento(pig);

            return PartialView("Sucess", pig);
        }

        public PartialViewResult _DetallesPigView()
        {
            List<Pigmento> pig = this.repo.GetPigmentos();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesPigView", pig);
        }

        public PartialViewResult _DetallesPigView1(Guid IDPig)
        {
            Pigmento pig = this.repo.BuscarPigmento(IDPig);

            ViewData["PIG"] = pig;
            return PartialView("Detalles/_DetallesPigView1", pig);
        }

        // ------------------------------------- CERA ---------------------------------------------

        public PartialViewResult _CrearCeraView()
        {
            return PartialView("Crear/_CrearCeraView");
        }

        [HttpPost]
        public PartialViewResult _CrearCeraView(Cera cera)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarCera(cera);


            return PartialView("Sucess", cera);

        }

        public PartialViewResult _ActCeraView(Guid IDCera)
        {
            ViewData["IDCera"] = IDCera;
            return PartialView("Actualizar/_ActCeraView", this.repo.BuscarCera(IDCera));
        }

        [HttpPost]
        public PartialViewResult _ActCeraView(Cera cera)
        {
            this.repo.ActualizarCera(cera);

            return PartialView("Sucess", cera);
        }

        public PartialViewResult _DetallesCeraView()
        {
            List<Cera> ceras = this.repo.GetCeras();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesCeraView", ceras);
        }

        public PartialViewResult _DetallesCeraView1(Guid IDCera)
        {
            Cera cera = this.repo.BuscarCera(IDCera);

            ViewData["CERA"] = cera;
            return PartialView("Detalles/_DetallesCeraView1", cera);
        }

        // ------------------------------------- ENDURECEDOR ---------------------------------------------

        public PartialViewResult _CrearEndurecedorView()
        {
            return PartialView("Crear/_CrearEndurecedoriew");
        }

        [HttpPost]
        public PartialViewResult _CrearEndurecedorView(Endurecedor end)
        {
            //if (!ModelState.IsValid)
            //{
            //}
            this.repo.InsertarEndurecedor(end);


            return PartialView("Sucess", end);

        }

        public PartialViewResult _ActEndurecedorView(Guid IDEnd)
        {
            ViewData["IDEnd"] = IDEnd;
            return PartialView("Actualizar/_ActEndurecedorView", this.repo.BuscarEndurecedor(IDEnd));
        }

        [HttpPost]
        public PartialViewResult _ActEndurecedorView(Endurecedor end)
        {
            this.repo.ActualizarEndurecedor(end);

            return PartialView("Sucess", end);
        }

        public PartialViewResult _DetallesEndurecedorView()
        {
            List<Endurecedor> end = this.repo.GetEndurecedor();

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesEndurecedorView", end);
        }

        public PartialViewResult _DetallesEndurecedorView1(Guid IDEnd)
        {
            Endurecedor end = this.repo.BuscarEndurecedor(IDEnd);

            ViewData["END"] = end;
            return PartialView("Detalles/_DetallesEndurecedorView1", end);
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
            ViewData["IDPack"] = IDPack;
            return PartialView("Actualizar/_ActPackView", this.repo.BuscarPack(IDPack));
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

        // ----------------------------------------------------------------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
