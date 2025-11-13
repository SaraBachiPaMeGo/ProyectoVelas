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
        private readonly RepositoryClientes _cliRepo;
        private readonly RepositoryDocumentos _docRepo;

        public DocumentoController(RepositoryVelas velaRepo, RepositoryMoldes moldeRepo, RepositoryFragancias fragRepo,
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
        public PartialViewResult _CrearCeraView()
        {
            return PartialView("_CrearCeraView");
        }

        [HttpPost]
        public async Task<PartialViewResult> _CrearDocumentoView(Documento Documento)
        {
            var Documentos = await _docRepo.InsertarDocumentoAsync(Documento);

            return PartialView("Sucess", Documento);

        }

        [HttpGet]

        public async Task<IActionResult> ActualizarView(Guid IDDocumento)
        {
            var Documento = await _docRepo.BuscarDocumentoAsync(IDDocumento);

            if (Documento == null)
            {
                return PartialView("Error", new
                    ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Mensaje = "No se encontró ninguna Documento con el IDDocumento recibido. IDDocumento = " + IDDocumento +
                        "Error en el Controller de la vista _ActDocumentoView"
                });
            }
            else
                ViewData["IDDocumento"] = IDDocumento;
            return View("~/Views/Documento/_ActDocumentoView.cshtml", Documento.Data);
        }

        [HttpPost]
        public async Task<PartialViewResult> ActualizarView(Documento documento)
        {
            var Documentos = await _docRepo.ActualizarDocumentoAsync(documento.IDDoc, documento);

            return PartialView("Sucess", documento);
        }

        public async Task<PartialViewResult> _DetallesDocumentoView()
        {
            var Documento = await _docRepo.GetDocumentosAsync();

            return PartialView("~/Views/Documento/_DetallesDocumentoView.cshtml", Documento.Data);
        }

        public async Task<IActionResult> DetallesView1(Guid IDDoc)
        {
            var Documento = await _docRepo.BuscarDocumentoAsync(IDDoc);

            ViewData["Documento"] = Documento;
            return View("~/Views/Documento/_DetallesDocumentoView1.cshtml", Documento.Data);
        }

    }
}