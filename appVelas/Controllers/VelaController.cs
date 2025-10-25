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
        private readonly RepositoryMoldes _moldeRepo;
        private readonly RepositoryFragancias _fragRepo;
        private readonly RepositoryPigmentos _pigRepo;
        private readonly RepositoryCeras _ceraRepo;
        private readonly RepositoryMechas _mechaRepo;
        private readonly RepositoryVelaFragancias _vFragRepo;
        private readonly RepositoryVelaPigmentos _vPigRepo;
        private readonly RepositoryPedidos _pediRepo;
        private readonly RepositoryClientes _cliRepo;

        public VelaController(RepositoryVelas velaRepo, RepositoryMoldes moldeRepo, RepositoryFragancias fragRepo,
            RepositoryPigmentos pigRepo, RepositoryCeras ceraRepo, RepositoryMechas mechaRepo, RepositoryVelaFragancias velaFragRepo,
            RepositoryVelaPigmentos velaPigRepo, RepositoryPedidos pediRepo, RepositoryClientes cliRepo)
        {
            _velaRepo = velaRepo;
            _moldeRepo = moldeRepo;
            _fragRepo = fragRepo;
            _pigRepo = pigRepo;
            _ceraRepo = ceraRepo;
            _mechaRepo = mechaRepo;
            _vFragRepo = velaFragRepo;
            _vPigRepo = velaPigRepo;
            _pediRepo = pediRepo;
            _cliRepo = cliRepo;

        }

        public async Task<IActionResult> Index()
        {
            var velas = await _velaRepo.GetVelasAsync();
            return View(velas);
        }

        // ------------------------------------- VELA ---------------------------------------------

        public async Task<PartialViewResult>  _CrearVelaView()
        {
            var listaMoldes = await _moldeRepo.GetMoldesAsync();
            var listaFrag = await _fragRepo.GetFraganciasAsync();
            var listaPig = await _pigRepo.GetPigmentosAsync();
            var listaCera = await _ceraRepo.GetCerasAsync();
            var listaMecha = await _mechaRepo.GetMechasAsync();

            //ViewBag.Fragancias = new SelectList(this.context.Fragancia.ToList(), "IDFrag", "FragNombre");
            //ViewBag.Pigmentos = new SelectList(this.context.Pigmento.ToList(), "IDPig", "ColorNombre");

            ViewData["Moldes"] = listaMoldes;
            ViewData["Frag"] = listaFrag;
            ViewData["Pig"] = listaPig;
            ViewData["Cera"] = listaCera;
            ViewData["Mecha"] = listaMecha;

            return PartialView("Vela/_CrearVelaView");
        }

        [HttpPost]
        public async Task<PartialViewResult>  _CrearVelaView(Vela vela, List<Guid> IDFragancias, List<Guid> IDPigmentos)
        {
            var velfrag = new CustomApiResponse<VelaFragancia>();
            var velpig = new CustomApiResponse<VelaPigmento>();

            // Insertar fragancias
            foreach (var idFrag in IDFragancias)
            {
                velfrag = await _vFragRepo.BuscarVelaFraganciaAsync(idFrag);

                await _vFragRepo.InsertarVelaFraganciaAsync(velfrag.Data);
            }

            

            // Insertar pigmentos
            foreach (var idPig in IDPigmentos)
            {
                velpig = await _vPigRepo.BuscarVelaPigmentoAsync(idPig);

                await _vPigRepo.InsertarVelaPigmentoAsync(velpig.Data);
            }

            await _velaRepo.InsertarVelaAsync(vela);


            return PartialView("Sucess", vela);

        }

        public async Task<PartialViewResult>  _ActVelaView(Guid IDVela)
        {
            var vela = await _velaRepo.BuscarVelaAsync(IDVela);

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
                var listaMoldes = await _moldeRepo.GetMoldesAsync();
                //List<Fragancia> listaFrag = await _velaRepo.GetFragancias();
                //List<Pigmento> listaPig = await _velaRepo.GetPigmentos();
                var listaCera = await _ceraRepo.GetCerasAsync();
                var listaMecha = await _mechaRepo.GetMechasAsync();

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

                return PartialView("Vela/_ActVelaView", vela);
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
            await _vFragRepo.EliminarRelacionesFraganciaAsync(vela.IDVela);

            if (vela.Fragancias != null)
            {
                foreach (var idFrag in vela.Fragancias)
                {
                    await _vFragRepo.InsertarVelaFraganciaAsync(idFrag); 
                }
            }

            await _vPigRepo.EliminarRelacionesPigmentosAsync(vela.IDVela);

            if (vela.Pigmentos != null)
            {
                foreach (var idPig in vela.Pigmentos)
                {
                    await _vPigRepo.InsertarVelaPigmentoAsync(idPig); 
                }
            }

            await _velaRepo.ActualizarVelaAsync(vela);

            return PartialView("Sucess");
        }

        public async Task<PartialViewResult>  _DetallesVelaView()
        {
            var velas = await _velaRepo.GetVelasAsync();

            var listaMoldes = await _moldeRepo.GetMoldesAsync();
            var listaFrag = await _fragRepo.GetFraganciasAsync();
            var listaPig = await _pigRepo.GetPigmentosAsync();
            var listaCera = await _ceraRepo.GetCerasAsync();
            var listaMecha = await _mechaRepo.GetMechasAsync();

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
            var vela = await _velaRepo.BuscarVelaAsync(IDVela);
            var Moldes = await _moldeRepo.BuscarMoldeAsync(vela.Data.IDMolde);
            var Frag = await _fragRepo.BuscarFraganciaAsync(vela.Data.IDFrag);
            var Pig = await _pigRepo.BuscarPigmentoAsync(vela.Data.IDPig);
            var Cera = await _ceraRepo.BuscarCeraAsync(vela.Data.IDCera);
            var Mecha = await _mechaRepo.BuscarMechaAsync(vela.Data.IDMecha);
            var pedi = await _pediRepo.BuscarPedidoAsync(vela.Data.IDPedido);
            Guid id = pedi.Data.IDCliente;

            CustomApiResponse<Cliente> clien = await _cliRepo.BuscarClienteAsync(id);

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