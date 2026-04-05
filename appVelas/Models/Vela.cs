using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class Vela
    {
       
        
        [JsonProperty("IDVela")]
        public Guid IDVela { get; set; }

        [JsonProperty("VelaNombre")]
        public string VelaNombre { get; set; }

        [JsonProperty("Observ")]
        public string Observ { get; set; }

        [JsonProperty("FechaReal")]
        public DateTime FechaReal { get; set; }

        [JsonProperty("IDMolde")]
        public Guid? IDMolde { get; set; }

        [JsonProperty("IDEndurecedor")]
        public Guid? IDEnd { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }

        [JsonProperty("CosteHist")]
        public decimal? CosteHist { get; set; }

        [JsonProperty("IDCera")]
        public Guid IDCera { get; set; }

        [JsonProperty("CantidadCera")]
        public decimal? CantidadCera { get; set; }

        [JsonProperty("CantidadEnd")]
        public decimal? CantidadEnd { get; set; }

        [JsonProperty("Tiempo")]
        public decimal? Tiempo { get; set; }

        //public ICollection<Documento>? Documentos { get; set; }
        [JsonProperty("Image")]
        public byte[] Image { get; set; }          // 🔥 BYTES

        [JsonProperty("ImagenContentType")]
        public string ImagenContentType { get; set; } // opcional (muy recomendable)

        public List<VelaPigmento> VelaPigmentos { get; set; }
        public List<VelaFragancia> VelaFragancias { get; set; }
    }

}
