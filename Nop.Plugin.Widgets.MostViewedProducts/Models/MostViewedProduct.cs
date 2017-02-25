using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using System.ComponentModel.DataAnnotations.Schema;
using Nop.Core;


namespace Nop.Plugin.Widgets.MostViewedProducts.Models
{
    public class MostViewedProduct : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product id
        /// </summary>
        public virtual int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the total view count
        /// </summary>
        public virtual int ProductViewCount { get; set; }
    }
}
