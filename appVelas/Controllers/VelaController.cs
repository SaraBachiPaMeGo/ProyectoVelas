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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

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
        public async Task<IActionResult>  _CrearVelaView(Vela vela, List<VelaFragancia> vfrag,
            List<VelaPigmento> vpig, IFormFile file)
        {
            
            if (vela != null)
            {

                // 🔹 Inicializamos listas
                vela.VelaFragancias = new List<VelaFragancia>();
                vela.VelaPigmentos = new List<VelaPigmento>();

                // 🔥 Crear objetos VelaFragancia
                foreach (var item in vfrag)
                {
                    vela.VelaFragancias.Add(new VelaFragancia
                    {
                        IDFrag = item.IDFrag,
                        Cantidad = item.Cantidad,
                        Coste = 0, // si aplica,
                        NombreFragancia = item.NombreFragancia
                    });
                }

                foreach (var item in vpig)
                {
                    vela.VelaPigmentos.Add(new VelaPigmento
                    {
                        IDPig = item.IDPig,
                        Cantidad = item.Cantidad,
                        Coste = 0,
                        NombrePigmento = item.NombrePigmento
                    });
                }

                var form = Helper.CreateMultipartFormData(vela, file);

                form.Add(
                    new StringContent(JsonConvert.SerializeObject(vela.VelaFragancias)),
                    "vf"
                );

                form.Add(
                    new StringContent(JsonConvert.SerializeObject(vela.VelaPigmentos)),
                    "vp"
                );

                var response = await _velaRepo.InsertarVelaAsync(form);

                //PONER ALGO ALTERNATIVO SI IDVELA ES NULL
                return RedirectToAction("DetallesView1", new { IDVela = response.Data.IDVela });
            }
            else
            {
                var response = new CustomApiResponse<VelaFragancia>();
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

                if (vela.VelaFragancias != null)
                {
                    foreach (var idFrag in vela.VelaFragancias)
                    {
                        // await _vFragRepo.InsertarVelaFraganciaAsync(idFrag); 
                    }
                }

                await _vPigRepo.EliminarRelacionesPigmentosAsync(vela.IDVela);

                if (vela.VelaPigmentos != null)
                {
                    foreach (var idPig in vela.VelaPigmentos)
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