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
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly RepositoryVelas repo;
        //Contexto context;

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

        // ------------------------------------- VELA ---------------------------------------------

        public PartialViewResult _CrearVelaView()
        {
            List<Molde> listaMoldes = this.repo.GetMoldes();
            List<Fragancia> listaFrag = this.repo.GetFragancias();
            List<Pigmento> listaPig = this.repo.GetPigmentos();
            List<Cera> listaCera = this.repo.GetCeras();
            List<Mecha> listaMecha = this.repo.GetMechas();

            //ViewBag.Fragancias = new SelectList(this.context.Fragancia.ToList(), "IDFrag", "FragNombre");
            //ViewBag.Pigmentos = new SelectList(this.context.Pigmento.ToList(), "IDPig", "ColorNombre");

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            return PartialView("Crear/_CrearVelaView");
        }

        [HttpPost]
        public PartialViewResult _CrearVelaView(Vela vela, List<Guid> IDFragancias, List<Guid> IDPigmentos)
        {
            // Insertar fragancias
            foreach (var idFrag in IDFragancias)
            {
                this.repo.InsertarVelaFragancia(vela.IDVela, idFrag);
            }

            // Insertar pigmentos
            foreach (var idPig in IDPigmentos)
            {
                this.repo.InsertarVelaPigmento(vela.IDVela, idPig);
            }

            this.repo.InsertarVela(vela);


            return PartialView("Sucess", vela);

        }

        public PartialViewResult _ActVelaView(Guid IDVela)
        {
            Vela vela = this.repo.BuscarVela(IDVela);

            if (vela == null)
            {
                return PartialView("Error", new
                    ErrorViewModel {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        Mensaje = "No se encontró ninguna vela con el IDVela recibido. IDVELA = " + IDVela +
                        "Error en el Controller de la vista _ActVelaView"
                });
            }
            else {
                List<Molde> listaMoldes = this.repo.GetMoldes();
                //List<Fragancia> listaFrag = this.repo.GetFragancias();
                //List<Pigmento> listaPig = this.repo.GetPigmentos();
                List<Cera> listaCera = this.repo.GetCeras();
                List<Mecha> listaMecha = this.repo.GetMechas();

                // Carga todas las fragancias y pigmentos para los selects
                //ViewBag.Fragancias = new SelectList(context.Fragancia.ToList(), "IDFrag", "FragNombre");
                //ViewBag.Pigmentos = new SelectList(context.Pigmento.ToList(), "IDPig", "ColorNombre");

                // Carga las fragancias seleccionadas para esta vela
                //ViewBag.FraganciasSeleccionadas = this.repo.GetFraganciasPorVela(IDVela).Select(f => f.IDFrag).ToList();

                // Carga los pigmentos seleccionados para esta vela
                //ViewBag.PigmentosSeleccionados = this.repo.GetPigmentosPorVela(IDVela).Select(p => p.IDPig).ToList();

                ViewData["Moldes"] = listaMoldes;
                //ViewData["Frag"] = listaFrag;
                //ViewData["Pig"] = listaPig;
                ViewData["Cera"] = listaCera;
                ViewData["Mecha"] = listaMecha;

                ViewData["IDVela"] = IDVela;

                return PartialView("Actualizar/_ActVelaView", vela);
            }
            
            
        }

        [HttpPost]
        public PartialViewResult _ActVelaView(Vela vela)
        {
            if (vela == null)
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDVela recibido. IDVELA = " + vela.IDVela +
                        "Error en el Controller de la vista _ActVelaView"
                });

            // Elimina todas las relaciones actuales y vuelve a insertar las seleccionadas
            repo.EliminarRelacionesFragancias(vela.IDVela);
            if (vela.Fragancias != null)
            {
                foreach (var idFrag in vela.Fragancias)
                {
                    repo.InsertarVelaFragancia(vela.IDVela, idFrag.IDFrag); // IDFragancia es el GUID dentro del objeto
                }
            }

            repo.EliminarRelacionesPigmentos(vela.IDVela);

            if (vela.Pigmentos != null)
            {
                foreach (var idPig in vela.Pigmentos)
                {
                    repo.InsertarVelaPigmento(vela.IDVela, idPig.IDPig); // o como se llame la propiedad del GUID dentro del objeto
                }
            }

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
            Molde mol = this.repo.BuscarMolde(IDMolde);

            if (mol == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDMolde recibido. IDMolde = " + IDMolde +
                        "Error en el Controller de la vista _ActMoldeView"
                });
            }
            else
            {
                ViewData["IDMolde"] = IDMolde;
                return PartialView("Actualizar/_CrearMoldeView", this.repo.BuscarMolde(IDMolde));
            }
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
            else{

                List<Cliente> listaClientes = this.repo.GetClientes();

                ViewData["clientes"] = listaClientes;
                ViewData["IDPedido"] = IDPedido;
                return PartialView("Actualizar/_ActPedidoView",ped);
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
            Fragancia frag = this.repo.BuscarFragancia(IDFrag);

            if (frag == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna fragancua con el IDFrag recibido. IDFrag = " + IDFrag +
                        "Error en el Controller de la vista _ActFragView"
                });
            }
            else
            {
                ViewData["IDFrag"] = IDFrag;
                return PartialView("Actualizar/_ActFragView", frag);
            }
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
            Mecha mecha = this.repo.BuscarMecha(IDMecha);

            if (mecha == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna mecha con el IDMecha recibido. IDMecha = " + IDMecha +
                        "Error en el Controller de la vista _ActMechaView"
                });
            }
            else
            {
                ViewData["IDMecha"] = IDMecha;
                return PartialView("Actualizar/_ActMechaView", mecha);
            }
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
            Pigmento pig = this.repo.BuscarPigmento(IDPig);

            if (pig == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pigmento con el IDPig recibido. IDPig = " + IDPig +
                        "Error en el Controller de la vista _ActPigView"
                });
            }
            else
            {
                ViewData["IDPig"] = IDPig;
                return PartialView("Actualizar/_ActPigView", pig);
            }
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
            Cera cera = this.repo.BuscarCera(IDCera);
            if (cera == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna cera con el IDCera recibido. IDCera = " + IDCera +
                        "Error en el Controller de la vista _ActCeraView"
                });
            }
            else
                ViewData["IDCera"] = IDCera;
            return PartialView("Actualizar/_ActCeraView", cera);
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
            return PartialView("Crear/_CrearEndurecedorView");
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
            Endurecedor end = this.repo.BuscarEndurecedor(IDEnd);

            if (end == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna endurecedor con el IDEnd recibido. IDEnd = " + IDEnd +
                        "Error en el Controller de la vista _ActEndurecedorView"
                });
            }
            else
            {
                ViewData["IDEnd"] = IDEnd;
            }
            return PartialView("Actualizar/_ActEndurecedorView", end);
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
            Pack pack = this.repo.BuscarPack(IDPack);

            if (pack == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna pack con el IDPack recibido. IDPack = " + IDPack +
                        "Error en el Controller de la vista _ActPackView"
                });
            }
            else
            {
                ViewData["IDPack"] = IDPack;
            }
            return PartialView("Actualizar/_ActPackView", pack);
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
