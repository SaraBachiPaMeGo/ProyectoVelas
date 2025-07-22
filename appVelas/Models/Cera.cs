﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{

    [Table("Cera")]

    public class Cera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDCera")]
        public Guid IDCera { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("Cantidad")]
        public decimal Cantidad { get; set; }

        [Column("Coste")]
        public decimal Coste { get; set; }

        [Column("IDVela")]
        public Guid IDVela { get; set; }
    }
}
