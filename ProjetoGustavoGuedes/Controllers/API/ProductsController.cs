using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model.Register;
using Persistence.Contexts;
using Service.Register;

namespace ProjetoGustavoGuedes.Controllers.API
{
    public class ProductsController : ApiController
    {
        private ProductService service = new ProductService();

        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            return service.Get();
        }

        // GET: api/Products/5
        public Product Get(long? id)
        {
            if (id == null) return null;

            return service.ById(id.Value);
        }

        // POST: api/Products
        public void Post([FromBody]Product value)
        {
            service.Save(value);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product value)
        {
            service.Save(value);
        }

        // DELETE: api/Products/5
        public Product Delete(int id)
        {
            return service.Delete(id);
        }
    }
}