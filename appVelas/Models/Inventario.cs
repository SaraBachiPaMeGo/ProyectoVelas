using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class Inventario
    {
        [JsonProperty("IDInventario")]
        public Guid IDInventario { get; set; }

        [JsonProperty("Firma")]
        public string Firma { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("CompradoEn")]
        public string CompradoEn { get; set; }

        [JsonProperty("Cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal Coste { get; set; }

        [JsonProperty("CosteHist")]
        public decimal? CosteHist { get; set; }
    }
}
