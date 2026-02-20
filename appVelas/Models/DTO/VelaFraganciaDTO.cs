using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models.DTO
{
    public class VelaFraganciaDTO
    {
        [JsonProperty("IDFrag")] public Guid? IDFrag { get; set; }
        [JsonProperty("NombreFragancia")] public string NombreFragancia { get; set; }
        [JsonProperty("Cantidad")] public decimal? Cantidad { get; set; }
        [JsonProperty("Coste")] public decimal? Coste { get; set; }
    }
}
