using Model.Tables;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tables
{
    public class CategoryDAL
    {
        private EFContext context = new EFContext();

        public IEnumerable<Category> GetOrderedByname()
        {

            return context.Categories.OrderBy(c => c.Name);

        }
        public Category ById(long? id)
        {
            return context.Categories.Where(c => c.CategoryID == id).Include("Products.Supplier").First();
        }

        public void Save(Category category)
        {
            if (category.CategoryID == null)
                context.Categories.Add(category);
            else
                context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Category Delete(long id)
        {

            var category = ById(id);

            context.Categories.Remove(category);
            context.SaveChanges();
            return category;
        }
    }
}

