using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EComerceMVC.Models;

namespace EComerceMVC.Controllers
{
    public class ProdutoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produto
        public ActionResult Index()
        {
            return View(db.Produtos.Where(x=>x.Ativo==true).ToList());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,Nome,Preco,Descricao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Preco,Descricao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        
        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            produto.Ativo = false;
            db.Entry(produto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult ProdutoP()
        {
            var produtos = from s in db.Produtos.Where(x => x.Ativo)
                           select s;
            
            return PartialView(produtos.ToList());
        }
        public PartialViewResult QuantidadeEstoqueP(int? id)
        {
            EstoqueProduto ep = new EstoqueProduto();
            ApplicationDbContext ex = new ApplicationDbContext();
            if (ex.EstoqueProdutos.Where(x => x.ProdutoId == id).FirstOrDefault() != null)
                ViewBag.Quantidade = ex.EstoqueProdutos.Where(x => x.ProdutoId == id).Sum(x => x.Quantidade);
            else
                ViewBag.Quantidade = 0;

            return PartialView();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto Produto = db.Produtos.Find(id);
            if (Produto == null)
            {
                return HttpNotFound();
            }
            return View(Produto);
        }

        // POST: AddProdutos/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(int id)
        {
            Produto  Produto = db.Produtos.Find(id);
            db.Produtos.Remove(Produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
