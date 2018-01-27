using EComerceMVC.Models;
using EComerceMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EComerceMVC.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        ApplicationDbContext storeDB = new ApplicationDbContext();
        // GET: Checkout
        public ActionResult Index()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new CarrinhoDeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        public ActionResult ClienteEFormaPagamento(string pagamento)
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new CarrinhoDeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            viewModel.FormaPagamento = pagamento;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClienteEFormaPagamento(FormCollection values)
        {
            ViewBag.Clientes = storeDB.Clientes;
            var order = new Pedido();
            TryUpdateModel(order);
            string forma = Request.Form["FormaPagamento"];
            try
            {
                order.FormaPagamento = forma;
                order.UsuarioId = (int)Session["Cliente"];
                order.DataPedido = DateTime.Now;
                order.Total = CarrinhoDeCompras.GetCart(this.HttpContext).GetTotal();
                //Save Order
                storeDB.Pedidos.Add(order);
                storeDB.SaveChanges();
                //Process the order
                var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                        new { id = order.PedidoId });
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }

        }
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Pedidos.Any(
                o => o.PedidoId == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}