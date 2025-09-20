using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{
    public class Mecha
    {
       
        
        [JsonProperty("IDMecha")]
        public Guid IDMecha { get; set; }

        [JsonProperty("Firma")]
        public string Firma { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("CompradoEn")]
        public string CompradoEn { get; set; }

        [JsonProperty("IDVela")]
        public Guid? IDVela { get; set; }

        [JsonProperty("Cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal Coste { get; set; }
    }
}
