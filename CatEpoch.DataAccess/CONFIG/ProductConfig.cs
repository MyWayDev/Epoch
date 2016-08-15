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
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        
        public ProductConfig()
        {
            Map(p => p.ToTable("Product.Products"));
            
            HasKey(p => p.Id);
            
            HasRequired(g => g.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(g => g.GroupId);
            
            HasMany(p => p.Promos)
                .WithOptional(p => p.Product)
                .HasForeignKey(p=>p.ProductId)
                .WillCascadeOnDelete(true);
            
            HasMany(s=>s.SalesHistories)
                .WithRequired(p=>p.Product)
                .HasForeignKey(p=>p.ProductId)
                .WillCascadeOnDelete(false);
            
            HasMany(p=>p.PromoProducts)
                .WithOptional(p=>p.Product)
                .HasForeignKey(p=>p.ProductId)
                .WillCascadeOnDelete(true);
        
            Property(p => p.Id).HasMaxLength(10).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
            Property(p => p.ProductName).IsRequired().HasMaxLength(55);
            Property(p => p.OldId).IsOptional().HasMaxLength(10);
            //Property(p => p.Active).HasDatabaseGeneratedOption(databaseGeneratedOption:);











        }
    }
}
