using Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Register
{
    public class Product
    {

        public long? ProductId { get; set; }
        public string Name { get; set; }

        public long? CategoryID { get; set; }
        public long? SupplierID { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
