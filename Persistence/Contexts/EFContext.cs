using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model.Register;
using Model.Tables;

namespace Persistence.Contexts
{
    public class EFContext : DbContext
    {
        
        #region CONSTRUCTOR

        public EFContext()
            : base("Asp_Net_MVC_CS")
        {
            Database.SetInitializer<EFContext>(new DropCreateDatabaseIfModelChanges<EFContext>());
        }

        #endregion CONSTRUCTOR

        #region DbSet Properies
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion
    }

}
