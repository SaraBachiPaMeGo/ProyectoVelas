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

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("Observ")]
        public string Observ { get; set; }

        [JsonProperty("FechaReal")]
        public DateTime FechaReal { get; set; }

        [JsonProperty("FechaVenta")]
        public DateTime FechaVenta { get; set; }

        [JsonProperty("GradFrag")]
        public decimal? GradFrag { get; set; }

        [JsonProperty("GradPig")]
        public decimal? GradPig { get; set; }

        [JsonProperty("IDFrag")]
        public Guid? IDFrag { get; set; }

        [JsonProperty("IDMolde")]
        public Guid? IDMolde { get; set; }

        [JsonProperty("IDPack")]
        public Guid? IDPack { get; set; }

        [JsonProperty("IDEndurecedor")]
        public Guid? IDEnd { get; set; }

        [JsonProperty("GradEnd")]
        public decimal? GradEnd { get; set; }

        [JsonProperty("IDPig")]
        public Guid? IDPig { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }

        [JsonProperty("IDPedido")]
        public Guid? IDPedido { get; set; }

        [JsonProperty("IDMecha")]
        public Guid IDMecha { get; set; }

        [JsonProperty("IDCera")]
        public Guid IDCera { get; set; }

        [JsonProperty("CantidadCera")]
        public decimal? CantidadCera { get; set; }

        [JsonProperty("CantidadMecha")]
        public decimal? CantidadMecha { get; set; }

        [JsonProperty("CantidadFrag")]
        public decimal? CantidadFrag { get; set; }

        [JsonProperty("CantidadPig")]
        public decimal? CantidadPig { get; set; }

        [JsonProperty("CantidadEnd")]
        public decimal? CantidadEnd { get; set; }


        [JsonProperty("CantidadPack")]
        public decimal? CantidadPack { get; set; }

        public ICollection<Documento>? Documentos { get; set; }

        public Pedido Pedido { get; set; }

        public List<VelaPigmento> Pigmentos { get; set; }
        public List<VelaFragancia> Fragancias { get; set; }
    }

}
