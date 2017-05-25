using Model.Tables;
using Newtonsoft.Json;
using Persistence.Contexts;
using Service.Tables;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjetoGustavoGuedes.Controllers
{
    public class CategoriesController : Controller
    {


        private CategoryService categoryService = new CategoryService();
        private EFContext context = new EFContext();

        #region INDEX
        // GET: Categories

        public async Task<ActionResult> Index()
        {

            var list = new List<Category>();

            var resp = await FromAPI(null, response =>
            {

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Category>>(result);
                }
            });
            return View(list);
        }

        /*public ActionResult Index()
        {//context.Suppliers.OrderBy(supplier => supplier.nomeSupplier)
            return View(categoryService.GetOrgerByname());
        }*/


        #endregion index

        #region CREATE
        // GET: Category
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            return SaveCategory(category);

        }
        #endregion create

        #region EDIT

        // GET: EditCategory
        public Task<ActionResult> Edit(long? id)
        {

            return GetViewById(id);
        }

        // POST: EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {

            return SaveCategory(category);

        }
        #endregion edit

        #region DELETE
        public async Task<ActionResult> Delete(long? id)
        {

            return await GetViewById(id);

        }
        // POST:DeleteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Category category = categoryService.Delete(id);
                TempData["Message"] = "Category	" +
                                category.Name.ToUpper() + "	Removed";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }


        }
        #endregion delete

        #region DETAILS
        // GET: DetailsCategory
        public async Task<ActionResult> Details(long? id)
        {

            return await GetViewById(id);
        }
        #endregion details

        #region LOADCATEGORY
        private ActionResult SaveCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryService.Save(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                return View(category);
            }
        }

        #endregion

        private async Task<HttpResponseMessage> FromAPI(long? id, Action<HttpResponseMessage> action)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority);

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "API/Categories";
                if (id != null)
                    url = "API/Categories" + id;

                var request = await client.GetAsync(url);

                if (action != null)
                    action.Invoke(request);


                return request;
            }
        }

        private async Task<ActionResult> GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category item = null;

            var resp = await FromAPI(id.Value, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<Category>(result);
                }
            });

            if (!resp.IsSuccessStatusCode)

                if (item == null)
                    return HttpNotFound();

            return View(item);
        }
    }
}