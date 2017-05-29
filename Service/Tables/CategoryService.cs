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


        public IQueryable<Category> Get()
        {
            return dal.Get();
        }
        public IQueryable<Category> GetOrderedByName()
        {
            return dal.GetOrderedByName();
        }
        public Category ById(long id)
        {

            return dal.ById(id);
        }

        public void Save(Category item)
        {
            dal.Save(item);
        }

        public Category Delete(long id)
        {

            return dal.Delete(id);
        }
    }
}
