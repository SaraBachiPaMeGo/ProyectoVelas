using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryMechas
    {
        private readonly IMechaService _mechaService;


        public RepositoryMechas(IMechaService mechaService)
        {
            _mechaService = mechaService;
        }

        // ------------------------------------- Mecha ---------------------------------------------
        public async Task<List<Mecha>> GetMechasAsync()
        {
            return await _mechaService.GetMechasAsync();
        }

        public async Task<Mecha> BuscarMechaAsync(Guid id)
        {
            return await _mechaService.BuscarMechaAsync(id);
        }

        public async Task<bool> InsertarMechaAsync(Mecha mecha)
        {
            return await _mechaService.InsertarMechaAsync(mecha);
        }

        public async Task<bool> ActualizarMechaAsync(Mecha mecha)
        {
            return await _mechaService.ActualizarMechaAsync(mecha);
        }
    }
}
