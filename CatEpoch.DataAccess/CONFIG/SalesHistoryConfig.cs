using CatEpoch.DataAccess.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatEpoch.DataAccess.CONFIG
{
    public class SalesHistoryConfig : EntityTypeConfiguration<SalesHistory>
    {
        public SalesHistoryConfig()
        {
            Map(s => s.ToTable("WareHouse.SalesHistories"));
          
            HasKey(p => new
            {
                p.ProductId,
                p.Month,
                p.Year

            });
            
        }
    }
}
