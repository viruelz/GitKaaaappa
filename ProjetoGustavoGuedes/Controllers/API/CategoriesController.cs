using Model.Tables;
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
        private CategoryService categoryservice = new CategoryService();

        // GET: api/Categories
        public IEnumerable<Category> Get()
        {
            return categoryservice.GetOrgerByname();
        }

        // GET: api/Categories/5
        public Category Get(int id)
        {
            return categoryservice.ById(id);
        }

        // POST: api/Categories
        public void Post([FromBody]Category value)
        {
            categoryservice.Save(value);
        }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]Category value)
        {

            categoryservice.Save(value);

        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {

            categoryservice.Delete(id);

        }
    }
}
