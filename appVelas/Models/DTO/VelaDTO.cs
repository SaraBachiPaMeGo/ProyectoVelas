using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models.DTO
{
    public class VelaDTO
    {
        [JsonProperty("IDVela")] public Guid IDVela { get; set; }


        [JsonProperty("VelaNombre")] public string VelaNombre { get; set; }

        [JsonProperty("FechaReal")] public DateTime FechaReal { get; set; }

        [JsonProperty("Coste")] public decimal? Coste { get; set; }

        [JsonProperty("NombreCera")] public string NombreCera { get; set; }


        [JsonProperty("CantidadCera")] public decimal? CantidadCera { get; set; }

        [JsonProperty("CantidadMecha")] public decimal? CantidadMecha { get; set; }

        [JsonProperty("CantidadEnd")] public decimal? CantidadEnd { get; set; }

        //[JsonProperty("IDVela")] public List<Documento>? Documentos { get; set; }

        [JsonProperty("VelaPigmentos")] public List<VelaPigmentoDTO> VelaPigmentos { get; set; }

        [JsonProperty("VelaFragancias")] public List<VelaFraganciaDTO> VelaFragancias { get; set; }


        [JsonProperty("Image")]
        public byte[] Image { get; set; }          // 🔥 BYTES

        [JsonProperty("ImagenContentType")]
        public string ImagenContentType { get; set; } // opcional (muy recomendable)
    }
}
