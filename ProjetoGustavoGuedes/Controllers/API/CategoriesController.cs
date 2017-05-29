using Model.Tables;
using ProjetoGustavoGuedes.Models;
using Service.Register;
using Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjetoGustavoGuedes.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private CategoryService cService = new CategoryService();
        private ProductService pService = new ProductService();
        // GET: api/Categories
        public CategoryListAPIModel Get()
        {
            var apiModel = new CategoryListAPIModel();
            try
            {
                apiModel.Result = cService.Get().ToList();
            }
            catch (System.Exception)
            {

                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // GET: api/Categories/5
        public CategoryAPIModel Get(long? id)
        {
            var apiModel = new CategoryAPIModel();

            try
            {
                if (id == null)
                    apiModel.Message = "!OK";
                else
                {
                    apiModel.Result = cService.ById(id.Value);
                    if (apiModel.Result != null)
                        apiModel.Result.products = pService
                            .GetByCategory(id.Value).ToList();
                }
            }
            catch (System.Exception)
            {
                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // POST: api/Categories
        public void Post([FromBody]Category value)
        {
            cService.Save(value);
        }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]Category value)
        {

            cService.Save(value);

        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {

            cService.Delete(id);

        }
    }
}
