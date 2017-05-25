using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Register
{
    public class Supplier
    {
        public long? SupplierID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> products { get; set; }
    }
}
