using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.POCO
{
   public class Promo
    {
       public string ProductId { get; set; }
       public virtual Product Product { get; set; }
       public virtual IList<PromoProduct> PromoProducts { get; set; }
       public virtual IList<Period> Periods { get; set; }
       public virtual IList<SalesHistory> SalesHistories { get; set; }
       
       public int PromoId { get; set; }
       public string PromoName { get; set; }
       public PromoGrade PromoGrade { get; set; }
    }
   public enum PromoType
   {
       DiscountPrice = 0,
       FreeProduct = 1,
       ListofChoices=2,
       Set=3
   }
    public enum PromoGrade
    {
        A = 1,
        B = 2,
        C = 3        
    }
}
