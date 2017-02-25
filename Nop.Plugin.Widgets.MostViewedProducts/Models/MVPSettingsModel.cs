using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Admin.Models.Settings;

namespace Nop.Plugin.Widgets.MostViewedProducts.Models
{
    public class MVPSettingsModel : CatalogSettingsModel
    {
        [NopResourceDisplayName("MVP.Settings.ShowMVPOnHomepage")]
        public bool ShowMVPOnHomepage { get; set; }
        public bool ShowMVPOnHomepage_OverrideForStore { get; set; }

        [NopResourceDisplayName("MVP.Settings.NumberOfMVPOnHomepage")]
        public int NumberOfMVPOnHomepage { get; set; }
        public bool NumberOfMVPOnHomepage_OverrideForStore { get; set; }
    }
}
