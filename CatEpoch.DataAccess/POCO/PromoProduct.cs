using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess.CONFIG;

namespace CatEpoch.DataAccess.POCO
{
   public class PromoProduct
    {
       public string ProductId { get; set; }
       public virtual Product Product { get; set; }
       public int PromoId { get; set; }
       public virtual Promo PromoDetail { get; set; }

       public int PromoProductId { get; set; }
       public float PromoPrice { get; set; }
       public int Qty { get; set; }
      
    }
}
