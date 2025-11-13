using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace appVelas.Controllers
{
    public class DocumentoController : Controller
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
        private readonly RepositoryDocumentos _docRepo;

        public DocumentoController(RepositoryVelas velaRepo, RepositoryMoldes moldeRepo, RepositoryFragancias fragRepo,
            RepositoryPigmentos pigRepo, RepositoryCeras ceraRepo, RepositoryMechas mechaRepo, RepositoryVelaFragancias velaFragRepo,
            RepositoryVelaPigmentos velaPigRepo, RepositoryPedidos pediRepo, RepositoryDocumentos _docRepo)
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
            _docRepo = _docRepo;
        }

        public async Task<IActionResult> Index()
        {
            var doc = await _docRepo.GetDocumentosAsync();
            return View(doc);
        }

        public PartialViewResult _CrearDocView()
        {
            return PartialView("_CrearDocView");
        }

        [HttpPost]
        public async Task<PartialViewResult> _CrearDocView(Documento Documento)
        {
            var Documentos = await _docRepo.InsertarDocumentoAsync(Documento);

            return PartialView("Sucess", Documento);

        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDDoc)
        {
            var Documento = await _docRepo.BuscarDocumentoAsync(IDDoc);

            if (Documento == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ningún Documento con el IDDoc recibido. IDDoc = " + IDDoc +
                        "Error en el Controller de la vista _ActDocView"
                });
            }
            else
                ViewData["IDDoc"] = IDDoc;
            return View("~/Views/Documento/_ActDocView.cshtml", Documento.Data);
        }

        [HttpPost]
        public async Task<PartialViewResult> ActualizarView(Documento doc)
        {
            var documento = await _docRepo.ActualizarDocumentoAsync(doc.IDDoc, doc);

            return PartialView("Sucess", documento);
        }

        public async Task<PartialViewResult> _DetallesDocView()
        {
            var Documento = await _docRepo.GetDocumentosAsync();

            return PartialView("~/Views/Documento/_DetallesDocView.cshtml", Documento.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDDoc)
        {
            var Documento = await _docRepo.BuscarDocumentoAsync(IDDoc);

            ViewData["Documento"] = Documento;
            return View("~/Views/Documento/_DetallesDocView1.cshtml", Documento.Data);
        }

    }
}