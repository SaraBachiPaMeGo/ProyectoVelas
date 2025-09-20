using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public class VelaFraganciaService : IVelaFraganciaService
    {
        public Task<List<VelaFragancia>> GetFraganciasPorVelaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            throw new NotImplementedException();
        }
    }
}
