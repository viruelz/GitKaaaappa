using Model.Register;
using Persistence.Contexts;
using Service.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoGustavoGuedes.Controllers
{
    public class SupplierController : Controller
    {
        private SupplierService supplierService = new SupplierService();
        private EFContext context = new EFContext();

        #region INDEX
        // GET: Supplier
        public ActionResult Index()
        {//context.Suppliers.OrderBy(supplier => supplier.nomeSupplier)
         // return View(context.Suppliers.OrderBy(supplier => supplier.name));
            return View(supplierService.GetOrgerByname());
        }

        #endregion index

        #region CREATE
        // GET: Supplier
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            return SaveSupplier(supplier);

        }
        #endregion create

        #region EDIT

        // GET: EditCategory
        public ActionResult Edit(long? id)
        {

            return GetViewById(id);
        }

        // POST: EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            return SaveSupplier(supplier);
        }
        #endregion edit

        #region DELETE
        public ActionResult Delete(long? id)
        {
            return GetViewById(id);

        }
        /*
         * 	Fabricante
         fabricante = context.Fabricantes.Where(f => f.FabricanteId	 ==id).Include("Produtos.Categoria").First();
         * */
        // POST:DeleteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Supplier supplier = supplierService.Delete(id);
                TempData["Message"] = "Supplier	" + supplier.Name.ToUpper()
                                + "	Removed";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }
        #endregion delete

        #region DETAILS
        // GET: DetailsCategory
        public ActionResult Details(long? id)
        {

            return GetViewById(id);
        }
        #endregion details

        private ActionResult GetViewById(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                                HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierService.ById((long)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        private ActionResult SaveSupplier(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplierService.Save(supplier);
                    return RedirectToAction("Index");
                }
                return View(supplier);
            }
            catch
            {
                return View(supplier);
            }
        }
    }
}