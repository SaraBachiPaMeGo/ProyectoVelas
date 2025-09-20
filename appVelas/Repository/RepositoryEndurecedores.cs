using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryEndurecedores
    {
        private readonly IEndurecedorService _endurecedorService;


        public RepositoryEndurecedores(IEndurecedorService endurecedorService)
        {
            _endurecedorService = endurecedorService;
        }

        // ------------------------------------- Endurecedor ---------------------------------------------
        public async Task<List<Endurecedor>> GetEndurecedorsAsync()
        {
            return await _endurecedorService.GetEndurecedorsAsync();
        }

        public async Task<Endurecedor> BuscarEndurecedorAsync(Guid id)
        {
            return await _endurecedorService.BuscarEndurecedorAsync(id);
        }

        public async Task<bool> InsertarEndurecedorAsync(Endurecedor endurecedor)
        {
            return await _endurecedorService.InsertarEndurecedorAsync(endurecedor);
        }

        public async Task<bool> ActualizarEndurecedorAsync(Endurecedor endurecedor)
        {
            return await _endurecedorService.ActualizarEndurecedorAsync(endurecedor);
        }
    }
}
