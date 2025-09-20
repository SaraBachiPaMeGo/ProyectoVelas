using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryFragancias
    {
        private readonly IFraganciaService _fraganciaService;


        public RepositoryFragancias(IFraganciaService FraganciaService)
        {
            _fraganciaService = FraganciaService;
        }

        // ------------------------------------- Fragancia ---------------------------------------------
        public async Task<List<Fragancia>> GetFraganciasAsync()
        {
            return await _fraganciaService.GetFraganciasAsync();
        }

        public async Task<Fragancia> BuscarFraganciaAsync(Guid id)
        {
            return await _fraganciaService.BuscarFraganciaAsync(id);
        }

        public async Task<bool> InsertarFraganciaAsync(Fragancia fragancia)
        {
            return await _fraganciaService.InsertarFraganciaAsync(fragancia);
        }

        public async Task<bool> ActualizarFraganciaAsync(Fragancia fragancia)
        {
            return await _fraganciaService.ActualizarFraganciaAsync(fragancia);
        }
    }
}
