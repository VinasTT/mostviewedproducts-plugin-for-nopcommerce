using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Widgets.MostViewedProducts.Models
{
    public class MostViewedProductMap : EntityTypeConfiguration<MostViewedProduct>
    {
        public MostViewedProductMap()
        {
            ToTable("MostViewedProduct");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.ProductId);
            //Map the additional properties
            Property(m => m.ProductViewCount);
            
        }
    }
}
