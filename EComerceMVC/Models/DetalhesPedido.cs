namespace EComerceMVC.Models
{
    public class DetalhesPedido
    {
        public int DetalhesPedidoId { get; set; }

        public int PedidoId { get; set; }

        public string Cliente { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal? PrecoUnitario { get; set; }
    }
}