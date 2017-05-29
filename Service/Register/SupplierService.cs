using Model.Register;
using Persistence.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Register
{
    public class SupplierService
    {
        private SupplierDAL dal = new SupplierDAL();


        public IEnumerable<Supplier> GetOrgerByname()
        {
            return dal.GetOrderedByname();
        }

        public Supplier ById(long id)
        {

            return dal.ById(id);
        }

        public void Save(Supplier supplier)
        {
            dal.Save(supplier);
        }

        public Supplier Delete(long id)
        {

            return dal.Delete(id);
        }
    }
}
