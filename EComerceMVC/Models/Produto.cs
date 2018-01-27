using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EComerceMVC.Models
{
    public class Produto
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(120)]
        public String Nome { get; set; }

        [Required]
        [Range(0, 99999.99,
             ErrorMessage = "O Preço de Venda deve estar entre " +
                            "10,00 e 99999,99.")]
        public Decimal Preco { get; set; }
        [StringLength(1024)]
        public String Descricao { get; set; }
        public Boolean Ativo { get; set; } = true;
    }
}