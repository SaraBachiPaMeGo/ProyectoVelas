using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{

    public class Molde
    {
       
        
        [JsonProperty("IDMolde")]
        public Guid IDMolde { get; set; }

        [JsonProperty("MoldeNombre")]
        public string MoldeNombre { get; set; }

        [JsonProperty("Firma")]
        public string Firma { get; set; }

        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("CompradoEn")]
        public string CompradoEn { get; set; }

        [JsonProperty("GramCera")]
        public decimal? GramCera { get; set; }

        [JsonProperty("Medidas")]
        public string Medidas { get; set; }

        [JsonProperty("Duracion")]
        public decimal? Duracion { get; set; }

        [JsonProperty("CMMecha")]
        public decimal? CMMecha { get; set; }

        [JsonProperty("Observ")]
        public string Observ { get; set; }

        [JsonProperty("MilAgua")]
        public decimal? MilAgua { get; set; }

        [JsonProperty("IDVela")]
        public Guid? IDVela { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }

        [JsonProperty("Cantidad")]
        public int? Cantidad { get; set; }
        //public ICollection<Documento>? Documentos { get; set; }

    }
}
