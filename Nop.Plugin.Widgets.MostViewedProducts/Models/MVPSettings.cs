using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Widgets.MostViewedProducts.Models
{
    public class MVPSettings : CatalogSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to show most viewed on home page
        /// </summary>
        public bool ShowMVPOnHomepage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show most viewed on home page
        /// </summary>
        public int NumberOfMVPOnHomepage { get; set; }
    }
}
