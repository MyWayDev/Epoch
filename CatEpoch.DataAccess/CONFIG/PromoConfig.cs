using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatEpoch.DataAccess.POCO;

namespace CatEpoch.DataAccess.CONFIG
{
    public class PromoConfig : EntityTypeConfiguration<Promo>
    {
        public PromoConfig()
        {

            Map(p => p.ToTable("Promo.Promos"));
            
            HasKey(k => k.PromoId);

            HasMany(p => p.Periods)
                .WithMany(p => p.Promos);

            HasMany(s=>s.SalesHistories)
                .WithRequired(p=>p.Promo)
                .HasForeignKey(p=>p.PromoId)
                .WillCascadeOnDelete(false);
        }
    }
}
