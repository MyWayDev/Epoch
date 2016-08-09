using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess.POCO;

namespace CatEpoch.DataAccess.CONFIG
{
   public class PromoDefConfig:EntityTypeConfiguration<PromoDef>
    {
       public PromoDefConfig()
       {
           
       }
    }
}
