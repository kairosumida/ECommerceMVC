using EComerceMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EComerceMVC.ViewModel
{
    public class CarrinhoDeComprasViewModel
    {
        [Key]
        public int RecordID { get; set; }
        public List<Carrinho> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public string FormaPagamento { get; set; }
    }
}