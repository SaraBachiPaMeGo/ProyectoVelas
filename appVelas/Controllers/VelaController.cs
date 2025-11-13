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

            return PartialView("_CrearVelaView");
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

        [HttpGet]
        public async Task<IActionResult> ActualizarView(Guid IDVela, IFormFile? imagen)
        {
            var vela = await _velaRepo.BuscarVelaAsync(IDVela);

            if (imagen != null && imagen.Length > 0)
            {
                // Carpeta donde guardar las imágenes
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "imagenes");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Generar nombre único
                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imagen.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Guardar archivo físicamente
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(fileStream);
                }

                // Guardar ruta relativa en la BD
                vela.Data.Image = $"/uploads/imagenes/{uniqueFileName}";
            }


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

                return View("~/Views/Vela/_ActVelaView", vela.Data);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> ActualizarView(Vela vela)
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

            await _velaRepo.ActualizarVelaAsync(vela.IDVela, vela);

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
            return PartialView("~/Views/Vela/_DetallesVelaView", velas.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDVela)
        {
            var vela = await _velaRepo.BuscarVelaAsync(IDVela);
            var Cera = await _ceraRepo.BuscarCeraAsync(vela.Data.IDCera);
            var Mecha = await _mechaRepo.BuscarMechaAsync(vela.Data.IDMecha);

            if (vela.Data.IDMolde.HasValue && vela.Data.IDMolde.Value != Guid.Empty)
            {
                CustomApiResponse<Molde> moldes = await _moldeRepo.BuscarMoldeAsync(vela.Data.IDMolde ?? Guid.Empty);

                ViewData["Moldes"] = moldes;

            }

            if (vela.Data.IDFrag.HasValue && vela.Data.IDFrag.Value != Guid.Empty)
            {
                CustomApiResponse<Fragancia> frag = await _fragRepo.BuscarFraganciaAsync(vela.Data.IDFrag ?? Guid.Empty);
                ViewData["Frag"] = frag;

            }

            if (vela.Data.Pigmentos != null)
            {
                CustomApiResponse<Pigmento> pig = await _pigRepo.BuscarPigmentoAsync(vela.Data.IDPig ?? Guid.Empty);
                ViewData["Pig"] = pig;


            }

            if (vela.Data.IDPedido.HasValue && vela.Data.IDPedido.Value != Guid.Empty)
            {
                CustomApiResponse<Pedido> pedi = await _pediRepo.BuscarPedidoAsync(vela.Data.IDPedido ?? Guid.Empty);
                ViewData["pedi"] = pedi;

                if (pedi.Data.IDCliente != Guid.Empty)
                {
                    CustomApiResponse<Cliente> clien = await _cliRepo.BuscarClienteAsync(pedi.Data.IDCliente);
                    ViewData["clien"] = clien;

                }
            }
            

            ViewData["Cera"] = Cera;
            ViewData["Mecha"] = Mecha;
            ViewData["VELA"] = vela;

            return View("~/Views/Vela/_DetallesVelaView1", vela.Data);
        }
    }
}