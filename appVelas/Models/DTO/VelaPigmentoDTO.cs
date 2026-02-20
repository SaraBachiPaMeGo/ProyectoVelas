using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models.DTO
{
    public class VelaPigmentoDTO
    {
        [JsonProperty("IDPig")] public Guid? IDPig { get; set; }
        [JsonProperty("NombrePigmento")] public string NombrePigmento { get; set; }
        [JsonProperty("Cantidad")] public decimal? Cantidad { get; set; }
        [JsonProperty("Coste")] public decimal? Coste { get; set; }
    }
}
