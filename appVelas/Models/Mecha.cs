using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{
    [Table("Mecha")]

    public class Mecha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDMecha")]
        public Guid IDMecha { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("IDVela")]
        public Guid IDVela { get; set; }

        [Column("Cantidad")]
        public decimal Cantidad { get; set; }

        [Column("Coste")]
        public decimal Coste { get; set; }
    }
}
