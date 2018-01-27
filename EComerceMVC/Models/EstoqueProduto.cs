using System.ComponentModel.DataAnnotations;

namespace EComerceMVC.Models
{
    public class EstoqueProduto
    {
        [Key]
        public int EstoqueId { get; set; }

        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}