using System;
using System.Collections.Generic;
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
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int GroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
    }
}
