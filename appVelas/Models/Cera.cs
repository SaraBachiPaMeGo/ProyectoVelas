using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{

    public class Cera
    {
       
        [JsonProperty("IDCera")]
        public Guid IDCera { get; set; }

        [JsonProperty("Firma")]
        public string Firma { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("CompradoEn")]
        public string CompradoEn { get; set; }

        [JsonProperty("Cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal Coste { get; set; }

        [JsonProperty("IDVela")]
        public ICollection<Documento>? Documentos { get; set; }
        public Guid? IDVela { get; set; }
    }
}
