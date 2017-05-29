using Model.Tables;
using Newtonsoft.Json;
using ProjetoGustavoGuedes.Models;
using Service.Tables;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjetoGustavoGuedes.Controllers
{
    public class CategoriesController : Controller
    {


        private CategoryService categoryService = new CategoryService();

        #region INDEX
        // GET: Categories

        public async Task<ActionResult> Index()
        {
            var apiModel = new CategoryListAPIModel();

            var resp = await FromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content
                    .ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryListAPIModel>(result);
                }
            });

            return View(apiModel.Result);
        }
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

        public async Task<ActionResult> Create(Category category)
        {
            var apiModel = new CategoryAPIModel();

            var resp = await PostFromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryAPIModel>(result);
                }
            }, category);

            return RedirectToAction("Index");
        }
        #endregion create

        #region EDIT

        // GET: EditCategory
        public async Task<ActionResult> Edit(long? id)
        {

            return await GetViewById(id);
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
        private ActionResult SaveCategory(Category item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryService.Save(item);
                    return RedirectToAction("Index");
                }
                return View(item);
            }
            catch
            {
                return View(item);
            }
        }
        private async Task<HttpResponseMessage> FromAPI(Action<HttpResponseMessage> action, long? id = null)
        {
            using (var client = new HttpClient())
            {
                var reqUrl = HttpContext.Request.Url;
                var baseUrl = string.Format("{0}://{1}",
                    reqUrl.Scheme, reqUrl.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories";
                if (id != null) url += "/" + id;

                var request = await client.GetAsync(url);
                //HttpContent content = new HttpContent();
                //content.
               // var r = client.PostAsync(url, content);

                if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        private async Task<HttpResponseMessage> PostFromAPI(Action<HttpResponseMessage> action, Category category)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories";

                var request = await client.PostAsJsonAsync(url, category);

                if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        private async Task<ActionResult> GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CategoryAPIModel item = null;
            var resp = await FromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content
                    .ReadAsStringAsync().Result;
                    item = JsonConvert
                    .DeserializeObject<CategoryAPIModel>(result);
                }
            }, id.Value);

            if (!resp.IsSuccessStatusCode)
                return new HttpStatusCodeResult(resp.StatusCode);

            if (item.Message == "!OK" ||
                item.Result == null) return HttpNotFound();

            return View(item.Result);
        }
        #endregion


    }
}