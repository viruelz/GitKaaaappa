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
    public class SupplierDAL
    {
        public EFContext context = new EFContext();

        public IEnumerable<Supplier> GetOrderedByname()
        {

            return context.Suppliers.OrderBy(s => s.Name);

        }
        public Supplier ById(long id)
        {
            return context.Suppliers.Where(s => s.SupplierID == id).Include("Products.Category").First();

        }

        public void Save(Supplier supplier)
        {
            if (supplier.SupplierID == null)
                context.Suppliers.Add(supplier);
            else
                context.Entry(supplier).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Supplier Delete(long id)
        {

            var supplier = ById(id);

            context.Suppliers.Remove(supplier);
            context.SaveChanges();
            return supplier;
        }
    }
}
