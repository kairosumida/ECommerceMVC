using EComerceMVC.Models;
using EComerceMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EComerceMVC.Controllers
{
    public class CarrinhoDeCompraController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/
        
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
        //
        // GET: /Store/AddToCart/5


        
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addProduto = storeDB.Produtos
                .Single(produto => produto.ProdutoId == id);

            // Add it to the shopping cart
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            cart.AddToCart(addProduto);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index", "Home", null);
        }
        [HttpPost]
        public ActionResult UpdateCartCount(int id, int cartCount)
        {
            CarrinhoDeComprasRemoverViewModel results = null;
            try
            {
                // Get the cart 
                var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

                // Get the name of the album to display confirmation 
                string albumName = storeDB.Carrrinhos.Single(item => item.RecordId == id).Produto.Nome;

                // Update the cart count 
                int itemCount = cart.UpdateCartCount(id, cartCount);

                //Prepare messages
                string msg = "A quantidade de " + Server.HtmlEncode(albumName) +
                        " foi atualizada no carrinho";
                if (itemCount == 0) msg = Server.HtmlEncode(albumName) +
                        " foi removida do carrinho.";
                //
                // Display the confirmation message 
                results = new CarrinhoDeComprasRemoverViewModel
                {
                    Message = msg,
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = itemCount,
                    DeleteId = id
                };
            }
            catch
            {
                results = new CarrinhoDeComprasRemoverViewModel
                {
                    Message = "Erro ocorreu ou entrada inválida...",
                    CartTotal = -1,
                    CartCount = -1,
                    ItemCount = -1,
                    DeleteId = id
                };
            }
            return Json(results);
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string nomeProduto = storeDB.Carrrinhos
                .Single(item => item.RecordId == id).Produto.Nome;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new CarrinhoDeComprasRemoverViewModel
            {
                Message = Server.HtmlEncode(nomeProduto) +
                    " Foi removido do carrinho.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
        [Authorize]
        public PartialViewResult Pagamento()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);


            return PartialView(cart);
        }

        // POST: Cliente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.



        // POST: Pedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Pagamento(Carrinho cart)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(cart).State = EntityState.Modified;
                storeDB.SaveChanges();
            }
            return PartialView(cart);
        }

    }
}