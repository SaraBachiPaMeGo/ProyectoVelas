using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryInventarios
    {
        private readonly IInventarioService _InventarioService;


        public RepositoryInventarios(IInventarioService InventarioService)
        {
            _InventarioService = InventarioService;
        }

        // ------------------------------------- Inventario ---------------------------------------------
        public async Task<CustomApiResponse<List<Inventario>>> GetInventariosAsync()
        {
            return await _InventarioService.GetInventariosAsync();
        }

        public async Task<CustomApiResponse<Inventario>> BuscarInventarioAsync(Guid id)
        {
            return await _InventarioService.BuscarInventarioAsync(id);
        }

        public async Task<CustomApiResponse<Inventario>> InsertarInventarioAsync(Inventario inv)
        {
            return await _InventarioService.InsertarInventarioAsync(inv);
        }

        public async Task<CustomApiResponse<Inventario>> ActualizarInventarioAsync(Guid id, Inventario inv)
        {
            return await _InventarioService.ActualizarInventarioAsync(id, inv);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _InventarioService.EliminarInventarioAsync(id);
        }
    }
}

