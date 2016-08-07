using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess.CONFIG;
using CatEpoch.DataAccess.POCO;

namespace CatEpoch.DataAccess
{
    public class DBC : DbContext

    {
        public DBC()
            : base("Name=DataConnection")
        {
           // Configuration.LazyLoadingEnabled = false;

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductTree> ProductTrees { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ProductGroupConfig());
            modelBuilder.Configurations.Add(new ProductTreeConfig());

        }
    }
}
