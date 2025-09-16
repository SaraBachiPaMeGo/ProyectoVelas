using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;

namespace appVelas.Controllers
{
    public class VelaController : Controller
    {
        private readonly RepositoryVelas repo;

        public VelaController(RepositoryVelas repo)
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
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna vela con el IDVela recibido. IDVELA = " + IDVela +
                        "Error en el Controller de la vista _ActVelaView"
                });
            }
            else
            {
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
    }
}