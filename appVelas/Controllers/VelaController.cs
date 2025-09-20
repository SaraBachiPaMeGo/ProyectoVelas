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
    public class VelaController : Controller
    {
        private readonly RepositoryVelas _velaRepo;

        public VelaController(RepositoryVelas velaRepo)
        {
            _velaRepo = velaRepo;
        }

        public async Task<IActionResult> Index()
        {
            var velas = await _velaRepo.GetVelasAsync();
            return View(velas);
        }

        // ------------------------------------- VELA ---------------------------------------------

        public async Task<PartialViewResult>  _CrearVelaView()
        {
            List<Molde> listaMoldes = await _velaRepo.getMoldes();
            List<Fragancia> listaFrag = await _velaRepo.GetFragancias();
            List<Pigmento> listaPig = await _velaRepo.GetPigmentos();
            List<Cera> listaCera = await _velaRepo.GetCeras();
            List<Mecha> listaMecha = await _velaRepo.GetMechas();

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
        public async Task<PartialViewResult>  _CrearVelaView(Vela vela, List<Guid> IDFragancias, List<Guid> IDPigmentos)
        {
            // Insertar fragancias
            foreach (var idFrag in IDFragancias)
            {
                await _velaRepo.InsertarVelaFragancia(vela.IDVela, idFrag);
            }

            // Insertar pigmentos
            foreach (var idPig in IDPigmentos)
            {
                await _velaRepo.InsertarVelaPigmento(vela.IDVela, idPig);
            }

            await _velaRepo.InsertarVelaAsync(vela);


            return PartialView("Sucess", vela);

        }

        public async Task<PartialViewResult>  _ActVelaView(Guid IDVela)
        {
            Vela vela = await _velaRepo.BuscarVelaAsync(IDVela);

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
                List<Molde> listaMoldes = await _velaRepo.GetMoldes();
                //List<Fragancia> listaFrag = await _velaRepo.GetFragancias();
                //List<Pigmento> listaPig = await _velaRepo.GetPigmentos();
                List<Cera> listaCera = await _velaRepo.GetCeras();
                List<Mecha> listaMecha = await _velaRepo.GetMechas();

                // Carga todas las fragancias y pigmentos para los selects
                //ViewBag.Fragancias = new SelectList(context.Fragancia.ToList(), "IDFrag", "FragNombre");
                //ViewBag.Pigmentos = new SelectList(context.Pigmento.ToList(), "IDPig", "ColorNombre");

                // Carga las fragancias seleccionadas para esta vela
                //ViewBag.FraganciasSeleccionadas = await _velaRepo.GetFraganciasPorVela(IDVela).Select(f => f.IDFrag).ToList();

                // Carga los pigmentos seleccionados para esta vela
                //ViewBag.PigmentosSeleccionados = await _velaRepo.GetPigmentosPorVela(IDVela).Select(p => p.IDPig).ToList();

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
        public async Task<PartialViewResult>  _ActVelaView(Vela vela)
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

            await _velaRepo.ActualizarVelaAsync(vela);

            return PartialView("Sucess");
        }

        public async Task<PartialViewResult>  _DetallesVelaView()
        {
            List<Vela> velas = await _velaRepo.GetVelasAsync();

            List<Molde> listaMoldes = await _velaRepo.GetMoldes();
            List<Fragancia> listaFrag = await _velaRepo.GetFragancias();
            List<Pigmento> listaPig = await _velaRepo.GetPigmentos();
            List<Cera> listaCera = await _velaRepo.GetCeras();
            List<Mecha> listaMecha = await _velaRepo.GetMechas();

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            //ViewData["VELAS"] = velas;
            return PartialView("Detalles/_DetallesVelaView", velas);
        }

        public async Task<PartialViewResult>  _DetallesVelaView1(Guid IDVela)
        {
            Vela vela = await _velaRepo.BuscarVelaAsync(IDVela);
            Molde Moldes = await _velaRepo.BuscarMolde(vela.IDMolde);
            Fragancia Frag = await _velaRepo.BuscarFragancia(vela.IDFrag);
            Pigmento Pig = await _velaRepo.BuscarPigmento(vela.IDPig);
            Cera Cera = await _velaRepo.BuscarCera(vela.IDCera);
            Mecha Mecha = await _velaRepo.BuscarMecha(vela.IDMecha);
            Pedido pedi = await _velaRepo.BuscarPedido(vela.IDPedido);
            Guid cli = pedi.IDCliente;
            string clien = await _velaRepo.BuscarCliente(cli).Nombre;

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