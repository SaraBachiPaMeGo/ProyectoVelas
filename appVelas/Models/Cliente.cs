using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace appVelas.Models
{
    [Table("Cliente")]

    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDCliente")]
        public Guid IDCliente { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Direccion")]
        public string Direccion { get; set; }

        [Column("Telf")]
        public string Telf { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("IDPedido")]
        public Guid IDPedido { get; set; }
    }
}
