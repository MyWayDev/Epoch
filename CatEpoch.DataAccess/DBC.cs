﻿using System;
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
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<PromoDef> PromoDefs { get; set; }
        public virtual DbSet<PromoDetail> PromoDetails { get; set; } 
        public virtual DbSet<Period> Periods { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ProductGroupConfig());
            modelBuilder.Configurations.Add(new ProductTreeConfig());
            modelBuilder.Configurations.Add(new PromoConfig());
            modelBuilder.Configurations.Add(new PromoDefConfig());
            modelBuilder.Configurations.Add(new PromoDetailConfig());

        }
    }
}
