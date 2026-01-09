using appVelas.Models;
using appVelas.Service;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryVelasFinalizadas
    {
        private readonly IVelaFinService _velaFinalizadaService;


        public RepositoryVelasFinalizadas(IVelaFinService velaFinalizadaService)
        {
            _velaFinalizadaService = velaFinalizadaService;
        }

        // ------------------------------------- VelaFinalizada ---------------------------------------------
        public async Task<CustomApiResponse<List<VelaFinalizada>>> GetVelaFinalizadasAsync()
        {
            return await _velaFinalizadaService.GetVelaFinalizadasAsync();
        }

        public async Task<CustomApiResponse<VelaFinalizada>> BuscarVelaFinalizadaAsync(Guid id)
        {
            return await _velaFinalizadaService.BuscarVelaFinalizadaAsync(id);
        }

        public async Task<CustomApiResponse<VelaFinalizada>> InsertarVelaFinalizadaAsync(VelaFinalizada VelaFinalizada)
        {
            return await _velaFinalizadaService.InsertarVelaFinalizadaAsync(VelaFinalizada);
        }

        public async Task<CustomApiResponse<VelaFinalizada>> ActualizarVelaFinalizadaAsync(Guid id, VelaFinalizada VelaFinalizada)
        {
            return await _velaFinalizadaService.ActualizarVelaFinalizadaAsync(id, VelaFinalizada);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _velaFinalizadaService.EliminarVelaFinAsync(id);
        }
    }
}
