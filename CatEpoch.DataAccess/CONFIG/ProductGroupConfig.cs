using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess.POCO;

namespace CatEpoch.DataAccess.CONFIG
{
    public class ProductGroupConfig : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupConfig()
        {
            HasKey(k => k.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(t => t.ProductTrees).WithRequired(g => g.ProductGroup).HasForeignKey(p => p.GroupId);
            
        }
    }
}
