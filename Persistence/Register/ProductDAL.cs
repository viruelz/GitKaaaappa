using Model.Register;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Register
{
    public class ProductDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Product> Get()
        {
            return context.Products;
        }
        public IQueryable<Product> GetOrderedByname()
        {

            return context.Products.Include(c => c.Category).Include(s => s.Supplier).OrderBy(p => p.Name);

        }

        public Product ById(long id)
        {
            return context.Products.Where(p => p.ProductId == id).Include(c => c.Category).Include(f => f.Supplier).First();

        }
        public IQueryable<Product> GetByCategory(long categoryId)
        {
            return context.Products.Where(c => c.CategoryID.HasValue && c.CategoryID.Value == categoryId).Include(f => f.Supplier);
        }
        public void Save(Product product)
        {
            if (product.ProductId == null)
                context.Products.Add(product);
            else
                context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Product Delete(long id)
        {

            var product = ById(id);

            context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }
    }
}
