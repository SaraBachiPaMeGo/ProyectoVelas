using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace appVelas.Models
{

    public class Pigmento
    {
       
        
        [JsonProperty("IDPig")]
        public Guid IDPig { get; set; }

        [JsonProperty("Firma")]
        public string Firma { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("ColorNombre")]
        public string ColorNombre { get; set; }

        [JsonProperty("CompradoEn")]
        public string CompradoEn { get; set; }

        [JsonProperty("IDVela")]
        public Guid? IDVela { get; set; }

        [JsonProperty("Cantidad")]
        public decimal? Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }

        public List<VelaPigmento> VelaPigmentos { get; set; }

        //public ICollection<Documento>? Documentos { get; set; }

        [JsonProperty("CosteHist")]
        public decimal? CosteHist { get; set; }
    }
}
