using Model.Tables;
using Persistence.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tables
{
    public class CategoryService
    {
        private CategoryDAL dal = new CategoryDAL();


        public IEnumerable<Category> GetOrgerByname()
        {
            return dal.GetOrderedByname();
        }

        public Category ById(long id)
        {

            return dal.ById(id);
        }

        public void Save(Category category)
        {
            dal.Save(category);
        }

        public Category Delete(long id)
        {

            return dal.Delete(id);
        }
    }
}
