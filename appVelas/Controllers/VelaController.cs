using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appVelas.Repository;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;
using appVelas.Service;

namespace appVelas.Controllers
{
    public class VelaController : Controller
    {
        private readonly RepositoryVelas _velaRepo;
        private readonly RepositoryMoldes _moldeRepo;
        private readonly RepositoryEndurecedores _endepo;
        private readonly RepositoryFragancias _fragRepo;
        private readonly RepositoryPigmentos _pigRepo;
        private readonly RepositoryCeras _ceraRepo;
        private readonly RepositoryMechas _mechaRepo;
        private readonly RepositoryVelaFragancias _vFragRepo;
        private readonly RepositoryVelaPigmentos _vPigRepo;
        private readonly RepositoryPedidos _pediRepo;
        private readonly RepositoryClientes _cliRepo;
        private readonly RepositoryPacks _packRepo;

        public VelaController(RepositoryVelas velaRepo, RepositoryMoldes moldeRepo, RepositoryFragancias fragRepo,
            RepositoryPigmentos pigRepo, RepositoryCeras ceraRepo, RepositoryMechas mechaRepo, RepositoryVelaFragancias velaFragRepo,
            RepositoryVelaPigmentos velaPigRepo, RepositoryPedidos pediRepo, RepositoryClientes cliRepo, RepositoryPacks packRepo,
            RepositoryEndurecedores endepo)
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
            _packRepo = packRepo;
            _endepo = endepo;
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
            var listaEND = await _endepo.GetEndurecedorsAsync();
            var listaFrag = await _fragRepo.GetFraganciasAsync();
            var listaPig = await _pigRepo.GetPigmentosAsync();
            var listaCera = await _ceraRepo.GetCerasAsync();
            var listaMecha = await _mechaRepo.GetMechasAsync();

            //ViewBag.Fragancias = new SelectList(this.context.Fragancia.ToList(), "IDFrag", "FragNombre");
            //ViewBag.Pigmentos = new SelectList(this.context.Pigmento.ToList(), "IDPig", "ColorNombre");

            ViewData["Moldes"] = listaMoldes.Data;
            ViewData["Frag"] = listaFrag.Data;
            ViewData["Pig"] = listaPig.Data;
            ViewData["Cera"] = listaCera.Data;
            ViewData["Mecha"] = listaMecha.Data;
            ViewData["End"] = listaEND.Data;

            return PartialView("_CrearVelaView");
        }

        [HttpPost]
        public async Task<IActionResult>  _CrearVelaView(Vela vela, List<Guid> IDFragancias, List<Guid> IDPigmentos, IFormFile file)
        {
            var form = Helper.CreateMultipartFormData(vela, file);

            var response = await _velaRepo.InsertarVelaAsync(form);

            if (response.Data.IDVela != Guid.Empty)
            {
                var velfrag = new CustomApiResponse<VelaFragancia>();
                var velpig = new CustomApiResponse<VelaPigmento>();

                // Insertar fragancias
                foreach (var idFrag in IDFragancias)
                {
                    //Hacer select con los id. 
                    velfrag = await _vFragRepo.BuscarVelaFraganciaAsync(idFrag);

                    await _vFragRepo.InsertarVelaFraganciaAsync(velfrag.Data);
                }

                // Insertar pigmentos
                foreach (var idPig in IDPigmentos)
                {
                    velpig = await _vPigRepo.BuscarVelaPigmentoAsync(idPig);

                    await _vPigRepo.InsertarVelaPigmentoAsync(velpig.Data);
                }

                return RedirectToAction("DetallesView1", new { IDVela = response.Data.IDVela });
            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDVela, IFormFile? imagen)
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
                var listaFrag = await _fragRepo.GetFraganciasAsync();
                var listaPig = await _pigRepo.GetPigmentosAsync();
                var listaPack = await _packRepo.GetPacksAsync();
                var listaCera = await _ceraRepo.GetCerasAsync();
                var listaMecha = await _mechaRepo.GetMechasAsync();

                // Carga todas las fragancias y pigmentos para los selects
                //ViewBag.Fragancias = new SelectList(context.Fragancia.ToList(), "IDFrag", "FragNombre");
                //ViewBag.Pigmentos = new SelectList(context.Pigmento.ToList(), "IDPig", "ColorNombre");

                // Carga las fragancias seleccionadas para esta vela
                //ViewBag.FraganciasSeleccionadas = await _velaRepo.GetFraganciasPorVela(IDVela).Select(f => f.IDFrag).ToList();

                // Carga los pigmentos seleccionados para esta vela
                //ViewBag.PigmentosSeleccionados = await _velaRepo.GetPigmentosPorVela(IDVela).Select(p => p.IDPig).ToList();

                ViewData["Moldes"] = listaMoldes.Data;
                ViewData["Frag"] = listaFrag.Data;
                ViewData["Pig"] = listaPig.Data;
                ViewData["Cera"] = listaCera.Data;
                ViewData["Mecha"] = listaMecha.Data;
                ViewData["Pack"] = listaMecha.Data;

                ViewData["IDVela"] = IDVela;

                return View("~/Views/Vela/_ActVelaView", vela.Data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarView(Vela vela, IFormFile file)
        {
            var form = Helper.CreateMultipartFormData(vela, file);

            var response = await _velaRepo.ActualizarVelaAsync(vela.IDVela, form);

            if (response.Data.IDVela != Guid.Empty)
            {
                // Elimina todas las relaciones actuales y vuelve a insertar las seleccionadas
                await _vFragRepo.EliminarRelacionesFraganciaAsync(vela.IDVela);

                if (vela.Fragancias != null)
                {
                    foreach (var idFrag in vela.Fragancias)
                    {
                        // await _vFragRepo.InsertarVelaFraganciaAsync(idFrag); 
                    }
                }

                await _vPigRepo.EliminarRelacionesPigmentosAsync(vela.IDVela);

                if (vela.Pigmentos != null)
                {
                    foreach (var idPig in vela.Pigmentos)
                    {
                        //await _vPigRepo.InsertarVelaPigmentoAsync(idPig); 
                    }
                }

                return RedirectToAction("DetallesView1", new { IDVela = response.Data.IDVela });

            }
            else
            {
                ViewData["Error"] = response.Error.Mensaje;

                return View();
           }

        }

        [HttpGet]
        public async Task<IActionResult>  _DetallesVelaView()
        {
            try
            {
                var velas = await _velaRepo.GetVelasAsync();

                var listaMoldes = await _moldeRepo.GetMoldesAsync();
                var listaEnd = await _endepo.GetEndurecedorsAsync();
                var listaFrag = await _fragRepo.GetFraganciasAsync();
                var listaPig = await _pigRepo.GetPigmentosAsync();
                var listaCera = await _ceraRepo.GetCerasAsync();
                var listaMecha = await _mechaRepo.GetMechasAsync();

                ViewData["Moldes"] = listaMoldes.Data;
                ViewData["end"] = listaEnd.Data;
                ViewData["Frag"] = listaFrag.Data;
                ViewData["Pig"] = listaPig.Data;
                ViewData["Cera"] = listaCera.Data;
                ViewData["Mecha"] = listaMecha.Data;

                //ViewData["VELAS"] = velas;
                return PartialView("~/Views/Vela/_DetallesVelaView.cshtml", velas.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetallesView1(Guid IDVela)
        {
            var vela = await _velaRepo.BuscarVelaAsync(IDVela);
           

            return View("~/Views/Vela/_DetallesVelaView1.cshtml", vela.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _velaRepo.EliminarAsync(id);

            if (res.Error != null){ViewData["Error"] = res.Error.Mensaje;}
            ViewData["OK"] = res.Data;

            return RedirectToAction("_DetallesVelaView");
        }
    }
}