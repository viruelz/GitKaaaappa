using Model.Register;
using Persistence.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Register
{
    public class ProductService
    {
        private ProductDAL dal = new ProductDAL();

        public IQueryable<Product> Get()
        {
            return dal.Get();
        }
        public IQueryable<Product> GetOrderedByName()
        {
            return dal.GetOrderedByname();
        }
        
        public Product ById(long id)
        {

            return dal.ById(id);
        }

        public void Save(Product product)
        {
            dal.Save(product);
        }

        public Product Delete(long id)
        {

            return dal.Delete(id);
        }
        public IQueryable<Product> GetByCategory(long categoryId)
        {
            return dal.GetByCategory(categoryId);
        }
    }
}
