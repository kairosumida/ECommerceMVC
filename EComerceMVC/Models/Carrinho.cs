using System.ComponentModel.DataAnnotations;

namespace EComerceMVC.Models
{
    public class Carrinho
    {
        [Key]
        public int RecordId { get; set; }

        public string CarrinhoId { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = " ")]
        [Range(0, 10000000, ErrorMessage = "Quantidade precisa estar entre 0 e 10000000")]
        public int Quantidade { get; set; }

        public System.DateTime DataCriacao { get; set; }

        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
        public string FormaPagamento { get; set; }
    }
}