using Model.Register;
using Persistence.Contexts;
using Service.Register;
using Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoGustavoGuedes.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService = new ProductService();
        private CategoryService categoryService = new CategoryService();
        private SupplierService supplierService = new SupplierService();

        // GET: Product
        public ActionResult Index()
        {
            return View(productService.GetOrderedByName());
        }

        // GET: Product/Details/5
        public ActionResult Details(long? id)
        {

            return GetViewById(id);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            FillViewBag();
            return View();

        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            return SaveProduct(product);

        }

        // GET: Product/Edit/5
        public ActionResult Edit(long? id)
        {
            FillViewBag(productService.ById((long)id));
            return GetViewById(id);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            return SaveProduct(product);
        }

        //	GET:	Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return GetViewById(id);
        }
        //	POST:	Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = productService.Delete(id);
                TempData["Message"] = "Product	" + product.Name.ToUpper()
                                + "	Removed";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult GetViewById(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                                HttpStatusCode.BadRequest);
            }
            Product product = productService.ById((long)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        private void FillViewBag(Product product = null)
        {
            if (product == null)
            {
                ViewBag.CategoryID = new SelectList(categoryService.
                                GetOrderedByName(),
                                "CategoryID", "Name");
                ViewBag.SupplierID = new SelectList(supplierService.
                                GetOrgerByname(),
                                "SupplierID", "Name");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(categoryService.
                                GetOrderedByName(),
                                "CategoryID", "Name", product.CategoryID);
                ViewBag.SupplierID = new SelectList(supplierService.
                                GetOrgerByname(),
                                "SupplierID", "Name", product.SupplierID);
            }
        }


        private ActionResult SaveProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Save(product);
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }
    }
}