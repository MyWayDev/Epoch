using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
   public class Product
    {
       public Product()
       {
         ProductGroup = new ProductGroup();
          
       }
        public int GroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }         
        public virtual IList<Promo> Promos { get; set; }       
        public virtual IList<SalesHistory> SalesHistories { get; set; }
        public virtual IList<PromoProduct> PromoProducts { get; set; } 
        
        public string Id { get; set; }
        public string OldId { get; set; }
        public string ProductName { get; set; }
        public float BasePrice { get; set; }
        public bool Active { get; set; }
        public bool Discontnuied { get; set; }
        // ProductGroup Relationship One to Many.
        
    }
}
